using System;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using Gamma.GtkWidgets;
using Gamma.Widgets;
using Gtk;
using NLog;
using QS.DomainModel.UoW;
using QSWidgetLib;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Contacts;
using Vodovoz.EntityRepositories;
using Vodovoz.Parameters;
namespace Vodovoz.Views.Orders
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DiscountsView : Gtk.Bin
	{
		public DiscountsView()
		{
			this.Build();

		}
	}
}
