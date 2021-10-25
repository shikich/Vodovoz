using QS.DomainModel.Entity;
using Vodovoz.Domain.Retail;

namespace Vodovoz.Journals.Nodes.Counterparties
{
	public class SalesChannelSelectableNode : PropertyChangedBase
	{
		private int _id;

		public virtual int Id
		{
			get => _id;
			set => SetField(ref _id, value);
		}

		private bool _selected;

		public virtual bool Selected
		{
			get => _selected;
			set => SetField(ref _selected, value);
		}

		private string _name;

		public virtual string Name
		{
			get => _name;
			set => SetField(ref _name, value);
		}

		public string Title => Name;

		public SalesChannelSelectableNode()
		{
		}

		public SalesChannelSelectableNode(SalesChannel salesChannel)
		{
			Id = salesChannel.Id;
			Name = salesChannel.Name;
		}
	}
}
