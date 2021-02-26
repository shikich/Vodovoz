using QS.Views.Dialog;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.Views.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class AddExistingDocumentsView : DialogViewBase<AddExistingDocumentsViewModel>
    {
        public AddExistingDocumentsView(
            AddExistingDocumentsViewModel viewModel) : base(viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            ybtnAddSelectedDocs.Clicked += (sender, e) => ViewModel.AddSelectedDocsCommand.Execute();
            
            baseOrdersDocumentsView.ViewModel = ViewModel.OrdersDocumentsViewModel;
            counterpartyDocumentsView.ViewModel = ViewModel.CounterpartyDocumentsViewModel;
        }
    }
}
