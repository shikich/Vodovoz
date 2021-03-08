﻿using System;
using QS.BusinessCommon.Domain;
using System.Collections.Generic;
using Vodovoz.Domain.Common;

namespace Vodovoz.OldExportTo1c.Catalogs
{
	public class MeasurementUnitsCatalog:GenericCatalog<MeasurementUnit>
	{
		public MeasurementUnitsCatalog(ExportData exportData)
			:base(exportData)
		{			
		}

		protected override string Name
		{
			get{return "КлассификаторЕдиницИзмерения";}
		}
		public override ReferenceNode CreateReferenceTo(MeasurementUnit unit)
		{			
			int id = GetReferenceId(unit);
			return new ReferenceNode(id,
				new PropertyNode("Код",
					Common1cTypes.String,
					unit.Id
				)
			);
		}
		protected override PropertyNode[] GetProperties(MeasurementUnit unit)
		{
			var properties = new List<PropertyNode>();
			properties.Add(
				new PropertyNode("Наименование",
					Common1cTypes.String,
					unit.Name
				)
			);
			properties.Add(
				new PropertyNode("НаименованиеПолное",
					Common1cTypes.String,
					unit.Name
				)
			);
			return properties.ToArray();
		}
	}
}

