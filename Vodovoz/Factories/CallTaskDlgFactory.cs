using Autofac;
using Vodovoz.Dialogs;
using Vodovoz.EntityRepositories;
using Vodovoz.EntityRepositories.CallTasks;
using Vodovoz.EntityRepositories.Employees;
using Vodovoz.EntityRepositories.Operations;

namespace Vodovoz.Factories
{
	public class CallTaskDlgFactory : ICallTaskDlgFactory
	{
		private readonly ILifetimeScope _scope;
		
		public CallTaskDlgFactory(ILifetimeScope scope)
		{
			_scope = scope;
		}

		public CallTaskDlg CreateNewCallTaskDlg()
		{
			var scope = _scope.BeginLifetimeScope();

			return new CallTaskDlg(
				scope,
				scope.Resolve<IEmployeeRepository>(),
				scope.Resolve<IBottlesRepository>(),
				scope.Resolve<ICallTaskRepository>(),
				scope.Resolve<IPhoneRepository>());
		}
		
		public CallTaskDlg OpenCallTaskDlg(int callTaskId)
		{
			var scope = _scope.BeginLifetimeScope();

			return new CallTaskDlg(
				callTaskId,
				scope,
				scope.Resolve<IEmployeeRepository>(),
				scope.Resolve<IBottlesRepository>(),
				scope.Resolve<ICallTaskRepository>(),
				scope.Resolve<IPhoneRepository>());
		}
		
		public CallTaskDlg OpenCallTaskDlg(int counterpartyId, int deliveryPointId)
		{
			var scope = _scope.BeginLifetimeScope();

			return new CallTaskDlg(
				counterpartyId,
				deliveryPointId,
				scope,
				scope.Resolve<IEmployeeRepository>(),
				scope.Resolve<IBottlesRepository>(),
				scope.Resolve<ICallTaskRepository>(),
				scope.Resolve<IPhoneRepository>());
		}
	}
}
