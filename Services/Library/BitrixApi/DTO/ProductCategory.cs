﻿using Newtonsoft.Json;

namespace BitrixApi.DTO
{
	public class ProductCategory
	{
		[JsonProperty("valueId")] 
		public string ValueId { get; set; }

		[JsonProperty("value")] 
		public string IsOurProduct { get; set; }
	}
}
