using System;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Rst.Auth.Utils;

[DebuggerDisplay("{ToString()}")]
public struct LTree
{
    private readonly ImmutableArray<string> _nodes;

    /// <summary>
    /// Constructs a new instance of <see cref="LTree"/>.
    /// </summary>
    /// <param name="value">The string value for the ltree.</param>
    public LTree(string value)
    {
        _nodes = value.Split('.').ToImmutableArray();
    }

    private LTree(ReadOnlySpan<string> slice)
    {
        _nodes = slice.ToArray().ToImmutableArray();
    }

    public static implicit operator LTree(string value) => new(value);
    public static implicit operator string(LTree ltree) => ltree.ToString();

    public override string ToString()
    {
        return string.Join('.', _nodes);
    }

    /// <summary>
    /// Returns whether this ltree is an ancestor of <paramref name="other"/> (or equal).
    /// </summary>
    /// <remarks>
    /// <p>The method call is translated to <c>left @&gt; right</c>.</p>
    /// <p>See https://www.postgresql.org/docs/current/ltree.html</p>
    /// </remarks>
    public bool IsAncestorOf(LTree other)
    {
        return _nodes.IndexOf(other, 0, new LQuery(other)) != -1;
    }

    /// <summary>
    /// Returns whether this ltree is a descendant of <paramref name="other"/> (or equal).
    /// </summary>
    /// <remarks>
    /// <p>The method call is translated to <c>left &lt;@ right</c>.</p>
    /// <p>See https://www.postgresql.org/docs/current/ltree.html</p>
    /// </remarks>
    public bool IsDescendantOf(LTree other)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns whether this ltree matches <paramref name="lquery"/>.
    /// </summary>
    /// <remarks>
    /// <p>The method call is translated to <c>left &lt;@ right</c>.</p>
    /// <p>See https://www.postgresql.org/docs/current/ltree.html</p>
    /// </remarks>
    public bool MatchesLQuery(string lquery)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns whether this ltree matches <paramref name="ltxtquery"/>.
    /// </summary>
    /// <remarks>
    /// <p>The method call is translated to <c>left @ right</c>.</p>
    /// <p>See https://www.postgresql.org/docs/current/ltree.html</p>
    /// </remarks>
    public bool MatchesLTxtQuery(string ltxtquery)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns subpath of this ltree from position <paramref name="start"/> to position <paramref name="end"/>-1
    /// (counting from 0).
    /// </summary>
    /// <remarks>
    /// <p>The method call is translated to <c>subltree(ltree, start, end)</c>.</p>
    /// <p>See https://www.postgresql.org/docs/current/ltree.html</p>
    /// </remarks>
    public LTree Subtree(int start, int end)
    {
        return new LTree(_nodes.AsSpan().Slice(start, end));
    }

    /// <summary>
    /// Returns subpath of this ltree starting at position <paramref name="offset"/>, with length <paramref name="len"/>.
    /// If <paramref name="offset"/> is negative, subpath starts that far from the end of the path.
    /// If <paramref name="len"/> is negative, leaves that many labels off the end of the path.
    /// </summary>
    /// <remarks>
    /// <p>The method call is translated to <c>subpath(ltree, offset, len)</c>.</p>
    /// <p>See https://www.postgresql.org/docs/current/ltree.html</p>
    /// </remarks>
    public Microsoft.EntityFrameworkCore.LTree Subpath(int offset, int len)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns subpath of ltree starting at position <paramref name="offset"/>, extending to end of path.
    /// If <paramref name="offset"/> is negative, subpath starts that far from the end of the path.
    /// </summary>
    /// <remarks>
    /// <p>The method call is translated to <c>subpath(ltree, offset)</c>.</p>
    /// <p>See https://www.postgresql.org/docs/current/ltree.html</p>
    /// </remarks>
    public Microsoft.EntityFrameworkCore.LTree Subpath(int offset)
    {
        return Subpath(offset, _nodes.Length);
    }

    /// <summary>
    /// Returns number of labels in path.
    /// </summary>
    /// <remarks>
    /// <p>The property is translated to <c>nlevel(ltree)</c>.</p>
    /// <p>See https://www.postgresql.org/docs/current/ltree.html</p>
    /// </remarks>
    public int NLevel => _nodes.Length;

    /// <summary>
    /// Returns position of first occurrence of <paramref name="other"/> in this ltree, or -1 if not found.
    /// </summary>
    /// <remarks>
    /// <p>The method call is translated to <c>index(ltree1, ltree2)</c>.</p>
    /// <p>See https://www.postgresql.org/docs/current/ltree.html</p>
    /// </remarks>
    public int Index(LTree other)
    {
        return _nodes.IndexOf(other, 0, new LQuery(other));
    }

    /// <summary>
    /// Returns position of first occurrence of <paramref name="other"/> in this ltree, or -1 if not found.
    /// The search starts at position <paramref name="offset"/>; negative offset means start -offset labels from the end of the path.
    /// </summary>
    /// <remarks>
    /// <p>The method call is translated to <c>index(ltree1, ltree2, offset)</c>.</p>
    /// <p>See https://www.postgresql.org/docs/current/ltree.html</p>
    /// </remarks>
    public int Index(Microsoft.EntityFrameworkCore.LTree other, int offset)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Computes longest common ancestor of paths.
    /// </summary>
    /// <remarks>
    /// <p>The method call is translated to <c>lca(others)</c>.</p>
    /// <p>See https://www.postgresql.org/docs/current/ltree.html</p>
    /// </remarks>
    public static Microsoft.EntityFrameworkCore.LTree LongestCommonAncestor(
        params Microsoft.EntityFrameworkCore.LTree[] others)
    {
        throw new NotImplementedException();
    }
}