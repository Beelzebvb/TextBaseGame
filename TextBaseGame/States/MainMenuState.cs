using System;
using System.Collections.Generic;
using TextBaseGame.Manager;
using TextBaseGame.Utilities;

namespace TextBaseGame.States
{
    class MainMenuState : State
    {

        public MainMenuState(Stack<State> states) : base(states)
        {
            OnUpdateUI?.Invoke();
        }


        public override void Update()
        {
            ConsoleUI.ColorText("Choice : ", ConsoleColor.Green);
            string keyInput = Console.ReadLine();

            if (GetKeyInput(keyInput, "0"))
            {
                PopState();
            }
            else if (GetKeyInput(keyInput, "1"))
            {
                OnUpdateUI?.Invoke();
                Console.WriteLine("IMPLEMENT GAME STATE...");
                Console.ReadKey();
            }
            else if (GetKeyInput(keyInput, "2"))
            {
                PushState(new CreateCharacterState(States));
            }
            else if (GetKeyInput(keyInput, "3"))
            {
                if (SaveManager.Instance.SaveCount() > 0)
                {
                    PushState(new SelectCharacterState(States));
                }
                else
                {
                    OnUpdateUI?.Invoke();
                    ConsoleUI.Error("Unable to find any save, please create one...");
                }
            }
            else if (GetKeyInput(keyInput, "4"))
            {
                OnUpdateUI?.Invoke();
                Save();
            }
            else if (GetKeyInput(keyInput, "5"))
            {
                OnUpdateUI?.Invoke();
                Load();
            }
            else if (GetKeyInput(keyInput, "6"))
            {
                if (SaveManager.Instance.SaveCount() >= 1)
                    Delete();
                else
                    ConsoleUI.Error("There is no save to delete...");
            }
            else if (GetKeyInput(keyInput, "7"))
            {
                GameManager.Instance.PlayerCharacter.DisplayInfo();
                Console.WriteLine("Press Any Key...");
                Console.ReadKey();
                OnUpdateUI?.Invoke();
            }
            else
            {
                OnUpdateUI?.Invoke();
                ConsoleUI.Error("Please select a valid option...");
            }

        }

        private void Save()
        {
            SaveManager.Instance.Save(GameManager.Instance.PlayerCharacter, InputNumber());
        }
        private void Load()
        {
            SaveManager.Instance.Load(GameManager.Instance.PlayerCharacter, InputNumber());
        }
        private void Delete()
        {
            Console.Clear();

            ConsoleUI.AddHeader("Delete Save", ConsoleColor.Yellow);

            for (int i = 0; i < SaveManager.Instance.GetValidIndex().Length; i++)
            {
                int index = SaveManager.Instance.GetValidIndex()[i];
                string option = $"{SaveManager.Instance.GetSaveName(index)}";
                ConsoleUI.AddOption(option, $"{index}", ConsoleColor.Red);
            }


            SaveManager.Instance.DeleteAt(InputNumber());
            OnUpdateUI.Invoke();
        }


        public override void DisplayUI()
        {
            Console.Clear();
            string[] menuList =
            {
                "Start Game",
                "Create Character",
                "Select Character",
                "Save",
                "Load",
                "Delete Save",
                "Display Characteristics"
            };

            Console.WriteLine();

            ConsoleUI.AddHeader("Menu", ConsoleColor.Yellow);
            ConsoleUI.DrawLine(30, "_", ConsoleColor.Yellow);
            Console.WriteLine();
            ConsoleUI.AddOption("Quit", "0", ConsoleColor.Red);
            ConsoleUI.AddList(menuList, 1, ConsoleColor.Cyan);
            Console.WriteLine();
            ConsoleUI.DrawLine(30, "_", ConsoleColor.Yellow);
            Console.WriteLine();

        }

    }
}
