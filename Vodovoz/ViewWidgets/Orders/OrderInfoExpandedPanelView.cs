using System;
using QS.Views.GtkUI;
using Vodovoz.ViewModels.Dialogs.Orders;
using Gtk;

namespace Vodovoz.ViewWidgets.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class OrderInfoExpandedPanelView : WidgetViewBase<OrderInfoExpandedPanelViewModel>
    {
        public OrderInfoExpandedPanelView()
        {
            this.Build();
        }

        protected override void ConfigureWidget()
        {
            ybtnExpander.Clicked += YbtnExpanderOnClicked;
            ViewModel.ExpandeEvent += YbtnExpanderOnClicked;
        }

        private void YbtnExpanderOnClicked(object sender, EventArgs e)
        {
            hboxPanel.Visible = ViewModel.IsExpanded = !hboxPanel.Visible;
            ybtnExpander.Label = hboxPanel.Visible ? "<<" : ">>";
        }

        public void AddPanel(Widget panel)
        {
            hboxPanel.Add(panel);
            panel.Show();
        }
    }
}
