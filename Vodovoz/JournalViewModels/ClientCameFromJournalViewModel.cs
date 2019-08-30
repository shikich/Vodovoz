﻿using System;
using NHibernate;
using NHibernate.Transform;
using QS.DomainModel.Config;
using QS.Services;
using Vodovoz.Dialogs.Client;
using Vodovoz.Domain.Client;
using Vodovoz.Filters.ViewModels;
using Vodovoz.JournalNodes;
using QS.Project.Domain;

namespace Vodovoz.JournalViewModels
{
	public class ClientCameFromJournalViewModel : FilterableSingleEntityJournalViewModelBase<ClientCameFrom, ClientCameFromViewModel, ClientCameFromJournalNode, ClientCameFromFilterViewModel>
	{
		readonly ICommonServices commonServices;
		public ClientCameFromJournalViewModel(ClientCameFromFilterViewModel filterViewModel, IEntityConfigurationProvider entityConfigurationProvider, ICommonServices commonServices) : base(filterViewModel, entityConfigurationProvider, commonServices)
		{
			TabName = "Откуда клиент";
			SetOrder(x => x.Name);

			this.commonServices = commonServices;
			UpdateOnChanges(typeof(ClientCameFrom));
		}

		protected override Func<IQueryOver<ClientCameFrom>> ItemsSourceQueryFunction => () => {
			ClientCameFrom clientCameFromAlias = null;
			ClientCameFromJournalNode resultAlias = null;

			var query = UoW.Session.QueryOver(() => clientCameFromAlias);
			if(!FilterViewModel.RestrictArchive)
				query.Where(() => !clientCameFromAlias.IsArchive);

			query.Where(GetSearchCriterion(
				() => clientCameFromAlias.Name,
				() => clientCameFromAlias.Id
			));

			var resultQuery = query
				.SelectList(list => list
				   .Select(x => x.Id).WithAlias(() => resultAlias.Id)
				   .Select(x => x.Name).WithAlias(() => resultAlias.Name)
				   .Select(x => x.IsArchive).WithAlias(() => resultAlias.IsArchive)
				)
				.OrderBy(x => x.Name).Asc
				.TransformUsing(Transformers.AliasToBean<ClientCameFromJournalNode>());

			return resultQuery;
		};

		protected override Func<ClientCameFromViewModel> CreateDialogFunction => () => new ClientCameFromViewModel (
			EntityConstructorParam.ForCreate(),
			commonServices
		);

		protected override Func<ClientCameFromJournalNode, ClientCameFromViewModel> OpenDialogFunction => node => new ClientCameFromViewModel(
			EntityConstructorParam.ForOpen(node.Id),
			commonServices
		);
	}
}
