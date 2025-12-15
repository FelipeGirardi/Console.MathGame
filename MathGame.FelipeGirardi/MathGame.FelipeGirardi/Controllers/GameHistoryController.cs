using MathGame.FelipeGirardi.Models;
using Spectre.Console;

namespace MathGame.FelipeGirardi.Controllers;
internal class GameHistoryController
{
    public void ShowGameHistory()
    {
        List<Game> games = GameDatabase.Games;
        int nGames = 1;
        Console.Clear();

        if (games.Count == 0)
        {
            AnsiConsole.MarkupLine("No games were played yet.");
            AnsiConsole.MarkupLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
        else
        {
            foreach (Game game in games)
            {
                AnsiConsole.MarkupLine($"Game {nGames}:");
                foreach (Question question in game.Questions)
                {
                    AnsiConsole.MarkupLine($"[{(question.IsCorrect ? "blue" : "red")}] {question.Operand1} {question.Operation} {question.Operand2} = {question.Result} [/]");
                }
                AnsiConsole.MarkupLine($"Points = {game.TotalPoints}\n");
                AnsiConsole.MarkupLine($"Time taken = {game.TotalTime} seconds\n");
                nGames++;
            }

            AnsiConsole.MarkupLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
    }
}