﻿using QS.Dialog.Gtk;
using QS.DomainModel.UoW;
using QS.Validation;
using Vodovoz.Domain.Client;

namespace Vodovoz.Dialogs.Client
{
	public partial class DeliveryPointCategoryDlg : EntityDialogBase<DeliveryPointCategory>
	{
		public DeliveryPointCategoryDlg()
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<DeliveryPointCategory>();
			ConfigureDlg();
		}

		public DeliveryPointCategoryDlg(DeliveryPointCategory sub) : this(sub.Id) { }

		public DeliveryPointCategoryDlg(int id)
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<DeliveryPointCategory>(id);
			ConfigureDlg();
		}

		void ConfigureDlg()
		{
			SetAccessibility();
			entName.Binding.AddBinding(Entity, e => e.Name, w => w.Text).InitializeFromSource();
			chkIsArchive.Binding.AddBinding(Entity, e => e.IsArchive, w => w.Active).InitializeFromSource();
		}

		void SetAccessibility()
		{
			chkIsArchive.Visible = Entity.Id > 0;
			entName.Sensitive = chkIsArchive.Sensitive = !Entity.IsArchive;
		}

		public override bool Save()
		{
			var valid = new QSValidator<DeliveryPointCategory>(Entity);
			if(valid.RunDlgIfNotValid((Gtk.Window)this.Toplevel))
				return false;
			UoWGeneric.Save();
			return true;
		}
	}
}