using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Web;

namespace Carp
{
    public static class Analysis
    {
        public const string programText =
        @"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{

    /// <summary>
    /// We're looking at this
    /// </summary>
    /// <param name=""args"">your arguments</param>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
        }
    }
}";

        /// <summary>
        /// yo 
        /// <para>Hey</para> 
        /// </summary>
        /// <returns></returns>
        public static string FirstFunctionName()
        {

            var options = new CSharpParseOptions(LanguageVersion.Default, DocumentationMode.Diagnose, SourceCodeKind.Regular);

            var tree = CSharpSyntaxTree.ParseText(programText, options);
            var root = tree.GetCompilationUnitRoot();

            var namespaceDeclaration = root.Members.FirstOrDefault(_ => _.Kind() == SyntaxKind.NamespaceDeclaration) as NamespaceDeclarationSyntax;
            var classDefinition = namespaceDeclaration.Members.FirstOrDefault(_ => _.Kind() == SyntaxKind.ClassDeclaration) as ClassDeclarationSyntax;
            var methodDefinition = classDefinition.Members.FirstOrDefault(_ => _.Kind() == SyntaxKind.MethodDeclaration) as MethodDeclarationSyntax;
            return methodDefinition.Identifier.Value as string;
        }
    }
}
