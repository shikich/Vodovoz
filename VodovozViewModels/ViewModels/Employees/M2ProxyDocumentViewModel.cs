using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using QS.Commands;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Services;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.OrderM2Proxy;

namespace Vodovoz.ViewModels.Employees
{
    public class M2ProxyDocumentViewModel : DialogViewModelBase, IEditableDialog
    {
		public IUnitOfWorkGeneric<M2ProxyDocument> UoWGeneric { get; }
        public ICommonServices CommonServices { get; }
        public IUnitOfWork UoW => UoWGeneric;
        public M2ProxyDocument Entity => UoWGeneric.Root;
        public IList<OrderEquipment> EquipmentList { get; } = new GenericObservableList<OrderEquipment>();
        public IUnitOfWork UoWOrder { get; private set; }
        public ValidationContext ValidationContext { get; set; }
        public bool IsEditable { get; set; } = true;

		public M2ProxyDocumentViewModel(
			IEntityUoWBuilder uowBuilder, 
			IUnitOfWorkFactory unitOfWorkFactory,
			INavigationManager navigationManager,
			ICommonServices commonServices) : base(navigationManager)
		{
			CommonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
			UoWGeneric = uowBuilder.CreateUoW<M2ProxyDocument>(unitOfWorkFactory);

			if (uowBuilder.RootUoW != null)
			{
				UoWOrder = uowBuilder.RootUoW;
				Entity.Order = UoWOrder.RootObject as Order;
			}
			
			ValidationContext = new ValidationContext(Entity);
			FillTitle(uowBuilder);
			Configure();
		}

		private void FillTitle(IEntityUoWBuilder uowBuilder)
		{
			if (uowBuilder.IsNewEntity) {
				Title = "Новая доверенность М-2";
			}
			else {
				Title = "Изменение доверенности М-2";
			}
		}

		private void Configure()
		{
			if (Entity.EmployeeDocument == null && Entity.Employee != null) {
				GetDocument();
			}
			
			FillForOrder();
		}

		void GetDocument()
		{
			var doc = Entity.Employee.GetMainDocuments();
			
			if (doc.Any()) {
				Entity.EmployeeDocument = doc[0];
			}
		}

		public void FillForOrder()
		{
			Order order = Entity.Order;
			
			if(order != null) {
				EquipmentList.Clear();
				var equipmentList = Entity.Order.ObservableOrderEquipments.Where(eq => eq.Direction == Direction.PickUp);
				Entity.Date = order.DeliveryDate ?? DateTime.Now;
				Entity.ExpirationDate = Entity.Date.AddDays(10);
				Entity.Supplier = order.Counterparty;
				Entity.Organization = order.Contract.Organization;

				foreach (var item in equipmentList) {
					EquipmentList.Add(item);
				}
			} else {
				Entity.Date = DateTime.Today;
				Entity.ExpirationDate = DateTime.Today.AddDays(10);
			}
		}

		private DelegateCommand saveCommand;
		public DelegateCommand SaveCommand => saveCommand ?? (
			saveCommand = new DelegateCommand(
				() =>
				{
					if (Save()) {
						Close(false, CloseSource.Save);
					}
				},
				() => true
			)
		);
		
		private bool Save()
		{
			if (UoWOrder == null) {
				return true;
			}

			if (!Validate()) {
				return false;
			}

			if(Entity.Order != null) {
				List<OrderDocument> list = new List<OrderDocument> {
					new OrderM2Proxy {
						AttachedToOrder = Entity.Order,
						M2Proxy = Entity,
						Order = Entity.Order
					}
				};
				(UoWOrder.RootObject as Order).AddAdditionalDocuments(list);
			}

			return true;
		}
		
		private bool Validate() => CommonServices.ValidationService.Validate(Entity, ValidationContext);
    }
}