﻿using System;
using Gamma.GtkWidgets;
using Gamma.Utilities;
using Gtk;
using Pango;
using QS.Dialog.Gtk;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Tdi;
using QS.Utilities;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.SidePanel.InfoProviders;
using Vodovoz.ViewWidgets.Mango;

namespace Vodovoz.SidePanel.InfoViews
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CounterpartyPanelView : Bin, IPanelView
	{
		private readonly IOrderRepository _orderRepository = new OrderRepository();
		private Counterparty _counterparty;

		public CounterpartyPanelView()
		{
			this.Build();
			Configure();
		}

		private void Configure()
		{
			labelName.LineWrapMode = Pango.WrapMode.WordChar;
			labelLatestOrderDate.LineWrapMode = Pango.WrapMode.WordChar;
			ytreeCurrentOrders.ColumnsConfig = ColumnsConfigFactory.Create<Order>()
				.AddColumn("Номер")
				.AddNumericRenderer(node => node.Id)
				.AddColumn("Дата")
				.AddTextRenderer(node => node.DeliveryDate.HasValue ? node.DeliveryDate.Value.ToShortDateString() : string.Empty)
				.AddColumn("Статус")
				.AddTextRenderer(node => node.OrderStatus.GetEnumTitle())
				.Finish();
		}
		
		private void Refresh(object changedObj)
		{
			_counterparty = changedObj as Counterparty;
			RefreshData();
		}

		#region IPanelView implementation

		public IInfoProvider InfoProvider { get; set; }

		public void Refresh()
		{
			_counterparty = (InfoProvider as ICounterpartyInfoProvider)?.Counterparty;
			RefreshData();
		}

		private void RefreshData()
		{
			if(_counterparty == null)
			{
				buttonSaveComment.Sensitive = false;
				return;
			}

			buttonSaveComment.Sensitive = true;
			labelName.Text = _counterparty.FullName;
			SetupPersonalManagers();
			textviewComment.Buffer.Text = _counterparty.Comment;

			var latestOrder = _orderRepository.GetLatestCompleteOrderForCounterparty(InfoProvider.UoW, _counterparty);
			if(latestOrder != null)
			{
				var daysFromLastOrder = (DateTime.Today - latestOrder.DeliveryDate.Value).Days;
				labelLatestOrderDate.Text = string.Format(
					"{0} ({1} {2} назад)",
					latestOrder.DeliveryDate.Value.ToShortDateString(),
					daysFromLastOrder,
					NumberToTextRus.Case(daysFromLastOrder, "день", "дня", "дней")
				);
			}
			else
			{
				labelLatestOrderDate.Text = "(Выполненных заказов нет)";
			}

			var currentOrders = _orderRepository.GetCurrentOrders(InfoProvider.UoW, _counterparty);
			ytreeCurrentOrders.SetItemsSource<Order>(currentOrders);
			vboxCurrentOrders.Visible = currentOrders.Count > 0;

			foreach(var child in PhonesTable.Children)
			{
				PhonesTable.Remove(child);
				child.Destroy();
			}

			uint rowsCount = Convert.ToUInt32(_counterparty.Phones.Count) + 1;
			PhonesTable.Resize(rowsCount, 2);
			for(uint row = 0; row < rowsCount - 1; row++)
			{
				Label label = new Label();
				label.Selectable = true;
				label.Markup = $"{_counterparty.Phones[Convert.ToInt32(row)].LongText}";

				HandsetView handsetView = new HandsetView(_counterparty.Phones[Convert.ToInt32(row)].DigitsNumber);

				PhonesTable.Attach(label, 0, 1, row, row + 1);
				PhonesTable.Attach(handsetView, 1, 2, row, row + 1);
			}

			Label labelAddPhone = new Label() { LabelProp = "Щёлкните чтобы\n добавить телефон-->" };
			PhonesTable.Attach(labelAddPhone, 0, 1, rowsCount - 1, rowsCount);

			Image addIcon = new Image();
			addIcon.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-add", IconSize.Menu);
			Button btn = new Button();
			btn.Image = addIcon;
			btn.Clicked += OnBtnAddPhoneClicked;
			PhonesTable.Attach(btn, 1, 2, rowsCount - 1, rowsCount);
			PhonesTable.ShowAll();
			btn.Sensitive = buttonSaveComment.Sensitive = _counterparty.Id != 0;
		}

		private void SetupPersonalManagers()
		{
			if(_counterparty?.SalesManager != null)
			{
				labelSalesManager.Visible = true;
				prefixSalesManager.Visible = true;
				labelSalesManager.Markup = _counterparty?.SalesManager.GetPersonNameWithInitials();
				prefixSalesManager.ModifyFont(FontDescription.FromString("9"));
				prefixSalesManager.QueueResize();
			}
			else
			{
				labelSalesManager.Visible = false;
				prefixSalesManager.Visible = false;
			}

			if(_counterparty?.Accountant != null)
			{
				labelAccountant.Visible = true;
				prefixAccountant.Visible = true;
				labelAccountant.Markup = _counterparty?.Accountant.GetPersonNameWithInitials();
			}
			else
			{
				labelAccountant.Visible = false;
				prefixAccountant.Visible = false;
			}

			if(_counterparty?.BottlesManager != null)
			{
				labelBottlesManager.Visible = true;
				prefixBottlesManager.Visible = true;
				labelBottlesManager.Markup = _counterparty?.BottlesManager.GetPersonNameWithInitials();
				prefixBottlesManager.ModifyFont(FontDescription.FromString("9"));
				prefixBottlesManager.QueueResize();
			}
			else
			{
				labelBottlesManager.Visible = false;
				prefixBottlesManager.Visible = false;
			}
		}

		public bool VisibleOnPanel => _counterparty != null;

		public void OnCurrentObjectChanged(object changedObject)
		{
			if(changedObject is Counterparty)
			{
				Refresh();
			}
		}

		protected void OnButtonSaveCommentClicked(object sender, EventArgs e)
		{
			using(var uow =
				UnitOfWorkFactory.CreateForRoot<Counterparty>(_counterparty.Id, "Кнопка «Cохранить комментарий» на панели контрагента"))
			{
				uow.Root.Comment = textviewComment.Buffer.Text;
				uow.Save();
			}
		}

		protected void OnBtnAddPhoneClicked(object sender, EventArgs e)
		{
			TDIMain.MainNotebook.OpenTab(
				DialogHelper.GenerateDialogHashName<Counterparty>(_counterparty.Id),
				() =>
				{
					var dlg = new CounterpartyDlg(EntityUoWBuilder.ForOpen(_counterparty.Id), UnitOfWorkFactory.GetDefaultFactory);
					dlg.ActivateContactsTab();
					dlg.EntitySaved += (o, args) => Refresh(args.Entity);
					return dlg;
				}
			);
		}

		#endregion
	}
}
