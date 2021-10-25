using Vodovoz.Dialogs;

namespace Vodovoz.Factories
{
	public interface ICallTaskDlgFactory
	{
		CallTaskDlg CreateNewCallTaskDlg();
		CallTaskDlg OpenCallTaskDlg(int callTaskId);
		CallTaskDlg OpenCallTaskDlg(int counterpartyId, int deliveryPointId);
	}
}
