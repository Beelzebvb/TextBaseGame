using System;
using System.Collections.Generic;
using TextBaseGame.Entity.Class;
using TextBaseGame.Manager;

namespace TextBaseGame.States
{
    class CreateCharacterState : State
    {

        public CreateCharacterState(Stack<State> states) : base(states)
        {
            DisplayUI();
        }

        public override void DisplayUI()
        {
            Console.Clear();
            string UI = "==============================\n" +
                        "=======Create Character=======\n" +
                        "==============================\n\n" +
                        "0 : Menu\n" +
                        "1 : Name\n" +
                        "2 : Select Class\n" +
                        "4 : Save\n";


            Console.Write(UI);
            Console.WriteLine();
        }

        public override void Update()
        {

            Console.Write("Choice : ");
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
            else if (GetKeyInput(keyInput, "4"))
            {
                Save();
            }
            else
            {
                Console.WriteLine("\n\nPlease select a valid option...\n");
            }
        }

        void SetName()
        {
            Console.Write("Character Name : ");
            string name = Console.ReadLine();
            GameManager.Instance.PlayerCharacter.Name = name;
            DisplayUI();
            Console.WriteLine("Character name has been set to : " + GameManager.Instance.PlayerCharacter.Name);
        }

        void SetClass()
        {
            Console.Clear();
            Console.WriteLine("Select Class : ");

            Console.WriteLine("1 : Warrior\n" +
                              "2 : Mage\n" +
                              "3 : Ranger\n" +
                              "4 : Thief");

            CharacterClass characterClass = new Warrior();

            string classInput = Console.ReadLine();

            if (GetKeyInput(classInput, "1", "warrior"))
            {
                characterClass = new Warrior();
            }
            else if (GetKeyInput(classInput, "2", "mage"))
            {
                characterClass = new Mage();
            }
            else if (GetKeyInput(classInput, "3", "ranger"))
            {
                characterClass = new Ranger(); ;
            }
            else if (GetKeyInput(classInput, "4", "thief"))
            {
                characterClass = new Thief();
            }

            GameManager.Instance.PlayerCharacter.CharacterClass = characterClass;

            DisplayUI();
            Console.WriteLine("You have selected : " + characterClass.GetType().Name);
        }

        void SelectStats()
        {
            
        }

        void Save()
        {
           //TODO override system or create new save
        }

    }
}
