namespace GranieManager.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Skill {get; set;}
    public int Stress {get; set;}
    public int Fatigue { get; set; }
    public int Points { get; set; }
    public string Game { get; set; }
    public decimal Money { get; set; }
}