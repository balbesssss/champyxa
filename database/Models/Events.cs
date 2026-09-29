using api.DB.Models;

namespace api.database.Models;

public class Events
{
    public Guid Id { get; set; }
    public string EventType { get; set; }
    public string EntityType { get; set; }
    public Guid EntityId { get; set; }
    public User UserId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
}
