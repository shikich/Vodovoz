﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using Gamma.GtkWidgets;
using Gtk;
using NHibernate.Criterion;
using NLog;
using QS.Dialog.Gtk;
using QS.Dialog.GtkUI;
using QS.DomainModel.UoW;
using QS.Project.Journal;
using QS.Project.Services;
using QS.Services;
using Vodovoz.Dialogs.OrderWidgets;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Sale;
using Vodovoz.EntityRepositories.Logistic;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.EntityRepositories.Undeliveries;
using Vodovoz.Filters.ViewModels;
using Vodovoz.Infrastructure.Services;
using Vodovoz.Journals.FilterViewModels;
using Vodovoz.Journals.JournalViewModels;
using Vodovoz.JournalViewers;
using Vodovoz.JournalViewModels;
using Vodovoz.TempAdapters;
using Order = Vodovoz.Domain.Orders.Order;

namespace Vodovoz
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class RouteListCreateItemsView : WidgetOnTdiTabBase
	{
		private static Logger _logger = LogManager.GetCurrentClassLogger();
		private readonly IRouteColumnRepository _routeColumnRepository = new RouteColumnRepository();
		private readonly IOrderRepository _orderRepository = new OrderRepository();

		private int goodsColumnsCount = -1;
		private bool _isEditable = true;
		private bool _canOpenOrder = true;
		private bool _isLogistician;

		private IPermissionResult _permissionResult;
		private RouteListItem _selectedRouteListItem;
		private IList<RouteColumn> _columnsInfo;

		private IList<RouteColumn> ColumnsInfo => _columnsInfo ?? _routeColumnRepository.ActiveColumns(RouteListUoW);

		private IUnitOfWorkGeneric<RouteList> routeListUoW;

		public IUnitOfWorkGeneric<RouteList> RouteListUoW {
			get => routeListUoW;
			set {
				if(routeListUoW == value)
					return;
				routeListUoW = value;
				if(RouteListUoW.Root.Addresses == null)
					RouteListUoW.Root.Addresses = new List<RouteListItem>();
				items = RouteListUoW.Root.ObservableAddresses;

                SubscribeOnChanges();

                UpdateColumns();

				ytreeviewItems.ItemsDataSource = items;
				ytreeviewItems.Reorderable = true;
				CalculateTotal();
			}
		}

		public void SubscribeOnChanges()
        {
            RouteListUoW.Root.ObservableAddresses.ElementChanged += Items_ElementChanged;
            RouteListUoW.Root.ObservableAddresses.ListChanged += Items_ListChanged;
            RouteListUoW.Root.ObservableAddresses.ElementAdded += Items_ElementAdded;

            items = RouteListUoW.Root.ObservableAddresses;
            ytreeviewItems.ItemsDataSource = items;
            ytreeviewItems.Reorderable = true;
            ytreeviewItems?.YTreeModel?.EmitModelChanged();
        }

		private bool CanEditRows => _isLogistician
										&& (_permissionResult.CanCreate && RouteListUoW.Root.Id == 0 || _permissionResult.CanUpdate)
										&& RouteListUoW.Root.Status != RouteListStatus.Closed
										&& RouteListUoW.Root.Status != RouteListStatus.MileageCheck;

		private bool disableColumnsUpdate;

		public bool DisableColumnsUpdate {
			get => disableColumnsUpdate;
			set {
				if(disableColumnsUpdate == value)
					return;

				disableColumnsUpdate = value;
				if(!disableColumnsUpdate)
					UpdateColumns();
			}
		}

		void Items_ElementAdded(object aList, int[] aIdx)
		{
			UpdateColumns();
			CalculateTotal();
        }

        void Items_ListChanged(object aList)
		{
			UpdateColumns();
		}

		public void OnForwarderChanged()
		{
			UpdateColumns();
		}

		private void UpdateColumns()
		{
			if(disableColumnsUpdate)
				return;

			var goodsColumns = items.SelectMany(i => i.GoodsByRouteColumns.Keys).Distinct().ToArray();

			var config = ColumnsConfigFactory.Create<RouteListItem>()
			.AddColumn("Заказ").AddTextRenderer( node => node.Order.Id.ToString())
			.AddColumn("Адрес").AddTextRenderer(node => node.Order.DeliveryPoint == null ? "Точка доставки не установлена" : string.Format("{0} д.{1}", node.Order.DeliveryPoint.Street, node.Order.DeliveryPoint.Building))
			.AddColumn("Время").AddTextRenderer(node => node.Order.DeliverySchedule == null ? string.Empty : node.Order.DeliverySchedule.Name);
			if(goodsColumnsCount != goodsColumns.Length) {
				goodsColumnsCount = goodsColumns.Length;

				foreach(var column in ColumnsInfo) {
					if(!goodsColumns.Contains(column.Id))
						continue;
					int id = column.Id;
					config = config.AddColumn(column.Name).AddTextRenderer(a => a.GetGoodsAmountForColumn(id).ToString("N0"));
				}
			}
			if(RouteListUoW.Root.Forwarder != null) {
				config
					.AddColumn("C экспедитором")
					.AddToggleRenderer(node => node.WithForwarder)
					.AddSetter((cell, node) => cell.Activatable = CanEditRows);
			}
			config
				.AddColumn("Товары").AddTextRenderer(x => ShowAdditional(x.Order.OrderItems))
				.AddColumn("К клиенту")
				.AddTextRenderer(x => x.EquipmentsToClientText, expand: false)
				.AddColumn("От клиента")
				.AddTextRenderer(x => x.EquipmentsFromClientText, expand: false);
			ytreeviewItems.ColumnsConfig =
				config.RowCells().AddSetter<CellRendererText>((c, n) => c.Foreground = n.Order.RowColor)
				.Finish();
		}

		private string ShowAdditional(IList<OrderItem> orderItems)
		{
			List<string> stringParts = new List<string>();

			var additionalItems = orderItems
					.Where(x => x.Nomenclature.Category != NomenclatureCategory.water 
								&& x.Nomenclature.Category != NomenclatureCategory.equipment
								&& x.Nomenclature.Category != NomenclatureCategory.service
								&& x.Nomenclature.Category != NomenclatureCategory.deposit
								&& x.Nomenclature.Category != NomenclatureCategory.master
					);
			foreach (var item in additionalItems)
			{
				var nomCount = item.Count.ToString($"N{item.Nomenclature.Unit.Digits}");
				stringParts.Add($"{item.Nomenclature.Name}: {nomCount}");
			}

			return string.Join("\n", stringParts);
		}

		public void IsEditable(bool isEditable, bool canOpenOrder = true)
		{
			_isEditable = isEditable;
			enumbuttonAddOrder.Sensitive = isEditable;
			_canOpenOrder = canOpenOrder;
			UpdateSensitivity();
		}

		void Items_ElementChanged(object aList, int[] aIdx)
		{
			UpdateColumns();
			CalculateTotal();
		}

		public RouteListCreateItemsView()
		{
			this.Build();
			enumbuttonAddOrder.ItemsEnum = typeof(AddOrderEnum);
			ytreeviewItems.Selection.Changed += OnSelectionChanged;
		}
		
		public void SetPermissionParameters(IPermissionResult permissionResult, bool isLogistician)
		{
			_permissionResult = permissionResult;
			_isLogistician = isLogistician;
		}

		void OnSelectionChanged(object sender, EventArgs e)
		{
			_selectedRouteListItem = ytreeviewItems.GetSelectedObject<RouteListItem>();
			UpdateSensitivity();
		}

		private void UpdateSensitivity()
		{
			buttonOpenOrder.Sensitive = _selectedRouteListItem != null && _canOpenOrder;
			buttonDelete.Sensitive = _selectedRouteListItem != null && _isEditable;
		}

		GenericObservableList<RouteListItem> items;

		protected void OnButtonDeleteClicked(object sender, EventArgs e)
		{
			if(!RouteListUoW.Root.TryRemoveAddress(_selectedRouteListItem, out string message, new RouteListItemRepository()))
				MessageDialogHelper.RunWarningDialog(
					"Невозможно удалить",
					message,
					ButtonsType.Ok
				);
			CalculateTotal();
		}

		protected void OnEnumbuttonAddOrderEnumItemClicked(object sender, QS.Widgets.EnumItemClickedEventArgs e)
		{
			AddOrderEnum choice = (AddOrderEnum)e.ItemEnum;
			switch(choice) {
				case AddOrderEnum.AddOrders:
					AddOrders();
					break;
				case AddOrderEnum.AddAllForRegion:
					AddOrdersFromRegion();
					break;
				default:
					break;
			}
		}

		protected void AddOrders()
		{
			var filter = new OrderJournalFilterViewModel(
				new CounterpartyJournalFactory(),
				new DeliveryPointJournalFactory())
			{
				ExceptIds = RouteListUoW.Root.Addresses.Select(address => address.Order.Id).ToArray()
			};

			var geoGrpIds = RouteListUoW.Root.GeographicGroups.Select(x => x.Id).ToArray();
			if(geoGrpIds.Any()) {
				GeographicGroup geographicGroupAlias = null;
				var districtIds = RouteListUoW.Session.QueryOver<District>()
					.Left.JoinAlias(d => d.GeographicGroup, () => geographicGroupAlias)
					.Where(() => geographicGroupAlias.Id.IsIn(geoGrpIds))
					.Select
					  (
						  Projections.Distinct(
						  Projections.Property<District>(x => x.Id)
					  )
					)
					.List<int>()
					.ToArray();

				filter.IncludeDistrictsIds = districtIds;
			}

			//Filter Creating
			filter.SetAndRefilterAtOnce(
				x => x.RestrictStartDate = RouteListUoW.Root.Date.Date,
				x => x.RestrictEndDate = RouteListUoW.Root.Date.Date,
				x => x.RestrictStatus = OrderStatus.Accepted,
				x => x.RestrictWithoutSelfDelivery = true,
				x => x.RestrictOnlySelfDelivery = false,
				x => x.RestrictHideService = true
			);

			var orderSelectDialog = new OrderForRouteListJournalViewModel(filter, UnitOfWorkFactory.GetDefaultFactory,
				ServicesConfig.CommonServices, new OrderSelectorFactory(), new EmployeeJournalFactory(), new CounterpartyJournalFactory(),
				new DeliveryPointJournalFactory(), new SubdivisionJournalFactory(), new GtkTabsOpener(),
				new UndeliveredOrdersJournalOpener(), new EmployeeService(), new UndeliveredOrdersRepository())
			{
				SelectionMode = JournalSelectionMode.Multiple
			};

			//Selected Callback
			orderSelectDialog.OnEntitySelectedResult += (sender, ea) =>
			{
				var selectedIds = ea.SelectedNodes.Select(x => x.Id);
				if(!selectedIds.Any()) {
					return;
				}
				foreach(var selectedId in selectedIds) {
					var order = RouteListUoW.GetById<Order>(selectedId);
					if(order == null) {
						return;
					}
					RouteListUoW.Root.AddAddressFromOrder(order);
				}
			};

			//OpenTab
			MyTab.TabParent.AddSlaveTab(MyTab, orderSelectDialog);
		}

		protected void AddOrdersFromRegion()
		{
			var filter = new DistrictJournalFilterViewModel { Status = DistrictsSetStatus.Active, OnlyWithBorders = true };
			var journalViewModel = new DistrictJournalViewModel(filter, UnitOfWorkFactory.GetDefaultFactory, ServicesConfig.CommonServices) {
				SelectionMode = JournalSelectionMode.Single, EnableDeleteButton = false, EnableEditButton = false, EnableAddButton = false
			};
			journalViewModel.OnEntitySelectedResult += (o, args) => {
				var selectedDistrict = args.SelectedNodes.FirstOrDefault();
				if(selectedDistrict != null)
				{
					var orders = _orderRepository.GetAcceptedOrdersForRegion(
						RouteListUoW, RouteListUoW.Root.Date, RouteListUoW.GetById<District>(selectedDistrict.Id));
					
					foreach(var order in orders)
					{
						if(RouteListUoW.Root.ObservableAddresses.All(a => a.Order.Id != order.Id))
						{
							RouteListUoW.Root.AddAddressFromOrder(order);
						}
					}
				}
			};
			MyTab.TabParent.AddSlaveTab(MyTab, journalViewModel);
		}

		void CalculateTotal()
		{
			var total = routeListUoW.Root.Addresses.SelectMany(a => a.Order.OrderItems)
				.Where(i => i.Nomenclature.Category == NomenclatureCategory.water && i.Nomenclature.TareVolume == TareVolume.Vol19L)
				.Sum(i => i.Count);

			labelSum.LabelProp = $"Всего бутылей: {total:N0}";
			UpdateWeightInfo();
			UpdateVolumeInfo();
		}

		public virtual void UpdateWeightInfo()
		{
			if(RouteListUoW != null && RouteListUoW.Root.Car != null) {
				string maxWeight = RouteListUoW.Root.Car.MaxWeight > 0
								   ? RouteListUoW.Root.Car.MaxWeight.ToString()
								   : " ?";
				string weight = RouteListUoW.Root.HasOverweight()
											? $"<span foreground = \"red\">Перегруз на {RouteListUoW.Root.Overweight()} кг.</span>"
											: $"<span foreground = \"green\">Вес груза: {RouteListUoW.Root.GetTotalWeight()}/{maxWeight} кг.</span>";
				lblWeight.LabelProp = weight;
			}
		}

		public virtual void UpdateVolumeInfo()
		{
			if(RouteListUoW != null && RouteListUoW.Root.Car != null) {
				string maxVolume = RouteListUoW.Root.Car.MaxVolume > 0
								   ? RouteListUoW.Root.Car.MaxVolume.ToString()
								   : " ?";
				string volume = RouteListUoW.Root.HasVolumeExecess()
											? string.Format("<span foreground = \"red\">Объём груза превышен на {0} м<sup>3</sup>.</span>", RouteListUoW.Root.VolumeExecess())
											: string.Format("<span foreground = \"green\">Объём груза: {0}/{1} м<sup>3</sup>.</span>", RouteListUoW.Root.GetTotalVolume(), maxVolume);
				lblVolume.LabelProp = volume;
			}
		}

		protected void OnButtonOpenOrderClicked(object sender, EventArgs e)
		{
			if(_selectedRouteListItem != null)
			{
				MyTab.TabParent.OpenTab(
					DialogHelper.GenerateDialogHashName<Order>(_selectedRouteListItem.Order.Id),
					() => new OrderDlg(_selectedRouteListItem.Order)
				);
			}
		}

		protected void OnYtreeviewItemsRowActivated(object o, RowActivatedArgs args)
		{
			if(_canOpenOrder)
			{
				buttonOpenOrder.Click();
			}
		}
	}

	public enum AddOrderEnum
	{
		[Display(Name = "Выбрать заказы...")] AddOrders,
		[Display(Name = "Все заказы для логистического района")] AddAllForRegion
	}
}
