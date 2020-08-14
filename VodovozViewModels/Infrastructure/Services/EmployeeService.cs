using System;
using QS.DomainModel.UoW;
using QS.Services;
using Vodovoz.Domain.Employees;

namespace Vodovoz.Infrastructure.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IUserService userService;

		public EmployeeService(IUserService userService)
		{
			this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
		}

		public Employee GetEmployeeForCurrentUser(IUnitOfWork uow)
		{
			return GetEmployeeForUser(uow, userService.CurrentUserId);
		}

		public Employee GetEmployeeForUser(IUnitOfWork uow, int userId)
		{
			User userAlias = null;
			return uow.Session.QueryOver<Employee>()
				.JoinAlias(e => e.User, () => userAlias)
				.Where(() => userAlias.Id == userId)
				.SingleOrDefault();
		}
	}
}
