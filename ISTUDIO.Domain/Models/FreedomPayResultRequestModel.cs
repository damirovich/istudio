using System.Text.Json;

namespace ISTUDIO.Domain.Models;

public class FreedomPayResultRequestModel
{
    private readonly Dictionary<string, string?> _parameters = new Dictionary<string, string?>();

    public string? this[string key]
    {
        get => _parameters.ContainsKey(key) ? _parameters[key] : null;
        set => _parameters[key] = value;
    }

    public void AddParameter(string key, string? value)
    {
        _parameters[key] = value;
    }

    public IReadOnlyDictionary<string, string?> GetParameters() => _parameters;

    public string ToJson() => JsonSerializer.Serialize(_parameters);

    public void ValidateRequiredFields()
    {
        if (string.IsNullOrEmpty(this["pg_result"]))
        {
            throw new ArgumentException("pg_result is required");
        }
        if (string.IsNullOrEmpty(this["pg_sig"]))
        {
            throw new ArgumentException("pg_sig is required");
        }
    }
}
