namespace api.DB.Models;

public class TechStep
{
    public Guid Id { get; set; }
    public TechCard TechCard 
    public int Order { get; set; }
    public string Name { get; set; }
    public string TypeStep { get; set; }
    public bool IsMandatory { get; set; }
    public string Instruction { get; set; }
}
