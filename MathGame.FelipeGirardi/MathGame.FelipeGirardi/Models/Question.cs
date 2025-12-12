namespace MathGame.FelipeGirardi.Models;
internal class Question
{
    internal int operand1 { get; set; }
    internal Enums.Operations operation { get; set; }
    internal int operand2 { get; set; }
    internal int result { get; set; }
    internal bool isCorrect { get; set; }
}

