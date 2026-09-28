using api.DB.Models;

namespace api.db.Models;

public class StepParameters 
{
    public Guid Id { get; set; }
    public TechStep TechStep { get; set; }
    public string Name {get;set;}
    public string Unit { get; set; }
    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; }
    public bool IsRequired { get; set; } = true;
}