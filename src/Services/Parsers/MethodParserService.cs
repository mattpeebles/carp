using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Carp.Services.Parsers
{
    public interface IMethodParserService
    {
        MethodStructure GetMethodName(MethodDeclarationSyntax node);
    }

    public class MethodParserService : IMethodParserService
    {
        public MethodParserService()
        {

        }

        public MethodStructure GetMethodName(MethodDeclarationSyntax node)
        {
            var method = new MethodStructure()
            {
                Name = node.Identifier.ValueText
            };

            return method;
        }

    }

    public class MethodStructure
    {
        public string Name { get; set; }
    }
}
