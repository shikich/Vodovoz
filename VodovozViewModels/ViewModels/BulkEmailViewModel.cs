﻿using Mailjet.Api.Abstractions;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Criterion;
using NLog.Extensions.Logging;
using QS.Attachments.Domain;
using QS.Attachments.ViewModels.Widgets;
using QS.Commands;
using QS.Dialog;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Services;
using QS.ViewModels.Dialog;
using RabbitMQ.Infrastructure;
using RabbitMQ.MailSending;
using System;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Web;
using Vodovoz.Domain.Contacts;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.StoredEmails;
using Vodovoz.EntityRepositories;
using Vodovoz.Factories;
using Vodovoz.Parameters;
using Vodovoz.ViewModels.Journals.JournalNodes;
using VodovozInfrastructure.Configuration;
using EmailAttachment = Mailjet.Api.Abstractions.EmailAttachment;
using Order = Vodovoz.Domain.Orders.Order;

namespace Vodovoz.ViewModels.ViewModels
{
	public class BulkEmailViewModel : DialogViewModelBase
	{
		private readonly IUnitOfWork _uow;
		private readonly IEmailParametersProvider _emailParametersProvider;
		private readonly ICommonServices _commonServices;
		private readonly int _instanceId;
		private readonly InstanceMailingConfiguration _configuration;
		private DelegateCommand _startEmailSendingCommand;
		private IList<Attachment> _attachments = new List<Attachment>();
		private GenericObservableList<Attachment> _observableAttachments;
		private bool _isInSendingProcess;
		private readonly Employee _author;
		private float _attachmentsSize;
		private string _mailSubject;
		private string _mailTextPart;
		private double _sendingProgressValue;
		private int[] _alreadySentCounterpartyIds;
		private int _alreadySentMonthCount;
		private IList<Domain.Client.Counterparty> _counterpartiesToSent;
		private readonly IList<DebtorJournalNode> _debtorJournalNodes;
		private readonly IEmailRepository _emailRepository;
		private bool _canSend = true;

		public BulkEmailViewModel(INavigationManager navigation, IUnitOfWorkFactory unitOfWorkFactory,
			Func<IUnitOfWork, IQueryOver<Order>> itemsSourceQueryFunction, IEmailParametersProvider emailParametersProvider,
			ICommonServices commonServices, IAttachmentsViewModelFactory attachmentsViewModelFactory, Employee author, IEmailRepository emailRepository) : base(navigation)
		{
			_uow = (unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory))).CreateWithoutRoot();
			_emailParametersProvider = emailParametersProvider ?? throw new ArgumentNullException(nameof(emailParametersProvider));
			_commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
			_author = author ?? throw new ArgumentNullException(nameof(author));
			_emailRepository = emailRepository ?? throw new ArgumentNullException(nameof(emailRepository));
			_instanceId = emailRepository.GetCurrentDatabaseId(_uow);

			_configuration = _uow.GetAll<InstanceMailingConfiguration>().FirstOrDefault();

			AttachmentsEmailViewModel = attachmentsViewModelFactory.CreateNewAttachmentsViewModel(ObservableAttachments);

			ObservableAttachments.ListContentChanged += ObservableAttachments_ListContentChanged;

			var itemsSourceQuery = itemsSourceQueryFunction.Invoke(_uow);
			_debtorJournalNodes = itemsSourceQuery.List<DebtorJournalNode>();

			MailSubject = string.Empty;

