using System;
using QS.Project.Journal;
using QS.Utilities.Text;
using Vodovoz.Domain;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Sectors;

namespace Vodovoz.ViewModels.Journals.JournalNodes
{
    public class FinancialDistrictsSetsJournalNode : JournalEntityNodeBase<FinancialDistrictsSet>
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateActivated { get; set; }
        public DateTime? DateClosed { get; set; }
        public SectorsSetStatus Status { get; set; }
        public string AuthorName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorPatronymic { get; set; }
        public string Author => PersonHelper.PersonNameWithInitials(AuthorLastName, AuthorName, AuthorPatronymic);
    }
}