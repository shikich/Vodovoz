using System;
using Autofac;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Project.Journal.DataLoader;
using QS.Services;
using Vodovoz.Domain.Cash;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Fuel;
using Vodovoz.EntityRepositories.Fuel;
using Vodovoz.ViewModels.Dialogs.Fuel;
using Vodovoz.ViewModels.Journals.Nodes.Cash;

namespace Vodovoz.ViewModels.Journals.JournalViewModels.Cash
{
    public class FuelDocumentsJournalViewModel : MultipleEntityJournalViewModelBase<FuelDocumentJournalNode>
    {
	    private readonly IFuelRepository fuelRepository;

		//TODO filechooserProvider new Vodovoz.FileChooser("Категория Расхода.csv")
	    public FuelDocumentsJournalViewModel(
            IUnitOfWorkFactory unitOfWorkFactory,
            ICommonServices commonServices,
            IFuelRepository fuelRepository,
			ILifetimeScope scope,
			INavigationManager navigationManager = null
            ) : base(unitOfWorkFactory, commonServices, navigationManager, scope)
        {
	        this.fuelRepository = fuelRepository ?? throw new ArgumentNullException(nameof(fuelRepository));

			TabName = "Журнал учета топлива";

			var loader = new ThreadDataLoader<FuelDocumentJournalNode>(unitOfWorkFactory);
			loader.MergeInOrderBy(x => x.CreationDate, true);
			DataLoader = loader;
		        
            RegisterIncomeInvoice();
            RegisterTransferDocument();
            RegisterWriteoffDocument();
            
            FinishJournalConfiguration();
            
            UpdateOnChanges(
            	typeof(FuelIncomeInvoice),
            	typeof(FuelIncomeInvoiceItem),
            	typeof(FuelTransferDocument),
            	typeof(FuelWriteoffDocument),
            	typeof(FuelWriteoffDocumentItem)
            );
        }

	    public override string FooterInfo {
		    get {
			    var balance = fuelRepository.GetAllFuelsBalance(UoW);
			    string result = "";
			    foreach (var item in balance) {
				    result += $"{item.Key.Name}: {item.Value.ToString("0")} л., ";
			    }
			    result.Trim(' ', ',');
			    return result;
		    }
	    }
	    
	    #region IncomeInvoice

	    private IQueryOver<FuelIncomeInvoice> GetFuelIncomeQuery(IUnitOfWork uow)
	    {
		    FuelDocumentJournalNode resultAlias = null;
		    FuelIncomeInvoice fuelIncomeInvoiceAlias = null;
		    FuelIncomeInvoiceItem fuelIncomeInvoiceItemAlias = null;
		    Employee authorAlias = null;
		    Subdivision subdivisionToAlias = null;
		    var fuelIncomeInvoiceQuery = uow.Session.QueryOver<FuelIncomeInvoice>(() => fuelIncomeInvoiceAlias)
			    .Left.JoinQueryOver(() => fuelIncomeInvoiceAlias.Author, () => authorAlias)
			    .Left.JoinQueryOver(() => fuelIncomeInvoiceAlias.Subdivision, () => subdivisionToAlias)
			    .Left.JoinQueryOver(() => fuelIncomeInvoiceAlias.FuelIncomeInvoiceItems,
				    () => fuelIncomeInvoiceItemAlias)
			    .SelectList(list => list
				    .SelectGroup(() => fuelIncomeInvoiceAlias.Id).WithAlias(() => resultAlias.Id)
				    .Select(() => fuelIncomeInvoiceAlias.СreationTime).WithAlias(() => resultAlias.CreationDate)
				    .Select(() => fuelIncomeInvoiceAlias.Comment).WithAlias(() => resultAlias.Comment)
				    .Select(Projections.Sum(Projections.Property(() => fuelIncomeInvoiceItemAlias.Liters)))
				    .WithAlias(() => resultAlias.Liters)
				    .Select(() => authorAlias.Name).WithAlias(() => resultAlias.AuthorName)
				    .Select(() => authorAlias.LastName).WithAlias(() => resultAlias.AuthorSurname)
				    .Select(() => authorAlias.Patronymic).WithAlias(() => resultAlias.AuthorPatronymic)

				    .Select(() => subdivisionToAlias.Name).WithAlias(() => resultAlias.SubdivisionTo)
			    )
			    .OrderBy(() => fuelIncomeInvoiceAlias.СreationTime).Desc()
			    .TransformUsing(Transformers.AliasToBean<FuelDocumentJournalNode<FuelIncomeInvoice>>());

		    fuelIncomeInvoiceQuery.Where(GetSearchCriterion(
			    () => authorAlias.Name,
			    () => authorAlias.LastName,
			    () => authorAlias.Patronymic,
			    () => fuelIncomeInvoiceAlias.Comment
			));

		    return fuelIncomeInvoiceQuery;
	    }
        
