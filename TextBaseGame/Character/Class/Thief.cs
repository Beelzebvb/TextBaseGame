using System;
using System.Collections.Generic;
using System.Text;

namespace TextBaseGame.Entity.Class
{
    class Thief : CharacterClass
    {
        public Thief()
        {

        }
        public Thief(float health, float defense, float damage, float mana) : base(health, defense, damage, mana)
        {

        }

        public override void Attack(int index)
        {
            throw new NotImplementedException();
        }
    }
}
