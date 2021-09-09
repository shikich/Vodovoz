﻿using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using QS.Dialog.GtkUI;
using QS.DomainModel.UoW;
using Vodovoz.Core;
using Vodovoz.Domain.Permissions.Warehouses;
using Vodovoz.Domain.Store;

namespace Vodovoz.Additions.Store
{
	public class StoreDocumentHelper
	{
		private CurrentWarehousePermissions WarehousePermissions { get; }

		public StoreDocumentHelper()
		{
			WarehousePermissions = new CurrentWarehousePermissions();
		}
		public Warehouse GetDefaultWarehouse(IUnitOfWork uow, WarehousePermissionsType edit)
		{
			if(CurrentUserSettings.Settings.DefaultWarehouse != null) {
				var warehouse = uow.GetById<Warehouse>(CurrentUserSettings.Settings.DefaultWarehouse.Id);
				var warehouses = WarehousePermissions.WarehousePermissions.Where(x => x.Warehouse.Id == warehouse.Id);
				var permission = warehouses
					.SingleOrDefault(x => x.WarehousePermissionTypeType == WarehousePermissionsType.WarehouseView)
					?.PermissionValue;
				var permissionEdit =
					warehouses.SingleOrDefault(x => x.WarehousePermissionTypeType == edit)?.PermissionValue;
				if((permission.HasValue && permission.Value) && (permissionEdit.HasValue && permissionEdit.Value))
					return warehouse;
			}

			if(WarehousePermissions.WarehousePermissions.Count(x => x.WarehousePermissionTypeType == edit) == 1)
				return WarehousePermissions.WarehousePermissions.First(x => x.WarehousePermissionTypeType == edit).Warehouse;

			return null;
		}

		/// <summary>
		/// Проверка прав на просмотр документа
		/// </summary>
		/// <returns>Если <c>true</c> нет прав на просмотр.</returns>
		public bool CheckViewWarehouse(WarehousePermissionsType edit, params Warehouse[] warehouses)
		{
			//Внимание!!! Склад пустой обычно у новых документов. Возможность создания должна проверятся другими условиями. Тут пропускаем.
			warehouses = warehouses.Where(x => x != null).ToArray();
			if(warehouses.Length == 0)
				return false;
			var permission =
				WarehousePermissions.WarehousePermissions.Where(x =>
					x.WarehousePermissionTypeType == WarehousePermissionsType.WarehouseView);
			var permissionEdit = WarehousePermissions.WarehousePermissions.Where(x => x.WarehousePermissionTypeType == edit);
			if(warehouses.Any(x => permission.SingleOrDefault(y=>y.Warehouse.Id == x.Id).PermissionValue.Value
			                       || permissionEdit.SingleOrDefault(y=>y.Warehouse.Id == x.Id).PermissionValue.Value))
				return false;

			MessageDialogHelper.RunErrorDialog($"У вас нет прав на просмотр документов склада '{string.Join(";", warehouses.Distinct().Select(x => x.Name))}'.");
			return true;
		}

		/// <summary>
		/// Проверка прав на создание документа
		/// </summary>
		/// <returns>Если <c>true</c> нет прав на создание.</returns>
		public bool CheckCreateDocument(WarehousePermissionsType edit, params Warehouse[] warehouses)
		{
			warehouses = warehouses.Where(x => x != null).ToArray();
			if(warehouses.Any()) {
				if(warehouses.Any(x => WarehousePermissions.WarehousePermissions.SingleOrDefault(y=>y.WarehousePermissionTypeType == edit && y.Warehouse.Id == x.Id).PermissionValue.Value))
					return false;

				MessageDialogHelper.RunErrorDialog(
					string.Format(
						"У вас нет прав на создание этого документа для склада '{0}'.",
						string.Join(";", warehouses.Distinct().Select(x => x.Name))
					)
				);
			} else {
				if(WarehousePermissions.WarehousePermissions.Any(x => x.WarehousePermissionTypeType == edit))
					return false;

				MessageDialogHelper.RunErrorDialog("У вас нет прав на создание этого документа.");
			}
			return true;
		}

		/// <summary>
		/// Проверка всех прав диалога.
		/// </summary>
		/// <returns>Если <c>true</c> нет прав на просмотр.</returns>
		public bool CheckAllPermissions(bool isNew, WarehousePermissionsType edit, params Warehouse[] warehouses)
		{
			if(isNew && CheckCreateDocument(edit, warehouses))
				return true;

			if(CheckViewWarehouse(edit, warehouses))
				return true;

			return false;
		}

		/// <summary>
		/// Проверка прав на изменение документа
		/// </summary>
		/// <returns>Если <c>false</c> нет прав на создание.</returns>
		public bool CanEditDocument(WarehousePermissionsType edit, params Warehouse[] warehouses)
		{
			warehouses = warehouses.Where(x => x != null).ToArray();
			if(warehouses.Any())
				return warehouses.Any(x => WarehousePermissions.WarehousePermissions.SingleOrDefault(y=>y.WarehousePermissionTypeType == edit && y.Warehouse.Id == x.Id).PermissionValue.Value);
			return WarehousePermissions.WarehousePermissions.Any(x => x.WarehousePermissionTypeType == edit);
		}

		public QueryOver<Warehouse> GetWarehouseQuery() => QueryOver.Of<Warehouse>().AndNot(w => w.IsArchive);

		public QueryOver<Warehouse> GetRestrictedWarehouseQuery(params WarehousePermissionsType[] permissions)
		{
			var query = QueryOver.Of<Warehouse>().WhereNot(w => w.IsArchive);
			var disjunction = new Disjunction();

			foreach(var p in permissions) {
				disjunction.Add<Warehouse>(w => w.Id.IsIn(WarehousePermissions.WarehousePermissions.Where(x=>x.WarehousePermissionTypeType == p).Select(x => x.Id).ToArray()));
			}

			return query.Where(disjunction);
		}

		public IList<Warehouse> GetRestrictedWarehousesList(IUnitOfWork uow, params WarehousePermissionsType[] permissions)
		{
			var result = GetRestrictedWarehouseQuery(permissions)
				.DetachedCriteria
				.GetExecutableCriteria(uow.Session)
				.List<Warehouse>()
				.Where(w => w.OwningSubdivision != null)
				.ToList();
			return result;
		}

		/// <summary>
		/// Запрос на возврат всех скадов для которых есть хотя бы одно из разрешений.
		/// </summary>
		public QueryOver<Warehouse> GetRestrictedWarehouseQuery()
		{
			return QueryOver.Of<Warehouse>()
							.Where(w => w.Id.IsIn(WarehousePermissions.WarehousePermissions.Where(x=>x.PermissionValue == true).Select(x => x.Id).ToArray()))
							.AndNot(w => w.IsArchive);
		}
	}
}
