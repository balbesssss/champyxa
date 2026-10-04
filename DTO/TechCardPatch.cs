using System;

namespace api.DTO;

public class TechCardPatch
{
    public Guid? ProductId {get;set;}
    public int? Version { get; set; }
    public string? Status { get; set; }
}
