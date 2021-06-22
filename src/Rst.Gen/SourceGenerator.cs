using Microsoft.CodeAnalysis;

namespace Rst.Gen
{
    [Generator]
    public class SourceGenerator : ISourceGenerator
    {
        private const string firstText = @"
using System;

namespace Rst.Gen
{
}

";
        
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }
        
        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not SyntaxReceiver) return;

            context.AddSource($"{nameof(SourceGenerator)}.cs", firstText);
        }
    }
}