	    private void RegisterIncomeInvoice()
	    {
		    var complaintConfig = RegisterEntity<FuelIncomeInvoice>(GetFuelIncomeQuery)
			    .AddDocumentConfiguration(
				    //функция диалога создания документа
				    () =>
					{
						var scope = Scope.BeginLifetimeScope();
						return scope.Resolve<FuelIncomeInvoiceViewModel>(
							new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForCreate()));
						
						/*return new FuelIncomeInvoiceViewModel(
							EntityUoWBuilder.ForCreate(),
							UnitOfWorkFactory,
							employeeService,
							nomenclatureSelectorFactory,
							subdivisionRepository,
							fuelRepository,
							counterpartyJournalFactory,
							commonServices
						);*/
					},
				    //функция диалога открытия документа
				    (FuelDocumentJournalNode node) =>
					{
						var scope = Scope.BeginLifetimeScope();
						return scope.Resolve<FuelIncomeInvoiceViewModel>(
							new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForOpen(node.Id)));
						
						/*return new FuelIncomeInvoiceViewModel(
							EntityUoWBuilder.ForOpen(node.Id),
							UnitOfWorkFactory,
							employeeService,
							nomenclatureSelectorFactory,
							subdivisionRepository,
							fuelRepository,
							counterpartyJournalFactory,
							commonServices
						);*/
					},
				    //функция идентификации документа 
				    (FuelDocumentJournalNode node) => {
					    return node.EntityType == typeof(FuelIncomeInvoice);
				    },
				    "Входящая накладная"
			    );

