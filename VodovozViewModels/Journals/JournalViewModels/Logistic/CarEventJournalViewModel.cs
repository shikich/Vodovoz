using NHibernate;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Project.DB;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Services;
using System;
using Autofac;
using NHibernate.Criterion;
using NHibernate.Dialect.Function;
using QS.Navigation;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Sale;
using Vodovoz.ViewModels.Journals.FilterViewModels.Logistic;
using Vodovoz.ViewModels.Journals.JournalNodes.Logistic;
using Vodovoz.ViewModels.ViewModels.Logistic;

namespace Vodovoz.ViewModels.Journals.JournalViewModels.Logistic
{
	public class CarEventJournalViewModel : FilterableSingleEntityJournalViewModelBase<CarEvent, CarEventViewModel, CarEventJournalNode,
		CarEventFilterViewModel>
	{
		public CarEventJournalViewModel(
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			ILifetimeScope scope,
			INavigationManager navigationManager = null)
			: base(unitOfWorkFactory, commonServices, null, scope, navigationManager)
		{
			TabName = "Журнал событий ТС";

			UpdateOnChanges(
				typeof(CarEvent),
				typeof(CarEventType));
		}

		protected override Func<IUnitOfWork, IQueryOver<CarEvent>> ItemsSourceQueryFunction => (uow) =>
		{
			CarEvent carEventAlias = null;
			CarEventType carEventTypeAlias = null;
			Employee authorAlias = null;
			Employee driverAlias = null;
			Car carAlias = null;
			GeographicGroup geographicGroupAlias = null;
			CarEventJournalNode resultAlias = null;

			var authorProjection = Projections.SqlFunction(
				new SQLFunctionTemplate(NHibernateUtil.String, "GET_PERSON_NAME_WITH_INITIALS(?1, ?2, ?3)"),
				NHibernateUtil.String,
				Projections.Property(() => authorAlias.LastName),
				Projections.Property(() => authorAlias.Name),
				Projections.Property(() => authorAlias.Patronymic)
			);

			var driverProjection = Projections.SqlFunction(
				new SQLFunctionTemplate(NHibernateUtil.String, "GET_PERSON_NAME_WITH_INITIALS(?1, ?2, ?3)"),
				NHibernateUtil.String,
				Projections.Property(() => driverAlias.LastName),
				Projections.Property(() => driverAlias.Name),
				Projections.Property(() => driverAlias.Patronymic)
			);

			var geographicGroupsProjection = CustomProjections.GroupConcat(
				() => geographicGroupAlias.Name,
				orderByExpression: () => geographicGroupAlias.Name, separator: ", "
			);

			var itemsQuery = uow.Session.QueryOver(() => carEventAlias);
			itemsQuery.Left.JoinAlias(x => x.CarEventType, () => carEventTypeAlias);
			itemsQuery.Left.JoinAlias(x => x.Author, () => authorAlias);
			itemsQuery.Left.JoinAlias(x => x.Driver, () => driverAlias);
			itemsQuery.Left.JoinAlias(x => x.Car, () => carAlias);
			itemsQuery.Left.JoinAlias(() => carAlias.GeographicGroups, () => geographicGroupAlias);

			if(FilterViewModel.CarEventType != null)
			{
				itemsQuery.Where(x => x.CarEventType == FilterViewModel.CarEventType);
			}

			if(FilterViewModel.CreateEventDateFrom != null && FilterViewModel.CreateEventDateTo != null)
			{
				itemsQuery.Where(x => x.CreateDate >= FilterViewModel.CreateEventDateFrom.Value.Date.Add(new TimeSpan(0, 0, 0, 0)) &&
									  x.CreateDate <= FilterViewModel.CreateEventDateTo.Value.Date.Add(new TimeSpan(0, 23, 59, 59)));
			}

			if(FilterViewModel.StartEventDateFrom != null && FilterViewModel.StartEventDateTo != null)
			{
				itemsQuery.Where(x => x.StartDate >= FilterViewModel.StartEventDateFrom &&
									  x.StartDate <= FilterViewModel.StartEventDateTo);
			}

			if(FilterViewModel.EndEventDateFrom != null && FilterViewModel.EndEventDateTo != null)
			{
				itemsQuery.Where(x => x.EndDate >= FilterViewModel.EndEventDateFrom &&
									  x.EndDate <= FilterViewModel.EndEventDateTo);
			}

			if(FilterViewModel.Author != null)
			{
				itemsQuery.Where(x => x.Author == FilterViewModel.Author);
			}

			if(FilterViewModel.Car != null)
			{
				itemsQuery.Where(x => x.Car == FilterViewModel.Car);
			}

			if(FilterViewModel.Driver != null)
			{
				itemsQuery.Where(x => x.Driver == FilterViewModel.Driver);
			}

			itemsQuery.Where(GetSearchCriterion(
				() => carEventAlias.Id,
				() => carEventAlias.Comment,
				() => carEventTypeAlias.Name,
				() => carEventTypeAlias.ShortName,
				() => carAlias.Model,
				() => carAlias.RegistrationNumber,
				() => driverProjection)
			);

			itemsQuery
				.SelectList(list => list
					.SelectGroup(() => carEventAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => carEventAlias.CreateDate).WithAlias(() => resultAlias.CreateDate)
					.Select(() => carEventAlias.StartDate).WithAlias(() => resultAlias.StartDate)
					.Select(() => carEventAlias.EndDate).WithAlias(() => resultAlias.EndDate)
					.Select(() => carEventAlias.Comment).WithAlias(() => resultAlias.Comment)
					.Select(() => carEventTypeAlias.Name).WithAlias(() => resultAlias.CarEventTypeName)
					.Select(() => carAlias.RegistrationNumber).WithAlias(() => resultAlias.CarRegistrationNumber)
					.Select(() => carAlias.OrderNumber).WithAlias(() => resultAlias.CarOrderNumber)
					.Select(() => carAlias.TypeOfUse).WithAlias(() => resultAlias.CarTypeOfUse)
					.Select(() => carAlias.IsRaskat).WithAlias(() => resultAlias.IsRaskat)
					.Select(() => carAlias.RaskatType).WithAlias(() => resultAlias.CarRaskatType)
					.Select(authorProjection).WithAlias(() => resultAlias.AuthorFullName)
					.Select(driverProjection).WithAlias(() => resultAlias.DriverFullName)
					.Select(geographicGroupsProjection).WithAlias(() => resultAlias.GeographicGroups)
				)
				.TransformUsing(Transformers.AliasToBean<CarEventJournalNode>());

			return itemsQuery;
		};

		protected override Func<CarEventViewModel> CreateDialogFunction =>
			() =>
			{
				var scope = Scope.BeginLifetimeScope();
				return scope.Resolve<CarEventViewModel>(
					new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForCreate()));
			};

		protected override Func<CarEventJournalNode, CarEventViewModel> OpenDialogFunction =>
			node =>
			{
				var scope = Scope.BeginLifetimeScope();
				return scope.Resolve<CarEventViewModel>(
					new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForOpen(node.Id)));
			};
	}
}
