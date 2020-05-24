using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Carp.Services.Parsers
{
    public interface IClassParserService
    {
        ClassStructure Parse(ClassDeclarationSyntax node);
    }

    public class ClassParserService : IClassParserService
    {
        public ClassParserService(IMethodParserService methodParser)
        {
            _methodParser = methodParser;
        }

        private readonly IMethodParserService _methodParser;


        public ClassStructure Parse(ClassDeclarationSyntax node)
        {
            var structure = new ClassStructure()
            {
                Name = node.Identifier.ValueText,
                Methods = new List<MethodStructure>()
            };

            foreach (var member in node.Members)
            {
                switch (member.Kind())
                {
                    case SyntaxKind.MethodDeclaration:
                        structure.Methods.Add(_methodParser.GetMethodName(member as MethodDeclarationSyntax));
                        break;
                }
            }

            return structure;
        }
    }

    public class ClassStructure
    {
        public string Name { get; set; }

        public List<MethodStructure> Methods { get; set; }
    }
}
