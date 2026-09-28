namespace api.DB.Models;

public class TechStep
{
    public Guid Id { get; set; }
    public TechCard TechCard { get; set; }
    public int Order { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public bool IsMandatory { get; set; }
    public string Instruction { get; set; }
}