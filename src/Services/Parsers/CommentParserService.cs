using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carp.Utility;


namespace Carp.Services.Parsers
{
	public interface ICommentParserService
	{
		DocumentationComment? GetSingleLineComment(SyntaxNode method);

		/// <summary>
		/// Simple comment
		/// </summary>
		/// <param name="argument1">first argument with type of <see cref="string"/></param> 
		/// <returns>a simple <see cref="string"/></returns>
		string TestCommentStructure(string argument1);
	}

	public class CommentParserService : ICommentParserService
	{
		public CommentParserService()
		{

		}

		public DocumentationComment? GetSingleLineComment(SyntaxNode method)
		{
			var trivia = method.GetLeadingTrivia().FirstOrDefault(t => t.Kind() == SyntaxKind.SingleLineDocumentationCommentTrivia);
			return trivia == default ? null : Translate(trivia);
		}

		/// <inheritdoc />> 
		public string TestCommentStructure(string argument1)
		{
			return argument1;
		}

		private DocumentationComment Translate(SyntaxTrivia trivia)
		{
			var comment = new DocumentationComment()
			{
				Params = new List<DocumentationParam>()
			};

			var xmlElements = trivia.GetStructure()?.ChildNodes().OfType<XmlElementSyntax>();

			foreach (var xmlElement in xmlElements)
			{
				switch (xmlElement.StartTag.Name.ToString())
				{
					case XmlCommentTags.Summary:
						comment.Summary = GetContent(xmlElement);
						break;
					case XmlCommentTags.Parameter:
						var paramInfo = new DocumentationParam();
						var nameAttribute = xmlElement.StartTag.ChildNodes().FirstOrDefault(_ => _ is XmlNameAttributeSyntax);
						
						paramInfo.ArgumentName = ((IdentifierNameSyntax)nameAttribute?.ChildNodes().FirstOrDefault(_ => _ is IdentifierNameSyntax))?.Identifier.ValueText;
						paramInfo.ArgumentComment = GetContent(xmlElement);
						comment.Params.Add(paramInfo);
						break;
				}
			}

			return comment;
		}

		private static string GetContent(XmlElementSyntax comment)
		{
			return comment.Content.ToFullString().Replace("///", "");
		}
	}

	public class DocumentationComment
	{
		public string Summary { get; set; }

		public List<DocumentationParam> Params { get; set; }
	}

	public class DocumentationParam
	{
		public string ArgumentName { get; set; }

		public string ArgumentComment { get; set; }
	}
}
