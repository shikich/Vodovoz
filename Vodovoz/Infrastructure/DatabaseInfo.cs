﻿using System;
using QS.Project.Versioning;

namespace Vodovoz.Infrastructure
{
	public class DatabaseInfo : IDataBaseInfo
	{
		public string Name { get; set; }
		public bool IsDemo { get; set; }
		
		public Guid? BaseGuid { get; }

		public DatabaseInfo(string name, bool isDemo)
		{
			Name = name;
			IsDemo = isDemo;
		}
	}
}
