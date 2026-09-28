namespace api.DB.Models;

public class TechCard
{
    public Guid Id {get;set;}
    public Products ProductId {get;set;}
    public int Version { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}