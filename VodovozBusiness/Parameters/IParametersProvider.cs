namespace Vodovoz.Parameters
{
	public interface IParametersProvider
	{
		bool ContainsParameter(string parameterName);
		string GetParameterValue(string parameterName);
		void RefreshParameters();
	}
}
