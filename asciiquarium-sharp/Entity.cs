using System.Drawing;

namespace asciiquarium_sharp;

public class Entity
{
    public string Name { get; set; }
    public AquariumObjectTypes Type { get; set; }
    public string Shape { get; set; }
    public string Position { get; set; } //should change to some type of vector or array
    public Color DefaultColor { get; set; }
    public int Depth { get; set; }
    public int Physical { get; set; }
    public string? Color { get; set; }
    public int[]? CallbackArgs { get; set; }
    public TimeSpan? DieTime { get; set; }
    public string? DeathCb {get;set;}
}