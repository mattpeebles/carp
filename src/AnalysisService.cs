
namespace Carp
{
    using Carp.Parsers;
    using Carp.Services.Parsers;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection.Metadata;
    using System.Threading.Tasks;

    public interface IAnalysisService
    {
        string ProgramText { get; }

        Task<ProgramStructure> AnalyzeStructureAsync();
        Task<CompilationUnitSyntax> GetProgramRoot();
    }

    public class AnalysisService : IAnalysisService
    {
        public AnalysisService(IRosalynService rosalyn, INamespaceParserService namespaceParser, HttpClient client)
        {
            _rosalyn = rosalyn;
            _namespaceParser = namespaceParser;
            _client = client;
        }

        private readonly IRosalynService _rosalyn;
        private readonly INamespaceParserService _namespaceParser;
        private readonly HttpClient _client;
        public string ProgramText { get; set; }


        public async Task<CompilationUnitSyntax> GetProgramRoot()
        {
            ProgramText = await _client.GetStringAsync("https://raw.githubusercontent.com/mattpeebles/carp/master/src/AnalysisService.cs");
            return _rosalyn.ParseFile(ProgramText);
        }

        /// <summary>
        /// yo 
        /// <para>Hey</para> 
        /// </summary>
        /// <returns></returns>
        public async Task<ProgramStructure> AnalyzeStructureAsync()
        {
            var root = await GetProgramRoot();

            var structure = new ProgramStructure()
            {
                Namespaces = new List<NamespaceStructure>()
            };

            foreach (var member in root.Members)
            {
                switch (member.Kind())
                {
                    case SyntaxKind.NamespaceDeclaration:
                        var nameSpaceInfo = _namespaceParser.Parse(member as NamespaceDeclarationSyntax);
                        structure.Namespaces.Add(nameSpaceInfo);
                        break;
                }
            }

            return structure;
        }
    }

    public class ProgramStructure
    {
        public List<NamespaceStructure> Namespaces { get; set; }
    }
}
