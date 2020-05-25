
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

        //        private const string _program = @"using System;
        //using System.Collections;
        //using System.Linq;
        //using System.Text;

        //namespace HelloWorld
        //{

        //    /// <summary>
        //    /// We're looking at this
        //    /// </summary>
        //    /// <param name=""args"">your arguments</param>
        //    class Program
        //    {
        //        static void Main(string[] args)
        //        {
        //            Console.WriteLine(""Hello, World!"");
        //        }
        //    }
        //}";
        public string ProgramText { get; set; }

        /// <summary>
        /// yo 
        /// <para>Hey</para> 
        /// </summary>
        /// <returns></returns>
        public async Task<ProgramStructure> AnalyzeStructureAsync()
        {
            ProgramText = await _client.GetStringAsync("https://raw.githubusercontent.com/mattpeebles/carp/master/src/AnalysisService.cs");

            var root = _rosalyn.ParseFile(ProgramText);

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
