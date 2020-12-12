using System;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using System.Text;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Collections.Generic;

namespace ReadonlyStructGenerator
{
    [Generator]
    public class ReadonlyStructGenerator : ISourceGenerator
    {
        private const string attributeSource = @"
using System;
namespace ReadonlyStructGenerator
{
    [AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class ReadonlyStructAttribute : Attribute
    {}
}";

        public void Execute(GeneratorExecutionContext context)
        {

            context.AddSource("ReadonlyStructCenerator.cs", SourceText.From(attributeSource, Encoding.UTF8));
            if (context.SyntaxReceiver is not SyntaxReceiver receiver)
            {
                return;
            }
            var options = (context.Compilation as CSharpCompilation).SyntaxTrees[0].Options as CSharpParseOptions;
            var compilation = context.Compilation.AddSyntaxTrees(
                CSharpSyntaxTree.ParseText(SourceText.From(attributeSource, Encoding.UTF8), options));
            var attributeSymbol = compilation.GetTypeByMetadataName("ReadonlyStructGenerator.ReadonlyStructAttribute");
            
            foreach (var candidate in receiver.CandidateStructs)
            {
                var model = compilation.GetSemanticModel(candidate.SyntaxTree);
                var typeSymbol = ModelExtensions.GetDeclaredSymbol(model, candidate);
                var attribute = typeSymbol.GetAttributes().FirstOrDefault(ad =>
                    ad.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default));
                if(attribute is not null)
                {
                    var namespaceName = typeSymbol.ContainingNamespace.ToDisplayString();
                    var structName = typeSymbol.Name;
                    var properties = candidate.Members
                        .OfType<PropertyDeclarationSyntax>()
                        .Where(prop=>prop.AccessorList?.Accessors.Any(acc=>acc.Kind() == SyntaxKind.InitAccessorDeclaration)??false)
                        .Select(prop => new { Type = prop.Type.ToString(), Name = prop.Identifier.Text });

                    var isConstructorDeclared = candidate.Members.Any(mem => mem.Kind() == SyntaxKind.ConstructorDeclaration);
                    var sb = new StringBuilder();
                    static string ToCamelCase(string name) => name.Length == 1 ? name.ToLower() : name.Substring(0,1).ToLower() + name.Substring(1);
                    var parameters = string.Join(",", properties.Select(p => p.Type + " " + ToCamelCase(p.Name)));
                    var propNames = string.Join(",", properties.Select(p => p.Name));
                    var argNames = string.Join(",", properties.Select(p => ToCamelCase(p.Name)));
                    var otherArgNams = string.Join(",", properties.Select(p => "other." + p.Name));
                    var sourceArgNames =  string.Join(",", properties.Select(p => "source." + p.Name));
                    var toStringParam = string.Join(",", properties.Select(p => "{" + p.Name + "}"));
                    sb.AppendLine($"#nullable enable");
                    sb.AppendLine($"using System;");
                    sb.AppendLine($"namespace {namespaceName}");
                    sb.AppendLine($"{{");
                    sb.AppendLine($"    readonly partial struct {structName} : IEquatable<{structName}>");
                    sb.AppendLine($"    {{");
                    if (!isConstructorDeclared)
                    {
                        sb.AppendLine($"        public {structName}({parameters}) => ({propNames}) = ({argNames});");
                    }
                    sb.AppendLine($"        public bool Equals({structName} other) => ({propNames}) == ({otherArgNams});");
                    sb.AppendLine($"        public override bool Equals(object? obj) => (obj is {structName} other) && Equals(other);");
                    sb.AppendLine($"        public override int GetHashCode() => HashCode.Combine({propNames});");
                    sb.AppendLine($"        public static bool operator ==({structName} left, {structName} right) => left.Equals(right);");
                    sb.AppendLine($"        public static bool operator !=({structName} left, {structName} right) => !(left == right);");
                    sb.AppendLine($"        public override string ToString() => $\"{{nameof({structName})}}({toStringParam})\";");
                    sb.AppendLine($"    }}");
                    sb.AppendLine($"}}");
                    context.AddSource($"{structName}_generated.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            //System.Diagnostics.Debugger.Launch();
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

    }

    internal class SyntaxReceiver : ISyntaxReceiver
    {
        internal List<StructDeclarationSyntax> CandidateStructs { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is StructDeclarationSyntax structDeclarationSyntax
                && structDeclarationSyntax.AttributeLists.Count > 0)
                CandidateStructs.Add(structDeclarationSyntax);
        }
    }


}
