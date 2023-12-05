namespace NetHttp;

public class GuidProvider
{
    public string GuidValue { get; } = Guid.NewGuid().ToString();
}
