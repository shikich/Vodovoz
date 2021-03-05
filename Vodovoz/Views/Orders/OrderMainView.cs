using Autofac;
using QS.Navigation;
using QS.Views;
using QS.Views.Resolve;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.Views.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class OrderMainView : ViewBase<SelfDeliveryOrderMainViewModel>
    {
        public OrderMainView(SelfDeliveryOrderMainViewModel viewModel) : base(viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            notebookOrder.ShowTabs = false;
            yradBtnInfo.Active = true;

            ybtnCancel.Clicked += (sender, args) => ViewModel.Close(false, CloseSource.Cancel);

            ResolveInfoView();
            ResolveOrderDocumentsView();
            ResolveWorkingOnOrderView();
        }

        private void ResolveInfoView()
        {
            if (ViewModel.OrderInfoViewModelBase != null)
            {
                ViewModel.OrderInfoViewModelBase.AutofacScope = ViewModel.AutofacScope;
                ViewModel.OrderInfoViewModelBase.ParentTab = ViewModel;

                var infoView = ViewModel.AutofacScope.Resolve<IGtkViewResolver>()
                    .Resolve(ViewModel.OrderInfoViewModelBase);

                vboxInfo.Add(infoView);
                infoView.Show();
            }
        }
        
        private void ResolveOrderDocumentsView()
        {
            if (ViewModel.OrderDocumentsViewModel != null)
            {
                ViewModel.OrderDocumentsViewModel.ParentTab = ViewModel;

                var documentsView = ViewModel.AutofacScope.Resolve<IGtkViewResolver>()
                    .Resolve(ViewModel.OrderDocumentsViewModel);

                vboxOrderDocuments.Add(documentsView);
                documentsView.Show();
            }
        }

        private void ResolveWorkingOnOrderView()
        {
            if (ViewModel.WorkingOnOrderViewModel != null)
            {
                var workingOnOrderView = ViewModel.AutofacScope.Resolve<IGtkViewResolver>()
                    .Resolve(ViewModel.WorkingOnOrderViewModel);

                vboxWorkingOnOrder.Add(workingOnOrderView);
                workingOnOrderView.Show();
            }
        }

        protected void OnYradBtnInfoToggled(object sender, System.EventArgs e)
        {
            if (yradBtnInfo.Active)
            {
                notebookOrder.CurrentPage = 0;
            }
        }

        protected void OnYradBtnDocsToggled(object sender, System.EventArgs e)
        {
            if (yradBtnDocs.Active)
            {
                notebookOrder.CurrentPage = 1;
            }
        }

        protected void OnYradBtnWorkingOnOrderToggled(object sender, System.EventArgs e)
        {
            if (yradBtnWorkingOnOrder.Active)
            {
                notebookOrder.CurrentPage = 2;
            }
        }
    }
}
