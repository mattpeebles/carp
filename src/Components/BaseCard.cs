namespace Carp.Components
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Components;
	
	public class BaseCard : ComponentBase
	{
		[Parameter]
		public HashSet<string> ClassSet { get; set; }

		public string GetTypeClass(string typeName)
		{
			return ClassSet?.Contains(typeName) == true ? "link" : "understate";
		}
	}
}
