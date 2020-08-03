using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Project.Journal.EntitySelector;
using QS.Services;
using Vodovoz.Domain.Complaints;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Fuel;
using Vodovoz.EntityRepositories.Fuel;
using Vodovoz.EntityRepositories.Subdivisions;
using Vodovoz.Infrastructure.Services;
using Vodovoz.Journals.JournalNodes;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Complaints;
using Vodovoz.ViewModels.Dialogs.Fuel;
using Vodovoz.ViewModels.Journals.Nodes.Cash;

namespace Vodovoz.ViewModels.Journals.Cash
{
    public class FuelDocumentsJournalViewModel : MultipleEntityJournalViewModelBase<FuelDocumentJournalNode>
    {
        public FuelDocumentsJournalViewModel(
            IUnitOfWorkFactory unitOfWorkFactory,
            ICommonServices commonServices,
            IEmployeeService employeeService,
            ISubdivisionRepository subdivisionRepository,
            IFuelRepository fuelRepository,
            IEntityAutocompleteSelectorFactory employeeSelectorFactory,
            ICounterpartyJournalFactory counterpartyJournalFactory,
            INomenclatureSelectorFactory nomenclatureSelectorFactory
            ) :
            base(unitOfWorkFactory, commonServices)
        {
            TabName = "Журнал документов по топливу";

            RegisterIncomeInvoice();
            // RegisterTransferDocument();
            // RegisterWriteoffDocument();
        }

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
		        .Left.JoinQueryOver(() => fuelIncomeInvoiceAlias.FuelIncomeInvoiceItems, () => fuelIncomeInvoiceItemAlias)
		        .SelectList(list => list
			        .SelectGroup(() => fuelIncomeInvoiceAlias.Id).WithAlias(() => resultAlias.Id)
			        .Select(() => fuelIncomeInvoiceAlias.СreationTime).WithAlias(() => resultAlias.CreationDate)
			        .Select(() => fuelIncomeInvoiceAlias.Comment).WithAlias(() => resultAlias.Comment)
			        .Select(Projections.Sum(Projections.Property(() => fuelIncomeInvoiceItemAlias.Liters))).WithAlias(() => resultAlias.Liters)
			        .Select(() => authorAlias.Name).WithAlias(() => resultAlias.AuthorName)
			        .Select(() => authorAlias.LastName).WithAlias(() => resultAlias.AuthorSurname)
			        .Select(() => authorAlias.Patronymic).WithAlias(() => resultAlias.AuthorPatronymic)

			        .Select(() => subdivisionToAlias.Name).WithAlias(() => resultAlias.SubdivisionTo)
		        )

		        .TransformUsing(Transformers.AliasToBean<FuelDocumentJournalNode<FuelIncomeInvoice>>())

	        return fuelIncomeInvoiceQuery;
        }
        
        private void RegisterIncomeInvoice()
		{
			var complaintConfig = RegisterEntity<FuelIncomeInvoice>(GetFuelIncomeQuery)
				.AddDocumentConfiguration(
					//функция диалога создания документа
					() => new FuelIncomeInvoiceViewModel(
						EntityUoWBuilder.ForCreate(),
						unitOfWorkFactory,
						employeeService,
						representationEntityPicker,
						subdivisionRepository,
						fuelRepository,
						counterpartyJournalFactory,
						services
					),
					//функция диалога открытия документа
					(ComplaintJournalNode node) => new ComplaintViewModel(
						EntityUoWBuilder.ForOpen(node.Id),
						unitOfWorkFactory,
						commonServices,
						undeliveriesViewOpener,
						employeeService,
						employeeSelectorFactory,
						counterpartySelectorFactory,
						filePickerService,
						subdivisionRepository
					),
					//функция идентификации документа 
					(ComplaintJournalNode node) => {
						return node.EntityType == typeof(Complaint);
					},
					"Клиентская жалоба",
					new JournalParametersForDocument() { HideJournalForCreateDialog = false, HideJournalForOpenDialog = true }
				);

			//завершение конфигурации
			complaintConfig.FinishConfiguration();

			fuelIncomeInvoiceConfig.AddViewModelDocumentConfiguration<FuelIncomeInvoiceViewModel>(
				//функция идентификации документа 
				(FuelDocumentVMNode node) => node.EntityType == typeof(FuelIncomeInvoice),
				//заголовок действия для создания нового документа
				"Входящая накладная",
				//функция диалога создания документа
				() => new FuelIncomeInvoiceViewModel(
					EntityUoWBuilder.ForCreate(),
					unitOfWorkFactory,
					employeeService,
					representationEntityPicker,
					subdivisionRepository,
					fuelRepository,
					services
				),
				//функция диалога открытия документа
				(node) => new FuelIncomeInvoiceViewModel(
					EntityUoWBuilder.ForOpen(node.DocumentId),
					unitOfWorkFactory,
					employeeService,
					representationEntityPicker,
					subdivisionRepository,
					fuelRepository,
					services
				)
			);

			//завершение конфигурации
			fuelIncomeInvoiceConfig.FinishConfiguration();
		}
    }
}