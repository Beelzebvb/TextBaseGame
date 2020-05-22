using System;

namespace TextBaseGame.Entity.Class
{
    class Ranger : CharacterClass
    {

        public Ranger() { }

        public Ranger(float health, float defense, float damage, float mana) : base(health, defense, damage, mana)
        {

        }

        public override void Attack(int index)
        {
            throw new NotImplementedException();
        }
    }
}
