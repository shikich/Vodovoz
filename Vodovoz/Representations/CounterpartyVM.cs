﻿using System;
using System.Collections.Generic;
using Gamma.ColumnConfig;
using Gtk;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect.Function;
using NHibernate.Transform;
using QSOrmProject;
using QSOrmProject.RepresentationModel;
using Vodovoz.Domain.Client;

namespace Vodovoz.ViewModel
{
	public class CounterpartyVM : RepresentationModelEntityBase<Counterparty, CounterpartyVMNode>
	{

		public CounterpartyFilter Filter {
			get {
				return RepresentationFilter as CounterpartyFilter;
			}
			set {
				RepresentationFilter = value as IRepresentationFilter;
			}
		}

		#region IRepresentationModel implementation

		public override void UpdateNodes ()
		{
			Counterparty counterpartyAlias = null;
			CounterpartyContract contractAlias = null;
			CounterpartyVMNode resultAlias = null;

			var query = UoW.Session.QueryOver<Counterparty> (() => counterpartyAlias);

			if (Filter.RestrictCounterpartyType != null) {
				query.Where (c => c.CounterpartyType == Filter.RestrictCounterpartyType);
			}

			if(!Filter.RestrictIncludeArhive)
			{
				query.Where(c => !c.IsArchive);
			}

			var counterpartyList = query
				.JoinAlias(c => c.CounterpartyContracts, () => contractAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
				.SelectList (list => list
					.SelectGroup (c => c.Id).WithAlias (() => resultAlias.Id)
					.Select (c => c.Name).WithAlias (() => resultAlias.Name)
					.Select (c => c.INN).WithAlias (() => resultAlias.INN)
					.Select (c => c.IsArchive).WithAlias (() => resultAlias.IsArhive)
				.Select (Projections.SqlFunction (
					new SQLFunctionTemplate (NHibernateUtil.String, "GROUP_CONCAT( ?1 SEPARATOR ?2)"),
					NHibernateUtil.String,
						Projections.Property (() => contractAlias.Id),
						Projections.Constant (", "))
					).WithAlias (() => resultAlias.Contracts)
			                       )
				.TransformUsing (Transformers.AliasToBean<CounterpartyVMNode> ())
				.List<CounterpartyVMNode> ();

			SetItemsSource (counterpartyList);
		}

		IColumnsConfig columnsConfig = FluentColumnsConfig <CounterpartyVMNode>.Create ()
			.AddColumn ("Контрагент").SetDataProperty (node => node.Name)
			.AddColumn("ИНН").AddTextRenderer(x => x.INN)
			.AddColumn("Договора").AddTextRenderer(x => x.Contracts)
			.RowCells ().AddSetter<CellRendererText> ((c, n) => c.Foreground = n.RowColor)
			.Finish ();

		public override IColumnsConfig ColumnsConfig {
			get { return columnsConfig; }
		}

		#endregion

		#region implemented abstract members of RepresentationModelBase

		protected override bool NeedUpdateFunc (Counterparty updatedSubject)
		{
			return true;
		}

		#endregion

		public CounterpartyVM () : this (UnitOfWorkFactory.CreateWithoutRoot ())
		{
			CreateRepresentationFilter = () => new CounterpartyFilter (UoW);
		}

		public CounterpartyVM (IUnitOfWork uow)
		{
			this.UoW = uow;
		}

		public CounterpartyVM (CounterpartyFilter filter) : this (filter.UoW)
		{
			Filter = filter;
		}
	}

	public class CounterpartyVMNode
	{
		public int Id{ get; set; }

		public bool IsArhive { get; set; }

		[UseForSearch]
		public string Name { get; set; }

		[UseForSearch]
		public string INN { get; set; }

		[UseForSearch]
		public string Contracts { get; set; }

		public string RowColor {
			get {
				if (IsArhive)
					return "grey";
				else
					return "black";

			}
		}
	}
}