		    //завершение конфигурации
		    complaintConfig.FinishConfiguration();
	    }

	    #endregion IncomeInvoice

	    #region TransferDocument

	    private IQueryOver<FuelTransferDocument> GetTransferDocumentQuery(IUnitOfWork uow)
	    {
		    FuelDocumentJournalNode resultAlias = null;
		    FuelTransferDocument fuelTransferAlias = null;
		    Employee authorAlias = null;
		    Subdivision subdivisionFromAlias = null;
		    Subdivision subdivisionToAlias = null;
		    var fuelTransferQuery = uow.Session.QueryOver<FuelTransferDocument>(() => fuelTransferAlias)
			    .Left.JoinQueryOver(() => fuelTransferAlias.Author, () => authorAlias)
			    .Left.JoinQueryOver(() => fuelTransferAlias.CashSubdivisionFrom, () => subdivisionFromAlias)
			    .Left.JoinQueryOver(() => fuelTransferAlias.CashSubdivisionTo, () => subdivisionToAlias)
			    .SelectList(list => list
				    .Select(() => fuelTransferAlias.Id).WithAlias(() => resultAlias.Id)
				    .Select(() => fuelTransferAlias.CreationTime).WithAlias(() => resultAlias.CreationDate)
				    .Select(() => fuelTransferAlias.Status).WithAlias(() => resultAlias.TransferDocumentStatus)
				    .Select(() => fuelTransferAlias.TransferedLiters).WithAlias(() => resultAlias.Liters)
				    .Select(() => fuelTransferAlias.Comment).WithAlias(() => resultAlias.Comment)
				    .Select(() => fuelTransferAlias.SendTime).WithAlias(() => resultAlias.SendTime)
				    .Select(() => fuelTransferAlias.ReceiveTime).WithAlias(() => resultAlias.ReceiveTime)

				    .Select(() => authorAlias.Name).WithAlias(() => resultAlias.AuthorName)
				    .Select(() => authorAlias.LastName).WithAlias(() => resultAlias.AuthorSurname)
				    .Select(() => authorAlias.Patronymic).WithAlias(() => resultAlias.AuthorPatronymic)

				    .Select(() => subdivisionFromAlias.Name).WithAlias(() => resultAlias.SubdivisionFrom)
				    .Select(() => subdivisionToAlias.Name).WithAlias(() => resultAlias.SubdivisionTo)
			    )
			    .OrderBy(() => fuelTransferAlias.CreationTime).Desc()
			    .TransformUsing(Transformers.AliasToBean<FuelDocumentJournalNode<FuelTransferDocument>>());

		    fuelTransferQuery.Where(GetSearchCriterion(
			    () => authorAlias.Name,
			    () => authorAlias.LastName,
			    () => authorAlias.Patronymic,
			    () => fuelTransferAlias.Comment
		    ));
		    
		    return fuelTransferQuery;
	    }
        
	    private void RegisterTransferDocument()
	    {
		    var complaintConfig = RegisterEntity<FuelTransferDocument>(GetTransferDocumentQuery)
			    .AddDocumentConfiguration(
				    //функция диалога создания документа
				    () =>
					{
						var scope = Scope.BeginLifetimeScope();
						return scope.Resolve<FuelTransferDocumentViewModel>(
							new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForCreate()));
					},
				    //функция диалога открытия документа
				    (FuelDocumentJournalNode node) =>
					{
						var scope = Scope.BeginLifetimeScope();
						return scope.Resolve<FuelTransferDocumentViewModel>(
							new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForOpen(node.Id)));
					},
				    //функция идентификации документа 
				    (FuelDocumentJournalNode node) => {
					    return node.EntityType == typeof(FuelTransferDocument);
				    },
				    "Перемещение"
			    );

		    //завершение конфигурации
		    complaintConfig.FinishConfiguration();
	    }

	    #endregion TransferDocument

	    #region WriteoffDocument

	    private IQueryOver<FuelWriteoffDocument> GetWriteoffDocumentQuery(IUnitOfWork uow)
	    {
		    FuelDocumentJournalNode resultAlias = null;
		    FuelWriteoffDocument fuelWriteoffAlias = null;
			Employee cashierAlias = null;
			Employee employeeAlias = null;
			Subdivision subdivisionAlias = null;
			ExpenseCategory expenseCategoryAlias = null;
			FuelWriteoffDocumentItem fuelWriteoffItemAlias = null;
			var fuelWriteoffQuery = uow.Session.QueryOver<FuelWriteoffDocument>(() => fuelWriteoffAlias)
				.Left.JoinQueryOver(() => fuelWriteoffAlias.Cashier, () => cashierAlias)
				.Left.JoinQueryOver(() => fuelWriteoffAlias.Employee, () => employeeAlias)
				.Left.JoinQueryOver(() => fuelWriteoffAlias.CashSubdivision, () => subdivisionAlias)
				.Left.JoinQueryOver(() => fuelWriteoffAlias.ExpenseCategory, () => expenseCategoryAlias)
				.Left.JoinQueryOver(() => fuelWriteoffAlias.FuelWriteoffDocumentItems, () => fuelWriteoffItemAlias)
				.SelectList(list => list
					.SelectGroup(() => fuelWriteoffAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => fuelWriteoffAlias.Date).WithAlias(() => resultAlias.CreationDate)
					.Select(() => fuelWriteoffAlias.Reason).WithAlias(() => resultAlias.Comment)

					.Select(() => cashierAlias.Name).WithAlias(() => resultAlias.AuthorName)
					.Select(() => cashierAlias.LastName).WithAlias(() => resultAlias.AuthorSurname)
					.Select(() => cashierAlias.Patronymic).WithAlias(() => resultAlias.AuthorPatronymic)

					.Select(() => employeeAlias.Name).WithAlias(() => resultAlias.EmployeeName)
					.Select(() => employeeAlias.LastName).WithAlias(() => resultAlias.EmployeeSurname)
					.Select(() => employeeAlias.Patronymic).WithAlias(() => resultAlias.EmployeePatronymic)

					.Select(() => expenseCategoryAlias.Name).WithAlias(() => resultAlias.ExpenseCategory)
					.Select(Projections.Sum(Projections.Property(() => fuelWriteoffItemAlias.Liters))).WithAlias(() => resultAlias.Liters)

					.Select(() => subdivisionAlias.Name).WithAlias(() => resultAlias.SubdivisionFrom)
				)
				.OrderBy(() => fuelWriteoffAlias.Date).Desc()
				.TransformUsing(Transformers.AliasToBean<FuelDocumentJournalNode<FuelWriteoffDocument>>());

			fuelWriteoffQuery.Where(GetSearchCriterion(
				() => cashierAlias.Name,
				() => cashierAlias.LastName,
				() => cashierAlias.Patronymic,
				() => employeeAlias.Name,
				() => employeeAlias.LastName,
				() => employeeAlias.Patronymic,
				() => fuelWriteoffAlias.Reason
			));
			
		    return fuelWriteoffQuery;
	    }
        
	    private void RegisterWriteoffDocument()
	    {
		    var complaintConfig = RegisterEntity<FuelWriteoffDocument>(GetWriteoffDocumentQuery)
			    .AddDocumentConfiguration(
				    //функция диалога создания документа
				    () =>
					{
						var scope = Scope.BeginLifetimeScope();
						return scope.Resolve<FuelIncomeInvoiceViewModel>(
							new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForCreate()));
						
						/*return new FuelWriteoffDocumentViewModel(
							EntityUoWBuilder.ForCreate(),
							UnitOfWorkFactory,
							employeeService,
							fuelRepository,
							subdivisionRepository,
							commonServices,
							employeeJournalFactory,
							reportViewOpener,
							fileChooserProvider,
							expenseCategoryJournalFilterViewModel,
							_subdivisionJournalFactory
						);*/
					},
				    //функция диалога открытия документа
				    (FuelDocumentJournalNode node) =>
					{
						var scope = Scope.BeginLifetimeScope();
						return scope.Resolve<FuelIncomeInvoiceViewModel>(
							new TypedParameter(typeof(IEntityUoWBuilder), EntityUoWBuilder.ForOpen(node.Id)));
						
						/*return new FuelWriteoffDocumentViewModel(
							EntityUoWBuilder.ForOpen(node.Id),
							UnitOfWorkFactory,
							employeeService,
							fuelRepository,
							subdivisionRepository,
							commonServices,
							employeeJournalFactory,
							reportViewOpener,
							fileChooserProvider,
							expenseCategoryJournalFilterViewModel,
							_subdivisionJournalFactory
						);*/
					},
				    //функция идентификации документа 
				    (FuelDocumentJournalNode node) => {
					    return node.EntityType == typeof(FuelWriteoffDocument);
				    },
				    "Акт выдачи топлива"
			    );

		    //завершение конфигурации
		    complaintConfig.FinishConfiguration();
	    }

	    #endregion WriteoffDocument
    }
}
