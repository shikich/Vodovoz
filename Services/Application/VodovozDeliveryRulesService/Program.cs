using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Threading;
using Mono.Unix;
using Mono.Unix.Native;
using NLog;

namespace VodovozDeliveryRulesService
{
	class Service
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		public static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += AppDomain_CurrentDomain_UnhandledException;

			logger.Info("Запуск службы правил доставки...");
			try {

				WebServiceHost host = new WebServiceHost(typeof(DeliveryRulesService));

				ServiceEndpoint webEndPoint = host.AddServiceEndpoint(
					typeof(IDeliveryRulesService),
					new WebHttpBinding(),
					$"http://delivery.vod.qsolution.ru:7075/DeliveryRules"
				);
				// $"http://delivery.vod.qsolution.ru:7075/DeliveryRules"
				//$"http://localhost:7075/DeliveryRules"


				WebHttpBehavior httpBehavior = new WebHttpBehavior();
				webEndPoint.Behaviors.Add(httpBehavior);

#if DEBUG
				host.Description.Behaviors.Add(new PreFilter());
#endif

				host.Open();

				logger.Info("Server started.");

				UnixSignal[] signals = {
					new UnixSignal (Signum.SIGINT),
					new UnixSignal (Signum.SIGHUP),
					new UnixSignal (Signum.SIGTERM)};
				UnixSignal.WaitAny(signals);
			}
			catch(Exception e) {
				logger.Fatal(e);
			}
			finally {
				if(Environment.OSVersion.Platform == PlatformID.Unix)
					Thread.CurrentThread.Abort();
				Environment.Exit(0);
			}
		}

		static void AppDomain_CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			logger.Fatal((Exception)e.ExceptionObject, "UnhandledException");
		}
	}

	public class PreFilter : IServiceBehavior
	{
		public void AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
		{
		}

		public void Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
		{
		}

		public void ApplyDispatchBehavior(ServiceDescription desc, ServiceHostBase host)
		{
			foreach(ChannelDispatcher cDispatcher in host.ChannelDispatchers)
				foreach(EndpointDispatcher eDispatcher in cDispatcher.Endpoints)
					eDispatcher.DispatchRuntime.MessageInspectors.Add(new ConsoleMessageTracer());
		}
	}

	public class ConsoleMessageTracer : IDispatchMessageInspector
	{
		static readonly Logger logger = LogManager.GetCurrentClassLogger();

		enum Action
		{
			Send,
			Receive
		};

		private Message TraceMessage(MessageBuffer buffer, Action action)
		{
			Message msg = buffer.CreateMessage();
			try {
				if(action == Action.Receive) {
					logger.Info("Received: {0}", msg.Headers.To.AbsoluteUri);
					if(!msg.IsEmpty)
						logger.Debug("Received Body: {0}", msg);
				} else
					logger.Debug("Sended: {0}", msg);
			}
			catch(Exception ex) {
				logger.Error(ex, "Ошибка логгирования сообщения.");
			}
			return buffer.CreateMessage();
		}

		public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			request = TraceMessage(request.CreateBufferedCopy(int.MaxValue), Action.Receive);
			return null;
		}

		public void BeforeSendReply(ref Message reply, object correlationState)
		{
			reply = TraceMessage(reply.CreateBufferedCopy(int.MaxValue), Action.Send);
		}
	}
}
