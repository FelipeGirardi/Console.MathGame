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
        int questionNumber = 1;

        // set timer to track game time
        Stopwatch clock = Stopwatch.StartNew();

        foreach (Operations operation in operations)
        {
            int operand1, operand2, result;
            char operationChar;
            bool isAnswerCorrect;
            Difficulty difficulty = GetDifficulty(questionNumber);

            (operand1, operand2) = GenerateOperands(operation, difficulty);
            result = GenerateResult(operand1, operand2, operation);
            operationChar = GetOperationChar(operation);

            string difficultyString = GetDifficultyString(difficulty);
            var answer = AnsiConsole.Ask<int>($"{difficultyString} Question {questionNumber}:[/] {operand1} {operationChar} {operand2} = ");

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

    public (int operand1, int operand2) GenerateOperands(Operations operation, Difficulty difficulty)
    {
        Random random = new Random();
        int operand1 = 0;
        int operand2 = 0;
        int minValue, maxValue;

        switch (operation)
        {
            // addition: operands between 0 and 100
            case Operations.Addition:
                minValue = difficulty == Difficulty.Easy ? 0 : difficulty == Difficulty.Medium ? 100 : 500;
                maxValue = difficulty == Difficulty.Easy ? 101 : difficulty == Difficulty.Medium ? 501 : 1001;
                operand1 = random.Next(minValue, maxValue);
                operand2 = random.Next(minValue, maxValue);
                break;

            // subtraction: operands between 0 and 100, operand1 must be bigger than operand2
            case Operations.Subtraction:
                minValue = difficulty == Difficulty.Easy ? 0 : difficulty == Difficulty.Medium ? 100 : 500;
                maxValue = difficulty == Difficulty.Easy ? 101 : difficulty == Difficulty.Medium ? 501 : 1001;
                operand1 = random.Next(minValue, maxValue);
                operand2 = random.Next(minValue, maxValue);

                if (operand2 > operand1)
                {
                    int temp = operand1;
                    operand1 = operand2;
                    operand2 = temp;
                }

                break;

            // multiplication: operands between 0 and 10
            case Operations.Multiplication:
                minValue = difficulty == Difficulty.Easy ? 0 : difficulty == Difficulty.Medium ? 10 : 25;
                maxValue = difficulty == Difficulty.Easy ? 11 : difficulty == Difficulty.Medium ? 25 : 50;
                operand1 = random.Next(minValue, maxValue);
                operand2 = random.Next(minValue, maxValue);
                break;

            // division: operands between 0 and 100, operand1 must be divisible by operand2
            case Operations.Division:
                bool isDivisionValid = false;
                minValue = difficulty == Difficulty.Easy ? 1 : difficulty == Difficulty.Medium ? 4 : 8;
                maxValue = difficulty == Difficulty.Easy ? 101 : difficulty == Difficulty.Medium ? 501 : 1001;

                while(!isDivisionValid) {
                    operand1 = random.Next(minValue, maxValue);

                    for (int i = 0; i < 10; i++)
                    {
                        operand2 = random.Next(minValue, maxValue);
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

    public string GetDifficultyString(Difficulty difficulty)
    {
        return difficulty == Difficulty.Easy ? "[blue](Easy)" :
               difficulty == Difficulty.Medium ? "[olive](Medium)" : "[maroon](Hard)";
    }

    public Difficulty GetDifficulty(int questionNumber)
    {
        return (questionNumber == 1 || questionNumber == 2) ? Difficulty.Easy :
               (questionNumber == 3 || questionNumber == 4) ? Difficulty.Medium : Difficulty.Hard;
    }
}
