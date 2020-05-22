using System;
using System.Collections.Generic;
using System.Text;

namespace TextBaseGame.States
{
    class SelectCharacterState : State
    {

        public SelectCharacterState(Stack<State> states) : base(states)
        {
            DisplayUI();
        }

        public override void Update()
        {
            //MakeChoice:
            Console.Write("Choice : ");
            string keyInput = Console.ReadLine();

            if (GetKeyInput(keyInput, "1"))
            {
                Console.WriteLine("Character 1 Selected");
                Console.ReadLine();
            }
            else if (GetKeyInput(keyInput, "2"))
            {
                Console.WriteLine("Character 2 Selected");
                Console.ReadLine();
            }
            else if (GetKeyInput(keyInput, "3"))
            {
                Console.WriteLine("Character 3 Selected");
                Console.ReadLine();
            }
            else if (GetKeyInput(keyInput, "4"))
            {
                Console.WriteLine("Character 4 Selected");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\n\nPlease select a valid option...\n");
                //goto MakeChoice;
            }

            PopState();

        }
        public override void DisplayUI()
        {
            Console.Clear();

            string UI = "==============================\n" +
                        "======SELECT A CHARACTER======\n" +
                        "==============================\n\n" +
                        "1 : Character 1\n" +
                        "2 : Character 2\n" +
                        "3 : Character 3\n" +
                        "4 : Character 4\n\n";

            Console.Write(UI);
        }
    }
}
