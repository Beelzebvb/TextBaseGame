using System.Collections.Generic;
using TextBaseGame.Entity;
using TextBaseGame.States;

namespace TextBaseGame.Manager
{
    class GameManager : Singleton<GameManager>
    {

        readonly Stack<State> states;
        bool _isRunning;
        public Character PlayerCharacter;

        public GameManager()
        {
            PlayerCharacter = new Character();

            states = new Stack<State>();

            states.Push(new MainMenuState(states));

            _isRunning = true;
        }

        public void Run()
        {
            while (_isRunning)
            {
                if (states.Count > 0)
                {
                    Update();

                }
                else
                    _isRunning = false;
            }
        }

        void Update()
        {
            states.Peek().Update();
        }
        //Interactions -----
        // Explore
        // Enter Fight -> Fight State
        // Quit -> Any State ?
        // Select Character
        // New Character
        // Save
        // Load
    }
}
