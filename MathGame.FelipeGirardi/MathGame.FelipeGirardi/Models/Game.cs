namespace MathGame.FelipeGirardi.Models;
internal class Game
{
    internal List<Question> Questions { get; set; }
    internal int TotalPoints { get; set; }
    internal int TotalTime { get; set; }

    internal Game()
    {
        Questions = new List<Question>();
        TotalPoints = 0;
    }
}

