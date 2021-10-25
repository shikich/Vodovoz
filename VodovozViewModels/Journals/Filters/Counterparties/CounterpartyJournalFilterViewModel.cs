using System;
using System.ComponentModel;
using System.Data.Bindings.Collections.Generic;
using NHibernate.Transform;
using QS.Project.Filter;
using QS.Project.Journal;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Retail;
using Vodovoz.Journals.Nodes.Counterparties;

namespace Vodovoz.ViewModels.Journals.Filters.Counterparties
{
	public class CounterpartyJournalFilterViewModel : FilterViewModelBase<CounterpartyJournalFilterViewModel>, IJournalFilter
	{
		public CounterpartyJournalFilterViewModel(params Action<CounterpartyJournalFilterViewModel>[] filterParams)
		{
			UpdateWith(
				x => x.CounterpartyType,
				x => x.RestrictIncludeArchive,
				x => x.Tag
			);

            SalesChannel salesChannelAlias = null;
            SalesChannelSelectableNode salesChannelSelectableNodeAlias = null;

            var list = UoW.Session.QueryOver(() => salesChannelAlias)
                .SelectList(scList => scList
                .SelectGroup(() => salesChannelAlias.Id).WithAlias(() => salesChannelSelectableNodeAlias.Id)
                    .Select(() => salesChannelAlias.Name).WithAlias(() => salesChannelSelectableNodeAlias.Name)
                ).TransformUsing(Transformers.AliasToBean<SalesChannelSelectableNode>()).List<SalesChannelSelectableNode>();

            SalesChannels = new GenericObservableList<SalesChannelSelectableNode>(list);

			if(filterParams != null)
			{
				SetAndRefilterAtOnce(filterParams);
			}
        }

		private CounterpartyType? counterpartyType;
		public virtual CounterpartyType? CounterpartyType {
			get => counterpartyType;
			set => SetField(ref counterpartyType, value, () => CounterpartyType);
		}

		private bool restrictIncludeArchive;
		public bool RestrictIncludeArchive {
			get => restrictIncludeArchive;
			set => SetField(ref restrictIncludeArchive, value);
		}

		private Tag tag;
		public Tag Tag {
			get => tag;
			set => SetField(ref tag, value);
		}

		//TODO разобраться с Tag
		/*private IRepresentationModel tagVM;
		public IRepresentationModel TagVM {
			get {
				if(tagVM == null) {
					tagVM = new TagVM(UoW);
				}
				return tagVM;
			}
		}*/

        private bool? isForRetail;
        public bool? IsForRetail
        {
            get => isForRetail;
			set => SetField(ref isForRetail, value);
        }

        private GenericObservableList<SalesChannelSelectableNode> salesChannels = new GenericObservableList<SalesChannelSelectableNode>();
        public GenericObservableList<SalesChannelSelectableNode> SalesChannels
        {
            get => salesChannels;
            set {
				UnsubscribeOnCheckChanged();
				SetField(ref salesChannels, value);
				SubscribeOnCheckChanged();
			}
		}

		private void UnsubscribeOnCheckChanged()
		{
			foreach (SalesChannelSelectableNode selectableSalesChannel in SalesChannels)
			{
				selectableSalesChannel.PropertyChanged -= OnStatusCheckChanged;
			}
		}

		private void SubscribeOnCheckChanged()
		{
			foreach (SalesChannelSelectableNode selectableSalesChannel in SalesChannels)
			{
				selectableSalesChannel.PropertyChanged += OnStatusCheckChanged;
			}
		}

		private void OnStatusCheckChanged(object sender, PropertyChangedEventArgs e)
		{
			Update();
		}
	}
}
