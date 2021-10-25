using System;
using Autofac;
using Vodovoz.Additions;
using QSProjectsLib;

namespace Vodovoz.ServiceDialogs
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ResendEmailsDialog : QS.Dialog.Gtk.TdiTabBase
	{
		private ILifetimeScope _scope;
		
		public ResendEmailsDialog(ILifetimeScope scope)
		{
			_scope = scope;
			this.Build();
			datepicker1.Date = DateTime.Now;
			buttonSendErrorSendedEmails.Clicked += ButtonSendErrorSendedEmails_Clicked;
		}

		void ButtonSendErrorSendedEmails_Clicked(object sender, EventArgs e)
		{
			ManualEmailSender emailSender = new ManualEmailSender(_scope);
			emailSender.ResendEmailWithErrorSendingStatus(datepicker1.Date);
			MessageDialogWorks.RunInfoDialog("Done");
		}
	}
}
