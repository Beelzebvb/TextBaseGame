using System;
using System.Collections.Generic;
using TextBaseGame.Manager;

namespace TextBaseGame.States
{
    class MainMenuState : State
    {
        public MainMenuState(Stack<State> states) : base(states)
        {
            DisplayUI();
        }

        public override void Update()
        {
            Console.Write("Choice : ");
            string keyInput = Console.ReadLine();

            if (GetKeyInput(keyInput, "0"))
            {

                Console.WriteLine("Quit the Game");
                PopState();
            }
            else if (GetKeyInput(keyInput, "1"))
            {
                PushState(new CreateCharacterState(States));
            }
            else if (GetKeyInput(keyInput, "2"))
            {
                PushState(new SelectCharacterState(States));
            }
            else if (GetKeyInput(keyInput, "4"))
            {
                Save();
                DisplayUI();
            }
            else if (GetKeyInput(keyInput, "5"))
            {
                Load();
                DisplayUI();
                Console.WriteLine($"File at : {SaveManager.Instance.FilePath} has been loaded.");
            }
            else if (GetKeyInput(keyInput, "6"))
            {
                DisplayUI();
                GameManager.Instance.PlayerCharacter.DisplayInfo();
                Console.WriteLine("Press Any Key...");
            }
            else
            {
                Console.WriteLine("\n\nPlease select a valid option...\n");
            }

        }

        private void Save()
        {
            SaveManager.Instance.Save(GameManager.Instance.PlayerCharacter, InputNumber());
            Console.ReadLine();
        }
        private void Load()
        {
            SaveManager.Instance.Load(GameManager.Instance.PlayerCharacter, InputNumber());
            Console.ReadLine();
        }


        public int InputNumber()
        {
        InputNumber:
            Console.Write("Select a save number : ");
            string indexInput = Console.ReadLine();

            if (Int32.TryParse(indexInput, out int saveIndex))
            {
                return saveIndex;
            }
            else
            {
                Console.WriteLine("Please select a valid option...");
                goto InputNumber;
            }
        }


        public override void DisplayUI()
        {
            Console.Clear();

            string UI = "==============================\n" +
                        "=============MENU=============\n" +
                        "==============================\n\n" +
                        "0 : QUIT\n" +
                        "1 : Create Character\n" +
                        "2 : Select Character\n" +
                        "3 : Start Game\n" +
                        "4 : Save\n" +
                        "5 : Load\n" +
                        "6 : Display Current Character Info\n\n";


            Console.Write(UI);
        }

    }
}
