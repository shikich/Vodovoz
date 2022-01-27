
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Views
{
	public partial class BulkEmailView
	{
		private global::Gtk.Table table1;

		private global::QSAttachment.Views.Widgets.AttachmentsView attachmentsEmailView;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gamma.GtkWidgets.yTextView ytextviewText;

		private global::Gtk.Label labelAttachments;

		private global::Gtk.Label labelRecepient;

		private global::Gtk.Label labelSendingDuration;

		private global::Gtk.Label labelSubject;

		private global::Gtk.Label labelText;

		private global::Gamma.GtkWidgets.yEntry yentrySubject;

		private global::Gamma.GtkWidgets.yLabel ylabelAttachmentsInfo;

		private global::Gamma.GtkWidgets.yLabel ylabelRecepientInfo;

		private global::Gamma.GtkWidgets.yLabel ylabelSendingDuration;

		private global::Gamma.GtkWidgets.yLabel ylabelSubjectInfo;

		private global::Gamma.GtkWidgets.yProgressBar yprogressbarSending;

		private global::Gtk.Button buttonSend;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Views.BulkEmailView
			this.Name = "Vodovoz.Views.BulkEmailView";
			this.Title = global::Mono.Unix.Catalog.GetString("Массовая рассылка");
			this.TypeHint = ((global::Gdk.WindowTypeHint)(1));
			this.WindowPosition = ((global::Gtk.WindowPosition)(3));
			this.Modal = true;
			this.Resizable = false;
			this.AllowGrow = false;
			// Internal child Vodovoz.Views.BulkEmailView.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(6)), ((uint)(3)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.attachmentsEmailView = new global::QSAttachment.Views.Widgets.AttachmentsView();
			this.attachmentsEmailView.Events = ((global::Gdk.EventMask)(256));
			this.attachmentsEmailView.Name = "attachmentsEmailView";
			this.table1.Add(this.attachmentsEmailView);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.attachmentsEmailView]));
			w2.TopAttach = ((uint)(2));
			w2.BottomAttach = ((uint)(3));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.HeightRequest = 150;
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.ytextviewText = new global::Gamma.GtkWidgets.yTextView();
			this.ytextviewText.CanFocus = true;
			this.ytextviewText.Name = "ytextviewText";
			this.GtkScrolledWindow.Add(this.ytextviewText);
			this.table1.Add(this.GtkScrolledWindow);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindow]));
			w4.TopAttach = ((uint)(1));
			w4.BottomAttach = ((uint)(2));
			w4.LeftAttach = ((uint)(1));
			w4.RightAttach = ((uint)(2));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelAttachments = new global::Gtk.Label();
			this.labelAttachments.Name = "labelAttachments";
			this.labelAttachments.Xalign = 1F;
			this.labelAttachments.LabelProp = global::Mono.Unix.Catalog.GetString("Вложения:");
			this.table1.Add(this.labelAttachments);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.labelAttachments]));
			w5.TopAttach = ((uint)(2));
			w5.BottomAttach = ((uint)(3));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelRecepient = new global::Gtk.Label();
			this.labelRecepient.Name = "labelRecepient";
			this.labelRecepient.Xalign = 1F;
			this.labelRecepient.LabelProp = global::Mono.Unix.Catalog.GetString("Количество адресатов:");
			this.table1.Add(this.labelRecepient);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.labelRecepient]));
			w6.TopAttach = ((uint)(3));
			w6.BottomAttach = ((uint)(4));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelSendingDuration = new global::Gtk.Label();
			this.labelSendingDuration.Name = "labelSendingDuration";
			this.labelSendingDuration.Xalign = 1F;
			this.labelSendingDuration.LabelProp = global::Mono.Unix.Catalog.GetString("Время работы:");
			this.table1.Add(this.labelSendingDuration);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1[this.labelSendingDuration]));
			w7.TopAttach = ((uint)(4));
			w7.BottomAttach = ((uint)(5));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelSubject = new global::Gtk.Label();
			this.labelSubject.Name = "labelSubject";
			this.labelSubject.Xalign = 1F;
			this.labelSubject.LabelProp = global::Mono.Unix.Catalog.GetString("Тема:");
			this.table1.Add(this.labelSubject);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1[this.labelSubject]));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelText = new global::Gtk.Label();
			this.labelText.Name = "labelText";
			this.labelText.Xalign = 1F;
			this.labelText.LabelProp = global::Mono.Unix.Catalog.GetString("Текст:");
			this.table1.Add(this.labelText);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1[this.labelText]));
			w9.TopAttach = ((uint)(1));
			w9.BottomAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yentrySubject = new global::Gamma.GtkWidgets.yEntry();
			this.yentrySubject.CanFocus = true;
			this.yentrySubject.Name = "yentrySubject";
			this.yentrySubject.IsEditable = true;
			this.yentrySubject.InvisibleChar = '•';
			this.table1.Add(this.yentrySubject);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1[this.yentrySubject]));
			w10.LeftAttach = ((uint)(1));
			w10.RightAttach = ((uint)(2));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabelAttachmentsInfo = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelAttachmentsInfo.Name = "ylabelAttachmentsInfo";
			this.ylabelAttachmentsInfo.Xalign = 0F;
			this.ylabelAttachmentsInfo.LabelProp = global::Mono.Unix.Catalog.GetString("0/15Мб");
			this.table1.Add(this.ylabelAttachmentsInfo);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabelAttachmentsInfo]));
			w11.TopAttach = ((uint)(2));
			w11.BottomAttach = ((uint)(3));
			w11.LeftAttach = ((uint)(2));
			w11.RightAttach = ((uint)(3));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabelRecepientInfo = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelRecepientInfo.Name = "ylabelRecepientInfo";
			this.ylabelRecepientInfo.Xalign = 0F;
			this.ylabelRecepientInfo.LabelProp = global::Mono.Unix.Catalog.GetString("0/1000 за раз (0 адресатов уже получали письмо массовой рассылки в течение послед" +
					"них 2 часов) и 0/20000 в месяц писем вида массовой рассылки");
			this.ylabelRecepientInfo.Wrap = true;
			this.ylabelRecepientInfo.Selectable = true;
			this.ylabelRecepientInfo.WidthChars = 100;
			this.table1.Add(this.ylabelRecepientInfo);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabelRecepientInfo]));
			w12.TopAttach = ((uint)(3));
			w12.BottomAttach = ((uint)(4));
			w12.LeftAttach = ((uint)(1));
			w12.RightAttach = ((uint)(2));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabelSendingDuration = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelSendingDuration.Name = "ylabelSendingDuration";
			this.ylabelSendingDuration.Xalign = 0F;
			this.ylabelSendingDuration.LabelProp = global::Mono.Unix.Catalog.GetString("0 секунд");
			this.table1.Add(this.ylabelSendingDuration);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabelSendingDuration]));
			w13.TopAttach = ((uint)(4));
			w13.BottomAttach = ((uint)(5));
			w13.LeftAttach = ((uint)(1));
			w13.RightAttach = ((uint)(2));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabelSubjectInfo = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelSubjectInfo.Name = "ylabelSubjectInfo";
			this.ylabelSubjectInfo.Xalign = 0F;
			this.ylabelSubjectInfo.LabelProp = global::Mono.Unix.Catalog.GetString("0/255 символов");
			this.table1.Add(this.ylabelSubjectInfo);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabelSubjectInfo]));
			w14.LeftAttach = ((uint)(2));
			w14.RightAttach = ((uint)(3));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yprogressbarSending = new global::Gamma.GtkWidgets.yProgressBar();
			this.yprogressbarSending.Name = "yprogressbarSending";
			this.table1.Add(this.yprogressbarSending);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table1[this.yprogressbarSending]));
			w15.TopAttach = ((uint)(5));
			w15.BottomAttach = ((uint)(6));
			w15.RightAttach = ((uint)(3));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(0));
			w1.Add(this.table1);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(w1[this.table1]));
			w16.Position = 0;
			// Internal child Vodovoz.Views.BulkEmailView.ActionArea
			global::Gtk.HButtonBox w17 = this.ActionArea;
			w17.Name = "dialog1_ActionArea";
			w17.Spacing = 10;
			w17.BorderWidth = ((uint)(5));
			w17.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonSend = new global::Gtk.Button();
			this.buttonSend.CanDefault = true;
			this.buttonSend.CanFocus = true;
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.UseUnderline = true;
			this.buttonSend.Label = global::Mono.Unix.Catalog.GetString("Запустить рассылку");
			w17.Add(this.buttonSend);
			global::Gtk.ButtonBox.ButtonBoxChild w18 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w17[this.buttonSend]));
			w18.Expand = false;
			w18.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 945;
			this.DefaultHeight = 441;
			this.yprogressbarSending.Hide();
			this.Show();
		}
	}
}
