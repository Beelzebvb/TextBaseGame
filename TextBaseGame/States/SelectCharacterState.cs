using System;
using System.Collections.Generic;
using TextBaseGame.Manager;
using TextBaseGame.Utilities;

namespace TextBaseGame.States
{
    class SelectCharacterState : State
    {

        public SelectCharacterState(Stack<State> states) : base(states)
        {
            OnUpdateUI?.Invoke();
        }

        public override void Update()
        {
        MakeChoice:
            ConsoleUI.ColorText("Choice : ", ConsoleColor.Green);
            string keyInput = Console.ReadLine();

            int[] validIndex = SaveManager.Instance.GetValidIndex();

            if (int.TryParse(keyInput, out int numberInput))
            {
                if(numberInput == 0)
                {
                    PopState();
                    return;
                }
                foreach (var index in validIndex)
                {
                    if (numberInput == index)
                    {
                        ConsoleUI.Warning($"Character {numberInput} Selected");
                        SaveManager.Instance.Load(GameManager.Instance.PlayerCharacter, numberInput);
                        Console.ReadLine();
                        PopState();
                        return;
                    }
                }
            }
            ConsoleUI.Error("Please select a valid option...");
            goto MakeChoice;
        }
        public override void DisplayUI()
        {
            Console.Clear();

            Console.WriteLine();

            ConsoleUI.AddHeader("Select a Character", ConsoleColor.Yellow);
            ConsoleUI.DrawLine(30, "_", ConsoleColor.Yellow);

            Console.WriteLine();

            ConsoleUI.AddOption("Menu", "0", ConsoleColor.Red);

            for (int i = 0; i < SaveManager.Instance.GetSaveNames().Length; i++)
            {
                string name = SaveManager.Instance.GetSaveNames()[i];
                ConsoleUI.AddOption($"{name}", $"{SaveManager.Instance.GetSaveIndexOf(name)}", ConsoleColor.Cyan);
            }
            Console.WriteLine();
            ConsoleUI.DrawLine(30, "_", ConsoleColor.Yellow);
            Console.WriteLine();
        }
    }
}
