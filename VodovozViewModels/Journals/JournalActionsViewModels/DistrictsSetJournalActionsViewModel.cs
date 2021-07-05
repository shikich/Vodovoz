﻿using System;
using System.Linq;
using QS.Commands;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Sale;
using Vodovoz.Infrastructure.Services;
using Vodovoz.JournalNodes;

namespace Vodovoz.Journals.JournalActionsViewModels
{
	public class DistrictsSetJournalActionsViewModel : EntitiesJournalActionsViewModel
	{
		private readonly ICommonServices _commonServices;
		private readonly IEmployeeService _employeeService;
		private readonly bool _canCreate;
		private DelegateCommand _copyDistrictSetCommand;
		
		public DistrictsSetJournalActionsViewModel(
			ICommonServices commonServices,
			IEmployeeService employeeService,
			IUnitOfWorkFactory unitOfWorkFactory) : base(commonServices?.InteractiveService)
		{
			_commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
			_employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));

			if(unitOfWorkFactory == null)
			{
				throw new ArgumentNullException(nameof(unitOfWorkFactory));
			}
			
			UoW = unitOfWorkFactory.CreateWithoutRoot();

			_canCreate = _commonServices.CurrentPermissionService.ValidateEntityPermission(typeof(DistrictsSet)).CanCreate;
		}
		
		public override object[] SelectedItems 
		{
			get => selectedItems;
			set 
			{
				if (SetField(ref selectedItems, value)) 
				{
					OnPropertyChanged(nameof(CanSelect));
					OnPropertyChanged(nameof(CanAdd));
					OnPropertyChanged(nameof(CanEdit));
					OnPropertyChanged(nameof(CanDelete));
					OnPropertyChanged(nameof(CanCopyDistrictSet));
				}
			}
		}

		public bool CanCopyDistrictSet => _canCreate && SelectedItems.OfType<DistrictsSetJournalNode>().FirstOrDefault() != null;
		
		public DelegateCommand CopyDistrictSetCommand => _copyDistrictSetCommand ?? (_copyDistrictSetCommand = new DelegateCommand(
				() =>
				{
					var selectedNode = SelectedItems.OfType<DistrictsSetJournalNode>().FirstOrDefault();

					if(selectedNode == null)
					{
						return;
					}

					var districtsSetToCopy = UoW.GetById<DistrictsSet>(selectedNode.Id);
					var alreadyCopiedDistrict = UoW.Session.QueryOver<District>()
						.WhereRestrictionOn(x => x.CopyOf.Id)
						.IsIn(districtsSetToCopy.Districts.Select(x => x.Id).ToArray())
						.Take(1)
						.SingleOrDefault();
					
					if(alreadyCopiedDistrict != null) 
					{
						interactiveService.ShowMessage(ImportanceLevel.Warning,
							$"Выбранная версия районов уже была скопирована\n" +
							$"Копия: (Код: {alreadyCopiedDistrict.DistrictsSet.Id}) {alreadyCopiedDistrict.DistrictsSet.Name}");
						return;
					}
					
					if(interactiveService.Question($"Скопировать версию районов \"{selectedNode.Name}\"")) 
					{
						var copy = (DistrictsSet)districtsSetToCopy.Clone();
						copy.Name += " - копия";
						copy.Author = _employeeService.GetEmployeeForUser(UoW, _commonServices.UserService.CurrentUserId);
						copy.Status = DistrictsSetStatus.Draft;
						copy.DateCreated = DateTime.Now;
						
						UoW.Save(copy);
						UoW.Commit();
						interactiveService.ShowMessage(ImportanceLevel.Info, "Копирование завершено");
					}
				},
				() => CanCopyDistrictSet
			)
		);
	}
}