﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Multi;
using NHibernate.Transform;
using NHibernate.Util;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.ViewModels;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Operations;
using Vodovoz.Domain.Store;
using Vodovoz.Infrastructure.Report.SelectableParametersFilter;
using Vodovoz.ViewModels.Reports;

namespace Vodovoz.ViewModels.ViewModels.Suppliers
{
	public class WarehousesBalanceSummaryViewModel : DialogTabViewModelBase
	{
		private SelectableParameterReportFilterViewModel _nomsViewModel;
		private SelectableParameterReportFilterViewModel _warsViewModel;
		private SelectableParametersReportFilter _nomsFilter;
		private SelectableParametersReportFilter _warsFilter;

		private bool _isGenerating = false;
		private BalanceSummaryReport _report;

		public WarehousesBalanceSummaryViewModel(
			IUnitOfWorkFactory unitOfWorkFactory, IInteractiveService interactiveService, INavigationManager navigation)
			: base(unitOfWorkFactory, interactiveService, navigation)
		{
			TabName = "Остатки по складам";
		}

		#region Свойства

		public DateTime? EndDate { get; set; } = DateTime.Today;

		public bool AllNomenclatures { get; set; } = true;
		public bool IsGreaterThanZeroByNomenclature { get; set; }
		public bool IsLessOrEqualZeroByNomenclature { get; set; }
		public bool IsLessThanMinByNomenclature { get; set; }
		public bool IsGreaterOrEqualThanMinByNomenclature { get; set; }

		public bool AllWarehouses { get; set; } = true;
		public bool IsGreaterThanZeroByWarehouse { get; set; }
		public bool IsLessOrEqualZeroByWarehouse { get; set; }
		public bool IsLessThanMinByWarehouse { get; set; }
		public bool IsGreaterOrEqualThanMinByWarehouse { get; set; }

		public SelectableParameterReportFilterViewModel NomsViewModel => _nomsViewModel ?? (_nomsViewModel = CreateNomsViewModel());

		public SelectableParameterReportFilterViewModel WarsViewModel => _warsViewModel ?? (_warsViewModel = CreateWarsViewModel());

		public bool IsGenerating
		{
			get => _isGenerating;
			set => SetField(ref _isGenerating, value);
		}

		public CancellationTokenSource ReportGenerationCancelationTokenSource { get; set; }

		public BalanceSummaryReport Report
		{
			get => _report;
			set => SetField(ref _report, value);
		}

		#endregion

		public void ShowWarning(string message)
		{
			ShowWarningMessage(message);
		}

		public async Task<BalanceSummaryReport> ActionGenerateReportAsync(CancellationToken cancellationToken)
		{
			var uow = UnitOfWorkFactory.CreateWithoutRoot("Отчет остатков по складам");
			try
			{
				return await GenerateAsync(EndDate ?? DateTime.Today, uow, cancellationToken);
			}
			finally
			{
				uow.Dispose();
			}
		}

