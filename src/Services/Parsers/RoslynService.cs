using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Carp.Parsers
{
    public interface IRosalynService
    {
        CompilationUnitSyntax ParseFile(string file);
        
        CompilationUnitSyntax ParseFile(Stream file);

        CompilationUnitSyntax ParseFile(byte[] file);
    }

    public class RoslynService : IRosalynService
    {
        public RoslynService()
        {

        }

        /// <summary>
        /// Gets tree root for a particular file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public CompilationUnitSyntax ParseFile(string file)
        {
            var options = new CSharpParseOptions(LanguageVersion.Default, DocumentationMode.Diagnose, SourceCodeKind.Regular);
            var tree = CSharpSyntaxTree.ParseText(file, options);
            return tree.GetCompilationUnitRoot();
        }

        public CompilationUnitSyntax ParseFile(Stream file)
        {
            var fileString = file.ToString();
            return ParseFile(fileString);
        }

        public CompilationUnitSyntax ParseFile(byte[] file)
        {
            var fileString = file.ToString();
            return ParseFile(fileString);
        }
    }
}
