using System;
using System.Collections.Generic;
using System.Threading;

namespace TextBaseGame.States
{
    abstract class State
    {

        public string Name => GetType().Name;

        public Stack<State> States { get; private set; }

        public State(Stack<State> states) 
        {
            States = states;
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

        public virtual void PushState(State target)
        {
            States.Push(target);
        }

        public virtual void PopState()
        {
            if (States.Count < 1) return;

            States.Pop();

            if(States.Count > 0)
            States.Peek().DisplayUI();
        }

        public virtual void Loading(int loadCount = 3, int delay = 150)
        {
            int i = 0;
            Console.WriteLine();
            while(i < loadCount)
            {
                Thread.Sleep(delay);
                Console.Write('.');
                i++;
            }
            Console.WriteLine();
        }
    }
}
