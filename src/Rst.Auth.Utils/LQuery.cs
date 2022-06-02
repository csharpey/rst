namespace Rst.Auth.Utils;

public class LQuery : IEqualityComparer<string>
{
    private readonly IList<string> _value;

    public LQuery() : this(string.Empty)
    {
    }
    public LQuery(string value)
    {
        _value = value.Split('.');
    }

    public LQuery(LTree lTree)
    {
        _value = lTree.ToString().Split('.');
    }

    public bool Equals(string? x, string? y)
    {
        throw new NotImplementedException();
    }

    public int GetHashCode(string obj)
    {
        return obj.GetHashCode();
    }
}