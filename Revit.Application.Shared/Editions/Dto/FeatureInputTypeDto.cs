using System.Collections.Generic;
using Abp.Runtime.Validation;

namespace Revit.Editions.Dto
{ 
	public class FeatureInputTypeDto
	{
		public string Name { get; set; }

		public IDictionary<string, object> Attributes { get; set; }

		//public IValueValidator Validator { get; set; }

		public LocalizableComboboxItemSourceDto ItemSource { get; set; }
	}
}