		private async Task<BalanceSummaryReport> GenerateAsync(
			DateTime endDate,
			IUnitOfWork localUow,
			CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

			var nomsSet = _nomsFilter.ParameterSets.FirstOrDefault(x => x.ParameterName == "nomenclatures");
			var noms = nomsSet?.GetIncludedParameters()?.ToList();
			var typesSet = _nomsFilter.ParameterSets.FirstOrDefault(x => x.ParameterName == "nomenclature_type");
			var types = typesSet?.GetIncludedParameters()?.ToList();
			var groupsSet = _nomsFilter.ParameterSets.FirstOrDefault(x => x.ParameterName == "product_groups");
			var groups = groupsSet?.GetIncludedParameters()?.ToList();
			var wars = _warsFilter.ParameterSets
				.FirstOrDefault(ps => ps.ParameterName == "warehouses")?.GetIncludedParameters()?.ToList();

			Nomenclature nomAlias = null;

			var warsIds = wars?.Select(x => (int)x.Value).ToArray();
			var groupsIds = groups?.Select(x => (int)x.Value).ToArray();
			var groupsSelected = groups?.Any() ?? false;
			var typesSelected = types?.Any() ?? false;
			var nomsSelected = noms?.Any() ?? false;
			var allNomsSelected = noms?.Count == nomsSet?.Parameters.Count;
			if(groupsSelected)
			{
				var nomsInGroupsIds = (List<int>)await localUow.Session.QueryOver(() => nomAlias)
					.Where(Restrictions.In(Projections.Property(() => nomAlias.ProductGroup.Id), groupsIds))
					.AndNot(() => nomAlias.IsArchive)
					.Select(n => n.Id).ListAsync<int>(cancellationToken);
				if(nomsSelected)
				{
					noms = noms.Where(x => nomsInGroupsIds.Contains((int)x.Value)).ToList();
				}
				else
				{
					noms?.AddRange(nomsSet.Parameters.Where(x => nomsInGroupsIds.Contains((int)x.Value)).ToList());
				}
			}

			if(!nomsSelected && !groupsSelected)
			{
				noms?.AddRange(nomsSet.Parameters);
			}

			var nomsIds = noms?.Select(x => (int)x.Value).ToArray();

			var report = new BalanceSummaryReport
			{
				EndDate = endDate,
				WarehousesTitles = wars?.Select(x => x.Title).ToList(),
				SummaryRows = new List<BalanceSummaryRow>()
			};

			#region Запросы

			WarehouseMovementOperation inAlias = null;
			WarehouseMovementOperation woAlias = null;
			BalanceBean resultAlias = null;

			var inQuery = localUow.Session.QueryOver(() => inAlias)
				.Where(() => inAlias.OperationTime <= endDate)
				.AndNot(() => nomAlias.IsArchive)
				.Inner.JoinAlias(x => x.Nomenclature, () => nomAlias)
				.Where(Restrictions.In(Projections.Property(() => inAlias.IncomingWarehouse.Id), warsIds))
				.SelectList(list => list
					.SelectGroup(() => inAlias.IncomingWarehouse.Id).WithAlias(() => resultAlias.WarehouseId)
					.SelectGroup(() => nomAlias.Id).WithAlias(() => resultAlias.NomId)
					.Select(Projections.Sum(Projections.Property(() => inAlias.Amount))).WithAlias(() => resultAlias.Amount)
				)
				.OrderBy(() => nomAlias.Id).Asc
				.ThenBy(() => inAlias.IncomingWarehouse.Id).Asc
				.TransformUsing(Transformers.AliasToBean<BalanceBean>());

			var woQuery = localUow.Session.QueryOver(() => woAlias)
				.Where(() => woAlias.OperationTime <= endDate)
				.AndNot(() => nomAlias.IsArchive)
				.Inner.JoinAlias(x => x.Nomenclature, () => nomAlias)
				.Where(Restrictions.In(Projections.Property(() => woAlias.WriteoffWarehouse.Id), warsIds))
				.SelectList(list => list
					.SelectGroup(() => woAlias.WriteoffWarehouse.Id).WithAlias(() => resultAlias.WarehouseId)
					.SelectGroup(() => nomAlias.Id).WithAlias(() => resultAlias.NomId)
					.Select(Projections.Sum(Projections.Property(() => woAlias.Amount))).WithAlias(() => resultAlias.Amount)
				)
				.OrderBy(() => nomAlias.Id).Asc
				.ThenBy(() => woAlias.WriteoffWarehouse.Id).Asc
				.TransformUsing(Transformers.AliasToBean<BalanceBean>());

			var msQuery = localUow.Session.QueryOver(() => nomAlias)
				.WhereNot(() => nomAlias.IsArchive)
				.Select(n => n.MinStockCount)
				.OrderBy(n => n.Id).Asc;

			if(typesSelected)
			{
				var typesIds = types.Select(x => (int)x.Value).ToArray();
				inQuery.Where(Restrictions.In(Projections.Property(() => nomAlias.Category), typesIds));
				woQuery.Where(Restrictions.In(Projections.Property(() => nomAlias.Category), typesIds));
				msQuery.Where(Restrictions.In(Projections.Property(() => nomAlias.Category), typesIds));
			}

			if(nomsSelected && !allNomsSelected)
			{
				inQuery.Where(Restrictions.In(Projections.Property(() => nomAlias.Id), nomsIds));
				woQuery.Where(Restrictions.In(Projections.Property(() => nomAlias.Id), nomsIds));
				msQuery.Where(Restrictions.In(Projections.Property(() => nomAlias.Id), nomsIds));
			}

			if(groupsSelected)
			{
				inQuery.Where(Restrictions.In(Projections.Property(() => nomAlias.ProductGroup.Id), groupsIds));
				woQuery.Where(Restrictions.In(Projections.Property(() => nomAlias.ProductGroup.Id), groupsIds));
				msQuery.Where(Restrictions.In(Projections.Property(() => nomAlias.ProductGroup.Id), groupsIds));
			}

			#endregion

			var batch = localUow.Session.CreateQueryBatch()
				.Add<BalanceBean>("in", inQuery)
				.Add<BalanceBean>("wo", woQuery)
				.Add<decimal>("ms", msQuery);

			var inResult = batch.GetResult<BalanceBean>("in").ToArray();
			var woResult = batch.GetResult<BalanceBean>("wo").ToArray();
			var msResult = batch.GetResult<decimal>("ms").ToArray();

			//Кол-во списаний != кол-во начислений, используется два счетчика
			var addedCounter = 0;
			var removedCounter = 0;
			for(var nomsCounter = 0; nomsCounter < noms?.Count; nomsCounter++)
			{
				cancellationToken.ThrowIfCancellationRequested();
				var row = new BalanceSummaryRow
				{
					NomId = (int)noms[nomsCounter].Value,
					NomTitle = noms[nomsCounter].Title,
					Separate = new List<decimal>(),
					Min = msResult[nomsCounter]
				};

				for(var warsCounter = 0; warsCounter < wars?.Count; warsCounter++)
				{
					row.Separate.Add(0);
					//Т.к. данные запросов упорядочены, тут реализован доступ по индексам
					var warId = (int)wars[warsCounter].Value;
					if(addedCounter != inResult.Length)
					{
						var tempIn = inResult[addedCounter];
						if(tempIn.WarehouseId == warId && tempIn.NomId == row.NomId)
						{
							row.Separate[warsCounter] += tempIn.Amount;
							addedCounter++;
						}
					}

					if(removedCounter != woResult.Length)
					{
						var tempWo = woResult[removedCounter];
						if(tempWo.WarehouseId == warId && tempWo.NomId == row.NomId)
						{
							row.Separate[warsCounter] -= tempWo.Amount;
							removedCounter++;
						}
					}
				}

				AddRow(ref report, row);
			}

			RemoveWarehousesByFilterCondition(ref report, cancellationToken);

			cancellationToken.ThrowIfCancellationRequested();
			return await new ValueTask<BalanceSummaryReport>(report);
		}

