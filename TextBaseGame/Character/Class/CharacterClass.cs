namespace TextBaseGame.Entity.Class
{
    abstract class CharacterClass
    {
        public string Name;

        public abstract void Attack(int index);

        public Stats ClassStats { get; private set; }


        public CharacterClass()
        {
            Name = GetType().Name;
            ClassStats = new Stats();
            ClassStats.InitStats(250f, 100f, 125f, 0f);

        }
        public CharacterClass(float health, float defense, float damage, float mana)
        {
            ClassStats.InitStats(health, defense, damage, mana);
        }
    }
}
