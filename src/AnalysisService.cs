
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

        Task<CompilationUnitSyntax> GetProgramRoot(string sourcePath);
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


        public async Task<CompilationUnitSyntax> GetProgramRoot(string sourcePath)
        {
            ProgramText = await _client.GetStringAsync(sourcePath);
            return _rosalyn.ParseFile(ProgramText);
        }
    }

    public class ProgramStructure
    {
        public List<NamespaceStructure> Namespaces { get; set; }
    }
}