		private void RemoveWarehousesByFilterCondition(ref BalanceSummaryReport report, CancellationToken cancellationToken)
		{
			if(AllWarehouses)
			{
				return;
			}

			for(var warCounter = 0; warCounter < report.WarehousesTitles.Count; warCounter++)
			{
				if(IsGreaterThanZeroByWarehouse && report.SummaryRows.FirstOrDefault(row => row.Separate[warCounter] > 0) == null
				|| IsLessOrEqualZeroByWarehouse && report.SummaryRows.FirstOrDefault(row => row.Separate[warCounter] <= 0) == null
				|| IsLessThanMinByWarehouse && report.SummaryRows.FirstOrDefault(row => row.Min < row.Separate[warCounter]) == null
				|| IsGreaterOrEqualThanMinByWarehouse && report.SummaryRows.FirstOrDefault(row => row.Min >= row.Separate[warCounter]) == null)
				{
					RemoveWarehouseByIndex(ref report, ref warCounter, cancellationToken);
				}
			}
		}

		private void RemoveWarehouseByIndex(ref BalanceSummaryReport report, ref int warCounter, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			report.WarehousesTitles.RemoveAt(warCounter);
			var i = warCounter;
			report.SummaryRows.ForEach(row => row.Separate.RemoveAt(i));
			warCounter--;
		}

		private void AddRow(ref BalanceSummaryReport report, BalanceSummaryRow row)
		{
			if(AllNomenclatures
			|| IsGreaterThanZeroByNomenclature && row.Separate.FirstOrDefault(war => war > 0) > 0
			|| IsLessOrEqualZeroByNomenclature && row.Separate.FirstOrDefault(war => war <= 0) <= 0
			|| IsLessThanMinByNomenclature && row.Separate.FirstOrDefault(war => war < row.Min) < row.Min
			|| IsGreaterOrEqualThanMinByNomenclature && row.Separate.FirstOrDefault(war => war >= row.Min) >= row.Min)
			{
				report.SummaryRows.Add(row);
			}
		}

		#region Настройка фильтров

