﻿using System;
using QSOrmProject;
using Vodovoz.Domain.Logistic;
using QSValidation;

namespace Vodovoz
{
	public partial class FuelTypeDlg : OrmGtkDialogBase<FuelType>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		public FuelTypeDlg ()
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<FuelType>();
			ConfigureDlg ();
		}

		public FuelTypeDlg (int id)
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<FuelType> (id);
			ConfigureDlg ();
		}

		public FuelTypeDlg (FuelType sub) : this (sub.Id) {}

		private void ConfigureDlg ()
		{
			yentryName.Binding.AddBinding 	  (Entity, e => e.Name, w => w.Text).InitializeFromSource();
			yspinbuttonCost.Binding.AddBinding(Entity, e => e.Cost, w => w.Value).InitializeFromSource();
		}

		public override bool Save ()
		{
			var valid = new QSValidator<FuelType> (UoWGeneric.Root);
			if (valid.RunDlgIfNotValid ((Gtk.Window)this.Toplevel))
				return false;

			logger.Info ("Сохраняем график доставки...");
			UoWGeneric.Save();
			return true;
		}
	}
}

