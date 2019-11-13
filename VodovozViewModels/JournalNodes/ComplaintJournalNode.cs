﻿using System;
using Gamma.Utilities;
using QS.Project.Journal;
using Vodovoz.Domain.Complaints;

namespace Vodovoz.JournalNodes
{
	public class ComplaintJournalNode : JournalEntityNodeBase
	{
		protected ComplaintJournalNode() : base(typeof(Complaint))
		{
		}

		public override string Title => $"{TypeString} №{Id} от {DateString}";

		public ComplaintType Type { get; set; }
		public string TypeString {
			get {
				switch(Type) {
					case ComplaintType.Inner:
						return "ВН";
					case ComplaintType.Client:
						return "КЛ";
				}
				return Type.GetEnumTitle();
			}
		}

		public int SequenceNumber { get; set; }

		public DateTime Date { get; set; }
		public string DateString => Date.ToString("dd.MM.yy");

		public ComplaintStatuses Status { get; set; }
		public string StatusString => Status.GetEnumTitle();

		public string WorkInSubdivision { get; set; }

		public DateTime LastPlannedCompletionDate { get; set; }
		public string PlannedCompletionDate { get; set; }

		public string ClientNameWithAddress { get; set; }

		public string Guilties { get; set; }

		public string ComplaintText { get; set; }

		public string Author { get; set; }

		public string Fines { get; set; }

		public string ResultText { get; set; }

		public DateTime? ActualCompletionDate { get; set; }
		public string ActualCompletionDateString => ActualCompletionDate.HasValue ? ActualCompletionDate.Value.ToString("dd.MM.yy") : "-";

		public string DaysInWork {
			get {
				if(ActualCompletionDate.HasValue) {
					return (ActualCompletionDate.Value - Date).TotalDays.ToString("F0");
				}
				return "-";
			}
		}
	}
}
