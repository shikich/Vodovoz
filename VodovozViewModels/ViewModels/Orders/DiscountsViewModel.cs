using System;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using QS.Commands;
using QS.DomainModel.UoW;
using QS.ViewModels;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Contacts;
using Vodovoz.Parameters;
using Vodovoz.EntityRepositories;
using Vodovoz.Services;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.ViewModels.Contacts
{
	public class DiscountsViewModel : WidgetViewModelBase
	{
		#region Properties

		private GenericObservableList<Discount> discountsList;
		public virtual GenericObservableList<Discount> DiscountsList
		{
			get => discountsList;
			set
			{
				SetField(ref discountsList, value, () => DiscountsList);
			}
		}

		private bool readOnly = false;
		public virtual bool ReadOnly
		{
			get => readOnly;
			set => SetField(ref readOnly, value, () => ReadOnly);
		}

		public DiscountsViewModel()
		{
			CreateCommands();
		}

		#endregion Properties

		#region Commands
		public DelegateCommand AddItemCommand { get; private set; }

		private void CreateCommands()
		{
			AddItemCommand = new DelegateCommand(
				() => {
					var discount = new Discount().Init();
					if(DiscountsList == null)
						DiscountsList = new GenericObservableList<Discount>();
					DiscountsList.Add(discount);
				},
				() => { return !ReadOnly; }
			);

		}

		#endregion Commands


	}
}
