using System;
using System.Collections.Generic;
using System.Threading;
using TextBaseGame.Utilities;

namespace TextBaseGame.States
{
    abstract class State
    {

        public string Name => GetType().Name;

        public Action OnUpdateUI = delegate { };

        public Stack<State> States { get; private set; }

        public State(Stack<State> states)
        {
            States = states;
            OnUpdateUI += DisplayUI;
        }

        ~State()
        {
            OnUpdateUI -= DisplayUI;
        }

        public abstract void Update();

        public abstract void DisplayUI();

        public virtual bool GetKeyInput(string keyInput, string letter, string word = "")
        {
            if (word != string.Empty)
            {
                if (keyInput.Equals(letter, StringComparison.OrdinalIgnoreCase) || keyInput.Equals(word, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            else
            {
                if (keyInput.Equals(letter, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public virtual int InputNumber()
        {
        InputNumber:
            Console.Write("Select a number : ");
            string indexInput = Console.ReadLine();

            if (Int32.TryParse(indexInput, out int saveIndex))
            {
                return Math.Abs(saveIndex);
            }
            else
            {
                ConsoleUI.Error("Please select a valid option...");
                goto InputNumber;
            }
        }


        public virtual void PushState(State target)
        {
            States.Push(target);
        }

        public virtual void PopState()
        {
            if (States.Count < 1) return;

            States.Pop();

            if (States.Count > 0)
                States.Peek().OnUpdateUI?.Invoke();
        }

        public virtual void Loading(int loadCount = 3, int delay = 150)
        {
            int i = 0;
            Console.WriteLine();
            while (i < loadCount)
            {
                Thread.Sleep(delay);
                Console.Write('.');
                i++;
            }
            Console.WriteLine();
        }
    }
}
