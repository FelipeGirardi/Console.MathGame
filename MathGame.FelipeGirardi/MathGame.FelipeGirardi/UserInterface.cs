using Spectre.Console;
using MathGame.FelipeGirardi.Controllers;
using static MathGame.FelipeGirardi.Enums;

namespace MathGame.FelipeGirardi;
internal class UserInterface
{
    private readonly GameController _gameController = new();
    private readonly GameHistoryController _gameHistoryController = new();
    int nQuestions = 5;

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
                    ChooseGame(nQuestions);
                    break;
                case Options.RandomGame:
                    SetupRandomGame(nQuestions);
                    break;
                case Options.ViewHistory:
                    _gameHistoryController.ShowGameHistory();
                    break;
                case Options.Quit:
                    return;
            }
        }
    }

    internal void ChooseGame(int nQuestions)
    {

        Operations[] operations = new Operations[5];

        Console.Clear();

        var gameChoice = AnsiConsole.Prompt(
                new SelectionPrompt<Operations>()
                .Title("Choose a type of game:")
                .AddChoices(Enum.GetValues<Operations>()));

        for (int i = 0; i < nQuestions; i++)
        {
            operations[i] = gameChoice;
        }

        _gameController.StartGame(operations);
    }

    internal void SetupRandomGame(int nQuestions)
    {
        Operations[] operations = new Operations[5];
        Random random = new Random();
        Array opValues = Enum.GetValues(typeof(Operations));

        for (int i = 0; i < nQuestions; i++)
        {
            int randomOpNumber = random.Next(opValues.Length);
            operations[i] = (Operations)opValues.GetValue(randomOpNumber)!;
        }

        _gameController.StartGame(operations);
    }
}

