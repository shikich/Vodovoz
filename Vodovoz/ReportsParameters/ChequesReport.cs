﻿using System;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Report;
using QSReport;
using Vodovoz.Domain.Sale;
using QS.Dialog.GtkUI;

namespace Vodovoz.ReportsParameters
{
	public partial class ChequesReport : SingleUoWWidgetBase, IParametersWidget
	{
		GenericObservableList<GeographicGroup> geographicGroups;

		public ChequesReport()
		{
			this.Build();
			ydatepicker.Date = DateTime.Now.Date;
			ConfigureDlg();
		}

		void ConfigureDlg()
		{
			UoW = UnitOfWorkFactory.CreateWithoutRoot();
			geograficGroup.UoW = UoW;
			geograficGroup.Label = "Часть города:";
			geographicGroups = new GenericObservableList<GeographicGroup>();
			geograficGroup.Items = geographicGroups;
			foreach(var gg in UoW.Session.QueryOver<GeographicGroup>().List())
				geographicGroups.Add(gg);
		}

		#region IOrmDialog implementation

		public IUnitOfWork UoW { get; private set; }

		#endregion

		#region IParametersWidget implementation

		public event EventHandler<LoadReportEventArgs> LoadReport;

		public string Title => "Отчет по чекам";

		#endregion

		private int[] GetResultIds(IEnumerable<int> ids)
		{
			return ids.Any() ? ids.ToArray() : new int[] { 0 };
		}

		private ReportInfo GetReportInfo()
		{
			var repInfo = new ReportInfo {
				Identifier = "Orders.ChequesReport",
				Parameters = new Dictionary<string, object>
				{
					{ "date", ydatepicker.Date },
					{ "geographic_groups", GetResultIds(geographicGroups.Select(g => g.Id)) }
				}
			};
			return repInfo;
		}

		void OnUpdate(bool hide = false)
		{
			LoadReport?.Invoke(this, new LoadReportEventArgs(GetReportInfo(), hide));
		}

		protected void OnButtonCreateRepotClicked(object sender, EventArgs e) => OnUpdate(true);
	}
}