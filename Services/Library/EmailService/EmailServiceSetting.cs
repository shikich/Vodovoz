using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace EmailService
{
	public class EmailServiceSetting : /*IEmailServiceSetting,*/ IDisposable
	{
		private IEmailService _channel;

		public static bool SendingAllowed => !string.IsNullOrEmpty(_serviceUrl);

		private static string _serviceUrl;

		public /*static*/ IEmailService GetEmailService()
		{
			if(!SendingAllowed)
			{
				return null;
			}

			_channel = new ChannelFactory<IEmailService>(new BasicHttpBinding(), $"http://{_serviceUrl}/EmailService")
				.CreateChannel();

			return _channel;
		}

		public EmailServiceSetting(string serviceUrl = null)
		{
			_serviceUrl = serviceUrl;
		}

		public void Dispose() => (_channel as IChannel).Close();
	}
}
