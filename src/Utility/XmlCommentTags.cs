using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carp.Utility
{
	public static class XmlCommentTags
	{
		/**
		 *https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/recommended-tags-for-documentation-comments
		 */

		public const string CodeInline = "c";
		public const string CodeBlock = "code";
		public const string Example = "example";
		public const string Exception = "exception";
		public const string Include = "include";
		public const string List = "list";
		public const string Paragraph = "para";
		public const string Parameter = "param";
		public const string ParamRef = "paramref";
		public const string Permission = "permission";
		public const string Remarks = "remarks";
		public const string InheritDoc = "inheritdoc";
		public const string See = "see";
		public const string SeeAlso = "seealso";
		public const string Summary = "summary";
		public const string TypeParam = "typeparam";
		public const string TypeParamRef = "typeparamref";
		public const string Returns = "returns";
		public const string Value = "value";
	}
}