using VodovozInfrastructure.Endpoints;

namespace Vodovoz.ViewModels.TempAdapters
{
	public interface IDriverApiUserRegisterEndpointBuilder
	{
		DriverApiUserRegisterEndpoint CreateNewEndpoint();
	}
}
