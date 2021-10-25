using Gamma.GtkWidgets;
using QS.Views.GtkUI;
using Vodovoz.Domain.Client;
using Vodovoz.Filters.ViewModels;
using Vodovoz.Journals.Nodes.Counterparties;
using Vodovoz.ViewModels.Journals.Filters.Counterparties;

namespace Vodovoz.Filters.GtkViews
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CounterpartyFilterView : FilterViewBase<CounterpartyJournalFilterViewModel>
	{
		public CounterpartyFilterView(CounterpartyJournalFilterViewModel counterpartyJournalFilterViewModel) : base(counterpartyJournalFilterViewModel)
		{
			this.Build();
			Configure();
		}

		private void Configure()
		{
			//TODO разобраться с Tag
			yentryTag.Sensitive = false;
			//yentryTag.RepresentationModel = ViewModel.TagVM;
			//yentryTag.Binding.AddBinding(ViewModel, vm => vm.Tag, w => w.Subject).InitializeFromSource();
			yenumCounterpartyType.ItemsEnum = typeof(CounterpartyType);
			yenumCounterpartyType.Binding.AddBinding(ViewModel, vm => vm.CounterpartyType, w => w.SelectedItemOrNull).InitializeFromSource();
			checkIncludeArhive.Binding.AddBinding(ViewModel, vm => vm.RestrictIncludeArchive, w => w.Active).InitializeFromSource();

			if (ViewModel?.IsForRetail ?? false)
			{
				ytreeviewSalesChannels.ColumnsConfig = ColumnsConfigFactory.Create<SalesChannelSelectableNode>()
					.AddColumn("Название").AddTextRenderer(node => node.Name)
					.AddColumn("").AddToggleRenderer(x => x.Selected)
					.Finish();

				ytreeviewSalesChannels.ItemsDataSource = ViewModel.SalesChannels;
			} else
            {
				frame2.Visible = false;
			}
        }
	}
}
