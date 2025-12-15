using MathGame.FelipeGirardi.Models;
using Spectre.Console;
using System.Diagnostics;
using static MathGame.FelipeGirardi.Enums;

namespace MathGame.FelipeGirardi.Controllers;
internal class GameController
{
    public void StartGame(Operations[] operations)
    {
        Console.Clear();

        Game game = new Game();
        int totalPoints = 0;

        // set timer to track game time
        Stopwatch clock = Stopwatch.StartNew();

        foreach (Operations operation in operations)
        {
            int operand1, operand2, result;
            int questionNumber = 1;
            char operationChar;
            bool isAnswerCorrect;

            (operand1, operand2) = GenerateOperands(operation);
            result = GenerateResult(operand1, operand2, operation);
            operationChar = GetOperationChar(operation);

            var answer = AnsiConsole.Ask<int>($"[blue]Question {questionNumber}:[/] {operand1} {operationChar} {operand2} = ");

            if(answer == result)
            {
                AnsiConsole.MarkupLine($"[green]Correct answer![/] +1 point\n");
                isAnswerCorrect = true;
                totalPoints++;
            } else
            {
                AnsiConsole.MarkupLine($"[red]Wrong answer![/] The correct answer was {result}.\n");
                isAnswerCorrect = false;
            }

            // create Question object and add to Game object
            Question question = new Question(operand1, operationChar, operand2, answer, isAnswerCorrect);
            game.Questions.Add(question);

            questionNumber++;
        }

        // end timer and get game seconds
        clock.Stop();
        game.TotalTime = clock.Elapsed.Seconds;

        // get total points and store Game in mock database
        game.TotalPoints = totalPoints;
        GameDatabase.Games.Add(game);

        AnsiConsole.MarkupLine($"Game over! You scored [blue] {totalPoints} points.[/]");
        AnsiConsole.MarkupLine("Press any key to return to the menu.");
        Console.ReadKey();
    }

    public (int operand1, int operand2) GenerateOperands(Operations operation)
    {
        Random random = new Random();
        int operand1 = 0;
        int operand2 = 0;

        switch (operation)
        {
            // addition: operands between 0 and 100
            case Operations.Addition:
                operand1 = random.Next(0, 101);
                operand2 = random.Next(0, 101);
                break;

            // subtraction: operands between 0 and 100, operand1 must be bigger than operand2
            case Operations.Subtraction:
                operand1 = random.Next(0, 101);
                operand2 = random.Next(0, 101);

                if (operand2 > operand1)
                {
                    int temp = operand1;
                    operand1 = operand2;
                    operand2 = temp;
                }

                break;

            // multiplication: operands between 0 and 10
            case Operations.Multiplication:
                operand1 = random.Next(0, 11);
                operand2 = random.Next(0, 11);
                break;

            // division: operands between 0 and 100, operand1 must be divisible by operand2
            case Operations.Division:
                bool isDivisionValid = false;

                while(!isDivisionValid) {
                    operand1 = random.Next(0, 101);

                    for (int i = 0; i < 10; i++)
                    {
                        operand2 = random.Next(1, 101);
                        if (operand1 % operand2 == 0)
                        {
                            isDivisionValid = true;
                            break;
                        }
                    }
                }

                break;
        }

        return (operand1, operand2);
        
    }

    public int GenerateResult(int operand1, int operand2, Operations operation)
    {
        switch (operation)
        {
            case Operations.Addition:
                return operand1 + operand2;
            case Operations.Subtraction:
                return operand1 - operand2;
            case Operations.Multiplication:
                return operand1 * operand2;
            case Operations.Division:
                return operand1 / operand2;
            default:
                return 0;
        }
    }

    public char GetOperationChar(Operations operation)
    {
        switch (operation)
        {
            case Operations.Addition:
                return '+';
            case Operations.Subtraction:
                return '-';
            case Operations.Multiplication:
                return '*';
            case Operations.Division:
                return '/';
            default:
                return '+';
        }
    }
}