			Init();
		}

		private void Init()
		{
			var debtorIds = _debtorJournalNodes.Select(dbj => dbj.ClientId).ToArray();

			_alreadySentCounterpartyIds = _uow.Session.QueryOver<BulkEmail>()
				.Where(be => be.Counterparty.Id.IsIn(debtorIds))
				.JoinQueryOver(be => be.StoredEmail)
				.Where(se => se.SendDate > DateTime.Now.AddHours(-2))
				.And(se => se.State != StoredEmailStates.SendingError)
				.Select(be => be.Counterparty.Id)
				.List<int>()
				.ToArray();

			_alreadySentMonthCount = _uow.Session.QueryOver<BulkEmail>()
				.JoinQueryOver(be => be.StoredEmail)
				.Where(se => se.SendDate > DateTime.Now.AddMonths(-1))
				.RowCount();

			var counterpartiesToSentIds = _debtorJournalNodes
				.Where(d => !_alreadySentCounterpartyIds.Contains(d.ClientId))
				.Select(c => c.ClientId)
				.ToArray();

			_counterpartiesToSent = _uow.GetById<Domain.Client.Counterparty>(counterpartiesToSentIds);

			SendingProgressValue = 0;

			OnPropertyChanged(nameof(RecepientInfoDanger));
		}

		private void ObservableAttachments_ListContentChanged(object sender, EventArgs e)
		{
			_attachmentsSize = 0;

			foreach(var attachment in AttachmentsEmailViewModel.Attachments)
			{
				_attachmentsSize += (attachment.ByteFile.Length / 1024f) / 1024f;
			}

			OnPropertyChanged(nameof(AttachmentsSizeInfoDanger));
		}


		private Email SelectPriorityEmail(IList<Email> counterpartyEmails)
		{
			var email = counterpartyEmails.FirstOrDefault(e => e.EmailType?.EmailPurpose == EmailPurpose.ForBills)
						?? counterpartyEmails.FirstOrDefault(e => e.EmailType?.EmailPurpose == EmailPurpose.Work)
						?? counterpartyEmails.FirstOrDefault(e => e.EmailType?.EmailPurpose == EmailPurpose.Personal)
						?? counterpartyEmails.FirstOrDefault(e => e.EmailType?.EmailPurpose == EmailPurpose.ForReceipts)
						?? counterpartyEmails.FirstOrDefault();

			return email;
		}

		private void SendEmail(string email, string name, int storedEmailId)
		{
			var sendEmailMessage = new SendEmailMessage()
			{
				From = new EmailContact
				{
					Name = _emailParametersProvider.DocumentEmailSenderName,
					Email = _emailParametersProvider.DocumentEmailSenderAddress
				},

				To = new List<EmailContact>
				{
					new EmailContact
					{
						Name = name,
						Email = email
					}
				},

				Subject = MailSubject,
				TextPart = MailTextPart,
				HTMLPart = MailTextPart,
				Payload = new EmailPayload
				{
					Id = storedEmailId,
					Trackable = true,
					InstanceId = _instanceId
				}
			};

			var emailAttachments = new List<EmailAttachment>();

			foreach(var attachment in ObservableAttachments)
			{
				emailAttachments.Add(new EmailAttachment
				{
					ContentType = MimeMapping.GetMimeMapping(attachment.FileName),
					Filename = attachment.FileName,
					Base64Content = Convert.ToBase64String(attachment.ByteFile)
				});
			}

			sendEmailMessage.Attachments = emailAttachments;

			var serializedMessage = JsonSerializer.Serialize(sendEmailMessage);
			var sendingBody = Encoding.UTF8.GetBytes(serializedMessage);

			var logger = new Logger<RabbitMQConnectionFactory>(new NLogLoggerFactory());
			var connectionFactory = new RabbitMQConnectionFactory(logger);
			var connection = connectionFactory.CreateConnection(_configuration.MessageBrokerHost, _configuration.MessageBrokerUsername,
				_configuration.MessageBrokerPassword, _configuration.MessageBrokerVirtualHost);
			var channel = connection.CreateModel();
			var properties = channel.CreateBasicProperties();
			properties.Persistent = true;

			channel.BasicPublish(_configuration.EmailSendExchange, _configuration.EmailSendKey, false, properties, sendingBody);
		}

		#region Commands

		public DelegateCommand StartEmailSendingCommand =>
			_startEmailSendingCommand ?? (_startEmailSendingCommand = new DelegateCommand(() =>
			{
				if(!_commonServices.InteractiveService.Question($"Отправить письмо {_counterpartiesToSent.Count} выбранным клиентам?"))
				{
					return;
				}

				IsInSendingProcess = true;

				string withoutEmails = string.Empty;
				string sendingErrors = string.Empty;

				OnPropertyChanged(nameof(SendingProgressUpper));

				using(var unitOfWork = UnitOfWorkFactory.CreateWithoutRoot("BulkEmail"))
				{
					foreach(var counterparty in _counterpartiesToSent)
					{
						if(!_canSend)
						{
							return;
						}

						var email = SelectPriorityEmail(counterparty.Emails);
						if(email == null)
						{
							withoutEmails += counterparty.FullName + "; ";
						}
						else
						{
							var storedEmail = new StoredEmail
							{
								State = StoredEmailStates.WaitingToSend,
								Author = _author,
								ManualSending = true,
								SendDate = DateTime.Now,
								StateChangeDate = DateTime.Now,
								Subject = MailSubject,
								RecipientAddress = email.Address
							};

							unitOfWork.Save(storedEmail);

							var bulkEmail = new BulkEmail()
							{
								StoredEmail = storedEmail,
								Counterparty = counterparty
							};

							unitOfWork.Save(bulkEmail);

							try
							{
								SendEmail(email.Address, counterparty.FullName, storedEmail.Id);
								unitOfWork.Commit();
							}
							catch(Exception e)
							{
								sendingErrors += $"{ counterparty.FullName }; ";
							}
						}

						SendingProgressValue += 1;

						SendingProgressBarUpdated?.Invoke(this, EventArgs.Empty);
					}
				}

				if(withoutEmails.Length > 0)
				{
					_commonServices.InteractiveService.ShowMessage(ImportanceLevel.Warning, $"У следующих контрагентов отсутствует email:{ withoutEmails }");
				}

				if(sendingErrors.Length > 0)
				{
					_commonServices.InteractiveService.ShowMessage(ImportanceLevel.Error, $"Возникла ошибка при отправке писем следующим контрагентам: { sendingErrors }");
				}

				_commonServices.InteractiveService.ShowMessage(ImportanceLevel.Info, $"Завершено");

				Init();

				IsInSendingProcess = false;
			},
				() => CanExecute)
			);

		#endregion

		[PropertyChangedAlso(nameof(MailSubjectInfoDanger), nameof(CanExecute))]
		public string MailSubject
		{
			get => _mailSubject;
			set => SetField(ref _mailSubject, value);
		}

		public string MailSubjectInfo => $"{MailSubject?.Length ?? 0}/255 символов";

		public bool MailSubjectInfoDanger => MailSubject?.Length == 0 || MailSubject?.Length > 255;

		public string MailTextPart
		{
			get => _mailTextPart;
			set => SetField(ref _mailTextPart, value);
		}

		public AttachmentsViewModel AttachmentsEmailViewModel { get; }

		public string AttachmentsSizeInfo => $"{_attachmentsSize.ToString("F")} / 15 Мб";

		[PropertyChangedAlso(nameof(CanExecute))]
		public bool AttachmentsSizeInfoDanger => _attachmentsSize > 15;

		public GenericObservableList<Attachment> ObservableAttachments =>
			_observableAttachments ?? (_observableAttachments = new GenericObservableList<Attachment>(_attachments));

		[PropertyChangedAlso(nameof(SendingDurationInfo), nameof(SendedCountInfo))]

		public double SendingProgressValue
		{
			get => _sendingProgressValue;
			set => SetField(ref _sendingProgressValue, value);
		}

		public double SendingProgressUpper => _counterpartiesToSent.Count;

		public string SendedCountInfo => $"Обработано {SendingProgressValue} из {SendingProgressUpper} контрагентов";

		public string SendingDurationInfo
		{
			get
			{
				TimeSpan time = TimeSpan.FromSeconds(SendingProgressUpper - SendingProgressValue);
				return time.ToString(@"hh\:mm\:ss");
			}
		}

		public bool IsInSendingProcess
		{
			get => _isInSendingProcess;
			private set => SetField(ref _isInSendingProcess, value);
		}

		public string RecepientInfo =>
			$"{_counterpartiesToSent.Count - _alreadySentCounterpartyIds.Length} / 1000 за раз ({_alreadySentCounterpartyIds.Length} " +
			$"уже получали письмо массовой рассылки в течение последних 2 часов)" +
			$"{Environment.NewLine}и {_alreadySentMonthCount} / 20000 в месяц писем вида массовой рассылки";

		[PropertyChangedAlso(nameof(CanExecute))]
		public bool RecepientInfoDanger =>
			(_counterpartiesToSent.Count - _alreadySentCounterpartyIds.Length) > 1000 || _alreadySentMonthCount > 20000;

		public bool CanExecute => !AttachmentsSizeInfoDanger && !MailSubjectInfoDanger && !RecepientInfoDanger;

		public EventHandler SendingProgressBarUpdated;

		public void Stop()
		{
			_canSend = false;
		}
	}
}
