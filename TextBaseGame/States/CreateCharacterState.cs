using System;
using System.Collections.Generic;
using TextBaseGame.Entity;
using TextBaseGame.Entity.Class;
using TextBaseGame.Manager;
using TextBaseGame.Utilities;

namespace TextBaseGame.States
{
    class CreateCharacterState : State
    {

        public CreateCharacterState(Stack<State> states) : base(states)
        {
            OnUpdateUI.Invoke();
            GameManager.Instance.PlayerCharacter = new Character();
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
                SetName();
            }
            else if (GetKeyInput(keyInput, "2"))
            {
                SetClass();
            }
            else if (GetKeyInput(keyInput, "3"))
            {
                SelectStats();
            }
            else
            {
                ConsoleUI.Error("\nPlease select a valid option...\n");
            }
        }

        void SetName()
        {
        InputName:
            Console.Write("Character Name : ");
            string name = Console.ReadLine();
            if (name == string.Empty) {
                OnUpdateUI?.Invoke();
                ConsoleUI.Error("\nPlease select a valid option...\n");
                goto InputName; 
            }

            GameManager.Instance.PlayerCharacter.Name = name;
            OnUpdateUI?.Invoke();
            ConsoleUI.Warning("Character name has been set to : " + GameManager.Instance.PlayerCharacter.Name);
            Console.WriteLine();
        }

        void SetClass()
        {
            Console.Clear();
            ConsoleUI.AddHeader("Select Class", ConsoleColor.Yellow);

            string[] classList =
            {
                "Warrior",
                "Mage",
                "Ranger",
                "Thief",
                "Priest",
                "Archer",
                "Orc",
                "Demon"
            };

            ConsoleUI.AddList(classList, 1, ConsoleColor.Cyan);

            CharacterClass characterClass = new Warrior();

            string classInput = Console.ReadLine();

            if (GetKeyInput(classInput, "1", "warrior"))
            {
                characterClass = new Warrior(250, 100, 100, 0);
            }
            else if (GetKeyInput(classInput, "2", "mage"))
            {
                characterClass = new Mage(200, 75, 75, 100);
            }
            else if (GetKeyInput(classInput, "3", "ranger"))
            {
                characterClass = new Ranger(200, 50, 100, 50); ;
            }
            else if (GetKeyInput(classInput, "4", "thief"))
            {
                characterClass = new Thief(150, 50, 125, 75);
            }

            GameManager.Instance.PlayerCharacter.CharacterClass = characterClass;

            OnUpdateUI?.Invoke();
            ConsoleUI.Warning("You have selected : " + characterClass.GetType().Name);
            Console.WriteLine();
        }

        void SelectStats()
        {
            
        }

        void Save()
        {
           //TODO override system or create new save
        }

        public override void DisplayUI()
        {
            Console.Clear();

            Console.WriteLine();

            ConsoleUI.AddHeader("Create a Character", ConsoleColor.Yellow);
            ConsoleUI.DrawLine(30, "_", ConsoleColor.Yellow);
            Console.WriteLine();

            ConsoleUI.AddOption("Menu",      "0", ConsoleColor.Red);
            ConsoleUI.AddOption("Set Name",  "1", ConsoleColor.Green);
            ConsoleUI.AddOption("Set Class", "2", ConsoleColor.Magenta);

            Console.WriteLine();
            ConsoleUI.DrawLine(30, "_", ConsoleColor.Yellow);
            Console.WriteLine();

        }

    }
}
