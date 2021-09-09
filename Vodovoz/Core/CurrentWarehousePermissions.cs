﻿using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using QS.DomainModel.UoW;
using QS.Project.Services;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Permissions.Warehouses;
using Vodovoz.EntityRepositories.Employees;

namespace Vodovoz.Core
{
	public class CurrentWarehousePermissions
	{
		private IList<WarehousePermissionBase> permissions;

		public IList<WarehousePermissionBase> WarehousePermissions
		{
			get
			{
				if (permissions == null)
					Load();
				return permissions;
			}
		}

		private void Load()
		{
			var userId = ServicesConfig.UserService.CurrentUserId;
			using(var uow = UnitOfWorkFactory.CreateForRoot<User>(userId))
			{
				var employee = new EmployeeRepository().GetEmployeeForCurrentUser(uow);
				var subdivision = employee.Subdivision;
				permissions = new List<WarehousePermissionBase>();
				var userWarehousePermissionsQuery = uow.Session.QueryOver<UserWarehousePermission>()
					.Where(x => x.User.Id == userId && x.PermissionValue == true).List();
				userWarehousePermissionsQuery.ForEach(x => permissions.Add(x));
				while(subdivision != null || !permissions.Any())
				{
					var subdivisionWarehousePermissionQuery = uow.Session.QueryOver<SubdivisionWarehousePermission>()
						.Where(x => x.Subdivision.Id == subdivision.Id && x.PermissionValue == true).List();
					subdivisionWarehousePermissionQuery.ForEach(x => permissions.Add(x));
					subdivision = subdivision?.ParentSubdivision;
				}
			}
		}
	}
}
