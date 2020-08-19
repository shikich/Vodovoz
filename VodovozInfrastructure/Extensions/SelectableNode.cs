using System;
using QS.DomainModel.Entity;

namespace VodovozInfrastructure.Extensions
{
	public class SelectableNode<T> : PropertyChangedBase
	{
		public T Value { get; }

		public SelectableNode(T value)
		{
			if(value is object && value == null) {
				throw new ArgumentNullException(nameof(value));
			}
			this.Value = value;
		}

		private bool selected;
		public bool Selected {
			get => selected;
			set => SetField(ref selected, value);
		}

		public void SilentUnselect()
		{
			selected = false;
		}
	}
}
