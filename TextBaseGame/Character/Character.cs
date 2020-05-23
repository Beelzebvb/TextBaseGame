using System;
using TextBaseGame.Entity.Class;

namespace TextBaseGame.Entity
{
    class Character
    {

        public string Name = "Default Character";
        public float Index = 0;

        public CharacterClass CharacterClass { get; set; }

        //Attack
        public Character()
        {
            CharacterClass = new Warrior();
        }

        public void DisplayInfo()
        {
            string info = 
                          $"\ncharacter : {Index}" + "\n"
                        + $"     name  : {Name}\n"
                        + $"     class : {CharacterClass.Name}\n"
                        + $"     level : {CharacterClass.ClassStats.Level}\n"
                        +  "     stats :\n"
                        + $"             health  : {CharacterClass.ClassStats.Health}\n"
                        + $"             defense : {CharacterClass.ClassStats.Defense}\n"
                        + $"             damage  : {CharacterClass.ClassStats.Damage}\n"
                        + $"             mana    : {CharacterClass.ClassStats.Mana}\n"
                        +  "\n\n";

            Console.WriteLine(info);
        }


        public void Attack()
        {
            CharacterClass.Attack(0);
        }

        //Use Item
        //Escape

    }
}
