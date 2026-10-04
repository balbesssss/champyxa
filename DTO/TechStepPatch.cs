namespace api.DTO;

public class TechStepPatch
{
    public Guid? TechCard ;
    public int? Order { get; set; }
    public string? Name { get; set; }
    public string? TypeStep { get; set; }
    public bool? IsMandatory { get; set; }
    public string? Instruction { get; set; }
}