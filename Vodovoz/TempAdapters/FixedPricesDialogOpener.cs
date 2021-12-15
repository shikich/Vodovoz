﻿using Fias.Service;
using QS.Tdi;
using Vodovoz.Factories;
using Vodovoz.Parameters;
using Vodovoz.Services;

namespace Vodovoz.TempAdapters
{
	public class FixedPricesDialogOpener : IFixedPricesDialogOpener
	{
		private readonly IDeliveryPointViewModelFactory _deliveryPointViewModelFactory;

		public FixedPricesDialogOpener()
		{
			IParametersProvider parametersProvider = new ParametersProvider();
			IFiasApiParametersProvider fiasApiParametersProvider = new FiasApiParametersProvider(parametersProvider);
			IFiasApiClient fiasApiClient = new FiasApiClient(fiasApiParametersProvider.FiasApiBaseUrl, fiasApiParametersProvider.FiasApiToken);
			_deliveryPointViewModelFactory = new DeliveryPointViewModelFactory(fiasApiClient);
		}
		public void OpenFixedPricesForSelfDelivery(int counterpartyId)
		{
			CounterpartyDlg counterpartyDlg = new CounterpartyDlg(counterpartyId);
			counterpartyDlg.OpenFixedPrices();
			TDIMain.MainNotebook.AddTab(counterpartyDlg);
		}

		public void OpenFixedPricesForDeliveryPoint(int deliveryPointId)
		{
			var dpViewModel = _deliveryPointViewModelFactory.GetForOpenDeliveryPointViewModel(deliveryPointId);
			TDIMain.MainNotebook.AddTab(dpViewModel);
			dpViewModel.OpenFixedPrices();
		}
	}
}
