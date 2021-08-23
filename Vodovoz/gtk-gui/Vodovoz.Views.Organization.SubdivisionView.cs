﻿
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Views.Organization
{
	public partial class SubdivisionView
	{
		private global::Gtk.Table table1;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entryChief;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entryDefaultSalesPlan;

		private global::Gtk.Frame frmWarehoses;

		private global::Gtk.Alignment GtkAlignment4;

		private global::Gtk.Label lblWarehouses;

		private global::Gtk.Label GtkLabel8;

		private global::Gtk.HBox hbox3;

		private global::Gtk.Notebook notebook1;

		private global::Vodovoz.Core.Permissions.SubdivisionEntityPermissionWidget subdivisionentitypermissionwidget;

		private global::Gtk.Label labelPermissions;

		private global::Gtk.ScrolledWindow GtkScrolledWindow2;

		private global::QSOrmProject.RepresentationTreeView repTreeChildSubdivisions;

		private global::Gtk.Label labelChildSubdivizions;

		private global::Gtk.VBox vboxDocuments;

		private global::Gtk.ScrolledWindow GtkScrolledWindow4;

		private global::Gamma.GtkWidgets.yTreeView ytreeviewDocuments;

		private global::Gtk.HBox hbox5;

		private global::Gamma.GtkWidgets.yButton buttonAddDocument;

		private global::Gamma.GtkWidgets.yButton buttonDeleteDocument;

		private global::Gtk.Label label3;

		private global::Vodovoz.Core.WidgetContainerView widgetcontainerview2;

		private global::Gtk.Label label4;

		private global::Vodovoz.Core.WidgetContainerView widgetcontainerview3;

		private global::Gtk.Label label5;

		private global::Gtk.HBox hbox4;

		private global::Gtk.Button buttonSave;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.HBox hboxtype;

		private global::Gamma.GtkWidgets.yLabel ylabelType;

		private global::Gamma.Widgets.yEnumComboBox yenumcomboType;

		private global::Gtk.Label label1;

		private global::Gtk.Label label2;

		private global::Gtk.Label labelShortName;

		private global::Gamma.GtkWidgets.yLabel lblDefaultSalesPlan;

		private global::Gamma.GtkWidgets.yLabel lblGeographicGroup;

		private global::Gtk.Label lblParent;

		private global::Gamma.GtkWidgets.yEntry yentryName;

		private global::Gamma.Widgets.yEntryReference yentryrefParentSubdivision;

		private global::Gamma.GtkWidgets.yEntry yentryShortName;

		private global::Gamma.Widgets.ySpecComboBox ySpecCmbGeographicGroup;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Views.Organization.SubdivisionView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Views.Organization.SubdivisionView";
			// Container child Vodovoz.Views.Organization.SubdivisionView.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table(((uint)(8)), ((uint)(3)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.entryChief = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entryChief.Events = ((global::Gdk.EventMask)(256));
			this.entryChief.Name = "entryChief";
			this.entryChief.CanEditReference = false;
			this.table1.Add(this.entryChief);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1[this.entryChief]));
			w1.TopAttach = ((uint)(3));
			w1.BottomAttach = ((uint)(4));
			w1.LeftAttach = ((uint)(1));
			w1.RightAttach = ((uint)(2));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryDefaultSalesPlan = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entryDefaultSalesPlan.Events = ((global::Gdk.EventMask)(256));
			this.entryDefaultSalesPlan.Name = "entryDefaultSalesPlan";
			this.entryDefaultSalesPlan.CanEditReference = false;
			this.table1.Add(this.entryDefaultSalesPlan);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.entryDefaultSalesPlan]));
			w2.TopAttach = ((uint)(6));
			w2.BottomAttach = ((uint)(7));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.frmWarehoses = new global::Gtk.Frame();
			this.frmWarehoses.Name = "frmWarehoses";
			this.frmWarehoses.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frmWarehoses.Gtk.Container+ContainerChild
			this.GtkAlignment4 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
			this.GtkAlignment4.Name = "GtkAlignment4";
			this.GtkAlignment4.LeftPadding = ((uint)(12));
			// Container child GtkAlignment4.Gtk.Container+ContainerChild
			this.lblWarehouses = new global::Gtk.Label();
			this.lblWarehouses.Name = "lblWarehouses";
			this.lblWarehouses.Xalign = 0F;
			this.lblWarehouses.LabelProp = global::Mono.Unix.Catalog.GetString("<b>?</b>");
			this.lblWarehouses.UseMarkup = true;
			this.lblWarehouses.Selectable = true;
			this.GtkAlignment4.Add(this.lblWarehouses);
			this.frmWarehoses.Add(this.GtkAlignment4);
			this.GtkLabel8 = new global::Gtk.Label();
			this.GtkLabel8.Name = "GtkLabel8";
			this.GtkLabel8.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Склады отдела:</b>");
			this.GtkLabel8.UseMarkup = true;
			this.frmWarehoses.LabelWidget = this.GtkLabel8;
			this.table1.Add(this.frmWarehoses);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.frmWarehoses]));
			w5.TopAttach = ((uint)(2));
			w5.BottomAttach = ((uint)(6));
			w5.LeftAttach = ((uint)(2));
			w5.RightAttach = ((uint)(3));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hbox3 = new global::Gtk.HBox();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.notebook1 = new global::Gtk.Notebook();
			this.notebook1.CanFocus = true;
			this.notebook1.Name = "notebook1";
			this.notebook1.CurrentPage = 0;
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.subdivisionentitypermissionwidget = new global::Vodovoz.Core.Permissions.SubdivisionEntityPermissionWidget();
			this.subdivisionentitypermissionwidget.Events = ((global::Gdk.EventMask)(256));
			this.subdivisionentitypermissionwidget.Name = "subdivisionentitypermissionwidget";
			this.notebook1.Add(this.subdivisionentitypermissionwidget);
			// Notebook tab
			this.labelPermissions = new global::Gtk.Label();
			this.labelPermissions.Name = "labelPermissions";
			this.labelPermissions.LabelProp = global::Mono.Unix.Catalog.GetString("Права доступа");
			this.notebook1.SetTabLabel(this.subdivisionentitypermissionwidget, this.labelPermissions);
			this.labelPermissions.ShowAll();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.GtkScrolledWindow2 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
			this.GtkScrolledWindow2.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
			this.repTreeChildSubdivisions = new global::QSOrmProject.RepresentationTreeView();
			this.repTreeChildSubdivisions.CanFocus = true;
			this.repTreeChildSubdivisions.Name = "repTreeChildSubdivisions";
			this.GtkScrolledWindow2.Add(this.repTreeChildSubdivisions);
			this.notebook1.Add(this.GtkScrolledWindow2);
			global::Gtk.Notebook.NotebookChild w8 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1[this.GtkScrolledWindow2]));
			w8.Position = 1;
			// Notebook tab
			this.labelChildSubdivizions = new global::Gtk.Label();
			this.labelChildSubdivizions.Name = "labelChildSubdivizions";
			this.labelChildSubdivizions.LabelProp = global::Mono.Unix.Catalog.GetString("Дочерние подразделения");
			this.notebook1.SetTabLabel(this.GtkScrolledWindow2, this.labelChildSubdivizions);
			this.labelChildSubdivizions.ShowAll();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.vboxDocuments = new global::Gtk.VBox();
			this.vboxDocuments.Name = "vboxDocuments";
			this.vboxDocuments.Spacing = 6;
			// Container child vboxDocuments.Gtk.Box+BoxChild
			this.GtkScrolledWindow4 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow4.Name = "GtkScrolledWindow4";
			this.GtkScrolledWindow4.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow4.Gtk.Container+ContainerChild
			this.ytreeviewDocuments = new global::Gamma.GtkWidgets.yTreeView();
			this.ytreeviewDocuments.CanFocus = true;
			this.ytreeviewDocuments.Name = "ytreeviewDocuments";
			this.GtkScrolledWindow4.Add(this.ytreeviewDocuments);
			this.vboxDocuments.Add(this.GtkScrolledWindow4);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vboxDocuments[this.GtkScrolledWindow4]));
			w10.Position = 0;
			// Container child vboxDocuments.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.buttonAddDocument = new global::Gamma.GtkWidgets.yButton();
			this.buttonAddDocument.CanFocus = true;
			this.buttonAddDocument.Name = "buttonAddDocument";
			this.buttonAddDocument.UseUnderline = true;
			this.buttonAddDocument.Label = global::Mono.Unix.Catalog.GetString("Добавить");
			this.hbox5.Add(this.buttonAddDocument);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.buttonAddDocument]));
			w11.Position = 0;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.buttonDeleteDocument = new global::Gamma.GtkWidgets.yButton();
			this.buttonDeleteDocument.CanFocus = true;
			this.buttonDeleteDocument.Name = "buttonDeleteDocument";
			this.buttonDeleteDocument.UseUnderline = true;
			this.buttonDeleteDocument.Label = global::Mono.Unix.Catalog.GetString("Удалить");
			this.hbox5.Add(this.buttonDeleteDocument);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.buttonDeleteDocument]));
			w12.Position = 1;
			w12.Expand = false;
			w12.Fill = false;
			this.vboxDocuments.Add(this.hbox5);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vboxDocuments[this.hbox5]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			this.notebook1.Add(this.vboxDocuments);
			global::Gtk.Notebook.NotebookChild w14 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1[this.vboxDocuments]));
			w14.Position = 2;
			// Notebook tab
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Документы");
			this.notebook1.SetTabLabel(this.vboxDocuments, this.label3);
			this.label3.ShowAll();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.widgetcontainerview2 = new global::Vodovoz.Core.WidgetContainerView();
			this.widgetcontainerview2.Events = ((global::Gdk.EventMask)(256));
			this.widgetcontainerview2.Name = "widgetcontainerview2";
			this.notebook1.Add(this.widgetcontainerview2);
			global::Gtk.Notebook.NotebookChild w15 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1[this.widgetcontainerview2]));
			w15.Position = 3;
			// Notebook tab
			this.label4 = new global::Gtk.Label();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("Предустановленные права");
			this.notebook1.SetTabLabel(this.widgetcontainerview2, this.label4);
			this.label4.ShowAll();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.widgetcontainerview3 = new global::Vodovoz.Core.WidgetContainerView();
			this.widgetcontainerview3.Events = ((global::Gdk.EventMask)(256));
			this.widgetcontainerview3.Name = "widgetcontainerview3";
			this.notebook1.Add(this.widgetcontainerview3);
			global::Gtk.Notebook.NotebookChild w15 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1[this.widgetcontainerview3]));
			w15.Position = 4;
			// Notebook tab
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("Права на склад");
			this.notebook1.SetTabLabel(this.widgetcontainerview3, this.label5);
			this.label5.ShowAll();
			this.hbox3.Add(this.notebook1);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.notebook1]));
			w16.Position = 0;
			this.table1.Add(this.hbox3);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table1[this.hbox3]));
			w17.TopAttach = ((uint)(7));
			w17.BottomAttach = ((uint)(8));
			w17.RightAttach = ((uint)(3));
			// Container child table1.Gtk.Table+TableChild
			this.hbox4 = new global::Gtk.HBox();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.buttonSave = new global::Gtk.Button();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString("Сохранить");
			global::Gtk.Image w18 = new global::Gtk.Image();
			w18.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-save", global::Gtk.IconSize.Menu);
			this.buttonSave.Image = w18;
			this.hbox4.Add(this.buttonSave);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.buttonSave]));
			w19.Position = 0;
			w19.Expand = false;
			w19.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString("Отменить");
			global::Gtk.Image w20 = new global::Gtk.Image();
			w20.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-revert-to-saved", global::Gtk.IconSize.Menu);
			this.buttonCancel.Image = w20;
			this.hbox4.Add(this.buttonCancel);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.buttonCancel]));
			w21.Position = 1;
			w21.Expand = false;
			w21.Fill = false;
			this.table1.Add(this.hbox4);
			global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.table1[this.hbox4]));
			w22.RightAttach = ((uint)(3));
			w22.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hboxtype = new global::Gtk.HBox();
			this.hboxtype.Name = "hboxtype";
			this.hboxtype.Spacing = 6;
			// Container child hboxtype.Gtk.Box+BoxChild
			this.ylabelType = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelType.Name = "ylabelType";
			this.ylabelType.Xalign = 1F;
			this.ylabelType.LabelProp = global::Mono.Unix.Catalog.GetString(" Тип:");
			this.hboxtype.Add(this.ylabelType);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.hboxtype[this.ylabelType]));
			w23.Position = 0;
			w23.Expand = false;
			w23.Fill = false;
			// Container child hboxtype.Gtk.Box+BoxChild
			this.yenumcomboType = new global::Gamma.Widgets.yEnumComboBox();
			this.yenumcomboType.Name = "yenumcomboType";
			this.yenumcomboType.ShowSpecialStateAll = false;
			this.yenumcomboType.ShowSpecialStateNot = false;
			this.yenumcomboType.UseShortTitle = false;
			this.yenumcomboType.DefaultFirst = false;
			this.hboxtype.Add(this.yenumcomboType);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.hboxtype[this.yenumcomboType]));
			w24.Position = 1;
			w24.Expand = false;
			w24.Fill = false;
			this.table1.Add(this.hboxtype);
			global::Gtk.Table.TableChild w25 = ((global::Gtk.Table.TableChild)(this.table1[this.hboxtype]));
			w25.TopAttach = ((uint)(1));
			w25.BottomAttach = ((uint)(2));
			w25.LeftAttach = ((uint)(2));
			w25.RightAttach = ((uint)(3));
			w25.XOptions = ((global::Gtk.AttachOptions)(4));
			w25.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 1F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Название подразделения:");
			this.table1.Add(this.label1);
			global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.table1[this.label1]));
			w26.TopAttach = ((uint)(1));
			w26.BottomAttach = ((uint)(2));
			w26.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.Xalign = 1F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Начальник подразделения:");
			this.table1.Add(this.label2);
			global::Gtk.Table.TableChild w27 = ((global::Gtk.Table.TableChild)(this.table1[this.label2]));
			w27.TopAttach = ((uint)(3));
			w27.BottomAttach = ((uint)(4));
			w27.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelShortName = new global::Gtk.Label();
			this.labelShortName.Name = "labelShortName";
			this.labelShortName.Xalign = 1F;
			this.labelShortName.LabelProp = global::Mono.Unix.Catalog.GetString("Сокращенное наименование:");
			this.table1.Add(this.labelShortName);
			global::Gtk.Table.TableChild w28 = ((global::Gtk.Table.TableChild)(this.table1[this.labelShortName]));
			w28.TopAttach = ((uint)(2));
			w28.BottomAttach = ((uint)(3));
			w28.XOptions = ((global::Gtk.AttachOptions)(4));
			w28.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.lblDefaultSalesPlan = new global::Gamma.GtkWidgets.yLabel();
			this.lblDefaultSalesPlan.Name = "lblDefaultSalesPlan";
			this.lblDefaultSalesPlan.Xalign = 1F;
			this.lblDefaultSalesPlan.LabelProp = global::Mono.Unix.Catalog.GetString("План продаж по умолчанию:");
			this.table1.Add(this.lblDefaultSalesPlan);
			global::Gtk.Table.TableChild w29 = ((global::Gtk.Table.TableChild)(this.table1[this.lblDefaultSalesPlan]));
			w29.TopAttach = ((uint)(6));
			w29.BottomAttach = ((uint)(7));
			w29.XOptions = ((global::Gtk.AttachOptions)(4));
			w29.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.lblGeographicGroup = new global::Gamma.GtkWidgets.yLabel();
			this.lblGeographicGroup.Name = "lblGeographicGroup";
			this.lblGeographicGroup.Xalign = 1F;
			this.lblGeographicGroup.LabelProp = global::Mono.Unix.Catalog.GetString("Местоположение подразделения:");
			this.table1.Add(this.lblGeographicGroup);
			global::Gtk.Table.TableChild w30 = ((global::Gtk.Table.TableChild)(this.table1[this.lblGeographicGroup]));
			w30.TopAttach = ((uint)(5));
			w30.BottomAttach = ((uint)(6));
			w30.XOptions = ((global::Gtk.AttachOptions)(4));
			w30.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.lblParent = new global::Gtk.Label();
			this.lblParent.Name = "lblParent";
			this.lblParent.Xalign = 1F;
			this.lblParent.LabelProp = global::Mono.Unix.Catalog.GetString("Вышестоящее подразделение:");
			this.table1.Add(this.lblParent);
			global::Gtk.Table.TableChild w31 = ((global::Gtk.Table.TableChild)(this.table1[this.lblParent]));
			w31.TopAttach = ((uint)(4));
			w31.BottomAttach = ((uint)(5));
			w31.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yentryName = new global::Gamma.GtkWidgets.yEntry();
			this.yentryName.CanFocus = true;
			this.yentryName.Name = "yentryName";
			this.yentryName.IsEditable = true;
			this.yentryName.InvisibleChar = '●';
			this.table1.Add(this.yentryName);
			global::Gtk.Table.TableChild w32 = ((global::Gtk.Table.TableChild)(this.table1[this.yentryName]));
			w32.TopAttach = ((uint)(1));
			w32.BottomAttach = ((uint)(2));
			w32.LeftAttach = ((uint)(1));
			w32.RightAttach = ((uint)(2));
			w32.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yentryrefParentSubdivision = new global::Gamma.Widgets.yEntryReference();
			this.yentryrefParentSubdivision.Events = ((global::Gdk.EventMask)(256));
			this.yentryrefParentSubdivision.Name = "yentryrefParentSubdivision";
			this.table1.Add(this.yentryrefParentSubdivision);
			global::Gtk.Table.TableChild w33 = ((global::Gtk.Table.TableChild)(this.table1[this.yentryrefParentSubdivision]));
			w33.TopAttach = ((uint)(4));
			w33.BottomAttach = ((uint)(5));
			w33.LeftAttach = ((uint)(1));
			w33.RightAttach = ((uint)(2));
			w33.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yentryShortName = new global::Gamma.GtkWidgets.yEntry();
			this.yentryShortName.CanFocus = true;
			this.yentryShortName.Name = "yentryShortName";
			this.yentryShortName.IsEditable = true;
			this.yentryShortName.MaxLength = 20;
			this.yentryShortName.InvisibleChar = '●';
			this.table1.Add(this.yentryShortName);
			global::Gtk.Table.TableChild w34 = ((global::Gtk.Table.TableChild)(this.table1[this.yentryShortName]));
			w34.TopAttach = ((uint)(2));
			w34.BottomAttach = ((uint)(3));
			w34.LeftAttach = ((uint)(1));
			w34.RightAttach = ((uint)(2));
			w34.XOptions = ((global::Gtk.AttachOptions)(4));
			w34.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ySpecCmbGeographicGroup = new global::Gamma.Widgets.ySpecComboBox();
			this.ySpecCmbGeographicGroup.Name = "ySpecCmbGeographicGroup";
			this.ySpecCmbGeographicGroup.AddIfNotExist = false;
			this.ySpecCmbGeographicGroup.DefaultFirst = false;
			this.ySpecCmbGeographicGroup.ShowSpecialStateAll = false;
			this.ySpecCmbGeographicGroup.ShowSpecialStateNot = true;
			this.table1.Add(this.ySpecCmbGeographicGroup);
			global::Gtk.Table.TableChild w35 = ((global::Gtk.Table.TableChild)(this.table1[this.ySpecCmbGeographicGroup]));
			w35.TopAttach = ((uint)(5));
			w35.BottomAttach = ((uint)(6));
			w35.LeftAttach = ((uint)(1));
			w35.RightAttach = ((uint)(2));
			w35.XOptions = ((global::Gtk.AttachOptions)(4));
			w35.YOptions = ((global::Gtk.AttachOptions)(4));
			this.Add(this.table1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