		private SelectableParameterReportFilterViewModel CreateNomsViewModel()
		{
			_nomsFilter = new SelectableParametersReportFilter(UoW);
			var nomenclatureTypeParam = _nomsFilter.CreateParameterSet("Типы номенклатур", "nomenclature_type",
				new ParametersEnumFactory<NomenclatureCategory>());

			var nomenclatureParam = _nomsFilter.CreateParameterSet("Номенклатуры", "nomenclatures",
				new ParametersFactory(UoW, (filters) =>
				{
					SelectableEntityParameter<Nomenclature> resultAlias = null;
					var query = UoW.Session.QueryOver<Nomenclature>().Where(x => !x.IsArchive);
					if(filters != null && EnumerableExtensions.Any(filters))
					{
						foreach(var f in filters)
						{
							var filterCriterion = f();
							if(filterCriterion != null)
							{
								query.Where(filterCriterion);
							}
						}
					}

					query.SelectList(list => list
						.Select(x => x.Id).WithAlias(() => resultAlias.EntityId)
						.Select(x => x.OfficialName).WithAlias(() => resultAlias.EntityTitle)
					).TransformUsing(Transformers.AliasToBean<SelectableEntityParameter<Nomenclature>>());
					return query.List<SelectableParameter>();
				})
			);

			nomenclatureParam.AddFilterOnSourceSelectionChanged(nomenclatureTypeParam,
				() =>
				{
					var selectedValues = nomenclatureTypeParam.GetSelectedValues();
					return !EnumerableExtensions.Any(selectedValues)
						? null
						: nomenclatureTypeParam.FilterType == SelectableFilterType.Include
							? Restrictions.On<Nomenclature>(x => x.Category).IsIn(nomenclatureTypeParam.GetSelectedValues().ToArray())
							: Restrictions.On<Nomenclature>(x => x.Category).Not.IsIn(nomenclatureTypeParam.GetSelectedValues().ToArray());
				}
			);

			UoW.Session.QueryOver<ProductGroup>().Fetch(SelectMode.Fetch, x => x.Childs).List();

			_nomsFilter.CreateParameterSet("Группы товаров", "product_groups",
				new RecursiveParametersFactory<ProductGroup>(UoW, (filters) =>
					{
						var query = UoW.Session.QueryOver<ProductGroup>();
						if(filters != null && EnumerableExtensions.Any(filters))
						{
							foreach(var f in filters)
							{
								query.Where(f());
							}
						}

						return query.List();
					},
					x => x.Name,
					x => x.Childs)
			);

			return new SelectableParameterReportFilterViewModel(_nomsFilter);
		}

		private SelectableParameterReportFilterViewModel CreateWarsViewModel()
		{
			_warsFilter = new SelectableParametersReportFilter(UoW);

			_warsFilter.CreateParameterSet("Склады", "warehouses", new ParametersFactory(UoW, (filters) =>
			{
				SelectableEntityParameter<Warehouse> resultAlias = null;
				var query = UoW.Session.QueryOver<Warehouse>().Where(x => !x.IsArchive);
				if(filters != null && EnumerableExtensions.Any(filters))
				{
					foreach(var f in filters)
					{
						var filterCriterion = f();
						if(filterCriterion != null)
						{
							query.Where(filterCriterion);
						}
					}
				}

				query.SelectList(list => list
					.Select(x => x.Id).WithAlias(() => resultAlias.EntityId)
					.Select(x => x.Name).WithAlias(() => resultAlias.EntityTitle)
				).TransformUsing(Transformers.AliasToBean<SelectableEntityParameter<Warehouse>>());
				return query.List<SelectableParameter>();
			}));

			return new SelectableParameterReportFilterViewModel(_warsFilter);
		}

		#endregion
	}

	public class BalanceSummaryRow
	{
		public int NomId { get; set; }
		public string NomTitle { get; set; }
		public decimal Min { get; set; }
		public decimal Common => Separate.Sum();
		public decimal Diff => Common - Min;
		public List<decimal> Separate { get; set; }
	}

	public class BalanceSummaryReport
	{
		public DateTime EndDate { get; set; }
		public List<string> WarehousesTitles { get; set; }
		public List<BalanceSummaryRow> SummaryRows { get; set; }
	}

	public class BalanceBean
	{
		public int NomId { get; set; }
		public int WarehouseId { get; set; }
		public decimal Amount { get; set; }
	}
}
