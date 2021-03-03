using System;
using QS.ViewModels;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrderInfoExpandedPanelViewModel : UoWWidgetViewModelBase
    {
        public bool IsExpanded { get; set; } = true;
        public event EventHandler ExpandeEvent;

        public void Expande()
        {
            ExpandeEvent?.Invoke(null, EventArgs.Empty);
        }
    }
}
