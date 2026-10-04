using System.Text.Json.Serialization;

namespace api.Services;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ErrorCode
{
    ValidationError,
    NotFound,
    UniqueViolation,
    ForeignKeyViolation,
    CheckViolation,
    NotNullViolation,
    Unauthorized,
    Forbidden,
    InternalError
}