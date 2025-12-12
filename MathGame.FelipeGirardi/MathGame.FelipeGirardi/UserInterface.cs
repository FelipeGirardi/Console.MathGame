using Spectre.Console;
using MathGame.FelipeGirardi.Controllers;
using static MathGame.FelipeGirardi.Enums;

namespace MathGame.FelipeGirardi;
internal class UserInterface
{
    private readonly GameController _gameController = new();

    internal void MainMenu()
    {
        while (true)
        {
            Console.Clear();

            var menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<Options>()
                .Title("Choose an option:")
                .AddChoices(Enum.GetValues<Options>()));

            switch (menuChoice)
            {
                case Options.StartGame:
                    ChooseGame();
                    break;
                case Options.ViewHistory:
                    //_gameHistoryController.ViewHistory();
                    break;
                case Options.Quit:
                    return;
            }
        }
    }

    internal void ChooseGame()
    {
        Console.Clear();

        var gameChoice = AnsiConsole.Prompt(
                new SelectionPrompt<Operations>()
                .Title("Choose a type of game:")
                .AddChoices(Enum.GetValues<Operations>()));

        _gameController.startGame(gameChoice);
    }
}

