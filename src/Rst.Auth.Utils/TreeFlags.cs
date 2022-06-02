using System;

namespace Rst.Auth.Utils;

[Flags]
public enum TreeFlags
{
    Pattern = 1,
    Child = 2,
    All = Pattern | Child
}