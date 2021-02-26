﻿using Autofac;
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

            if (ViewModel.SelfDeliveryInfoViewModel != null)
            {
                var infoView = ViewModel.AutofacScope.Resolve<IGtkViewResolver>()
                    .Resolve(ViewModel.SelfDeliveryInfoViewModel);
                
                vboxInfo.Add(infoView);
                infoView.Show();
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
