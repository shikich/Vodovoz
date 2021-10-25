using System;
using QS.Project.Filter;

namespace Vodovoz.FilterViewModels.Organization
{
	public class SubdivisionFilterViewModel : FilterViewModelBase<SubdivisionFilterViewModel>
	{
		public SubdivisionFilterViewModel(params Action<SubdivisionFilterViewModel>[] filterParams)
		{
			if(filterParams != null)
			{
				SetAndRefilterAtOnce(filterParams);
			}
		}

		private int[] excludedSubdivisions;
		public virtual int[] ExcludedSubdivisions {
			get => excludedSubdivisions;
			set => UpdateFilterField(ref excludedSubdivisions, value, () => ExcludedSubdivisions);
		}

		private SubdivisionType? subdivisionType;
		public virtual SubdivisionType? SubdivisionType {
			get => subdivisionType;
			set => UpdateFilterField(ref subdivisionType, value, () => SubdivisionType);
		}
	}
}
