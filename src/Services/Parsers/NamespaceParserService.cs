
namespace Carp.Services.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.CSharp;

    public interface INamespaceParserService
    {
        NamespaceStructure Parse(NamespaceDeclarationSyntax node);
    }

    public class NamespaceParserService : INamespaceParserService 
    {
        public NamespaceParserService(IClassParserService classParser)
        {
            _classParser = classParser;
        }

        private readonly IClassParserService _classParser;

        public NamespaceStructure Parse(NamespaceDeclarationSyntax node)
        {
            var structure = new NamespaceStructure()
            {
                Name = node.Name.ToString(),
                Classes = new List<ClassStructure>()
            };

            foreach(var member in node.Members)
            {
                switch (member.Kind())
                {
                    case SyntaxKind.ClassDeclaration:
                        structure.Classes.Add(_classParser.Parse(member as ClassDeclarationSyntax));
                        break;
                } 
            }

            return structure;
        }
    }

    public class NamespaceStructure
    {
        public string Name { get; set; }
       
        public List<ClassStructure> Classes { get; set; }
    }
}
