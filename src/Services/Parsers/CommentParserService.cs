using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carp.Services.Parsers
{
    public interface ICommentParserService
    {
        string GetSingleLineComment(SyntaxNode method);
        
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

        public string GetSingleLineComment(SyntaxNode method)
        {
            var trivia = method.GetLeadingTrivia().FirstOrDefault(t => t.Kind() == SyntaxKind.SingleLineDocumentationCommentTrivia);
            if (trivia == default) return null;
            return trivia.ToFullString();
        }

        /// <inheritdoc />>
        public string TestCommentStructure(string argument1)
        {
            return argument1;
        }
    }
}
