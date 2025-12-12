namespace MathGame.FelipeGirardi.Models;
internal class Question
{
    internal int Operand1 { get; set; }
    internal char Operation { get; set; }
    internal int Operand2 { get; set; }
    internal int Result { get; set; }
    internal bool IsCorrect { get; set; }

    internal Question(int op1, char op, int op2, int result, bool isCorrect)
    {
        Operand1 = op1;
        Operation = op;
        Operand2 = op2; 
        Result = result;
        IsCorrect = isCorrect;
    }
}

