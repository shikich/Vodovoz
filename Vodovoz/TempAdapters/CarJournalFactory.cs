using QS.DomainModel.UoW;
using QS.Project.Journal;
using QS.Project.Services;
using Vodovoz.Filters.ViewModels;
using Vodovoz.JournalViewModels;

namespace Vodovoz.TempAdapters
{
    public class CarJournalFactory : ICarJournalFactory
    {
        public JournalViewModelBase CreateDefaultCarsJournal()
        {
            CarJournalFilterViewModel filter = new CarJournalFilterViewModel();
            var carJournal = new CarJournalViewModel(filter, UnitOfWorkFactory.GetDefaultFactory, ServicesConfig.CommonServices);
            return carJournal;
        }
    }
}