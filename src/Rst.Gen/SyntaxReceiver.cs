using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rst.Gen
{
    internal class SyntaxReceiver : ISyntaxReceiver
    {
        public ICollection<MemberAccessExpressionSyntax> GenerateCandidates { get; }
        public SyntaxReceiver()
        {
            GenerateCandidates = new List<MemberAccessExpressionSyntax>();
        }
        
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is not MemberAccessExpressionSyntax syntax) return;
            if (syntax.HasTrailingTrivia || syntax.Name.IsMissing) return;
            GenerateCandidates.Add(syntax);
        }
    }
}