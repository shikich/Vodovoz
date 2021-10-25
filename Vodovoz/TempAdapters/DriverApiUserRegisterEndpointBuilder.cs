using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Vodovoz.ViewModels.TempAdapters;
using VodovozInfrastructure.Endpoints;

namespace Vodovoz.TempAdapters
{
	public class DriverApiUserRegisterEndpointBuilder : IDriverApiUserRegisterEndpointBuilder
	{
		public DriverApiUserRegisterEndpoint CreateNewEndpoint()
		{
			var cs = new ConfigurationSection(new ConfigurationRoot(new List<IConfigurationProvider> { new MemoryConfigurationProvider(new MemoryConfigurationSource()) }), "")
				{
					["BaseUri"] = "https://driverapi.vod.qsolution.ru:7090/api/"
				};

			var apiHelper = new ApiClientProvider.ApiClientProvider(cs);

			return new DriverApiUserRegisterEndpoint(apiHelper);
		}
	}
}
