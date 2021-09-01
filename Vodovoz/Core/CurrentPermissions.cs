﻿using System.Collections.Generic;
using System.Linq;
using QS.DomainModel.UoW;
using QS.Project.Services;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Permissions.Warehouses;
using Vodovoz.EntityRepositories.Employees;

namespace Vodovoz.Core
{
	public class CurrentPermissions
	{
		private List<WarehousePermission> permissions;

		public List<WarehousePermission> Warehouse
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
				permissions = uow.Session.QueryOver<WarehousePermission>().Where(x=>(x.Subdivision.Id == subdivision.Id || x.User.Id == userId) && x.PermissionValue == true).List().ToList();
			}
		}
	}
}
