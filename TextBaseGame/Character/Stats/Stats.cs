using System;

namespace TextBaseGame.Entity
{
    class Stats
    {
        public float Level;
        public float Health;
        public float Defense;
        public float Damage;
        public float Mana;
        public float MaxHealth;
        public float MaxDefense;
        public float MaxDamage;
        public float MaxMana;

        public void TakeDamage(float amount) => Health = MathF.Max(0f, Health - amount);
        public void GainHealth(float amount) => Health = MathF.Min(MaxHealth, Health + amount);
        public void LoseMana(float amount) => Mana = MathF.Max(0f, Mana - amount);
        public void GainMana(float amount) => Mana = MathF.Min(MaxMana, Mana + amount);

        public void GainLevel()
        {
            Level++;
        }

        public void IncreaseMaxHealth(float amount)
        {
            Health += amount;
            MaxHealth += amount;
        }
        public void IncreaseMaxDefense(float amount)
        {
            Defense += amount;
            MaxDefense += amount;
        }
        public void IncreaseMaxDamage(float amount)
        {
            Damage += amount;
            MaxDamage += amount;
        }
        public void IncreaseMaxMana(float amount)
        {
            Mana += amount;
            MaxMana += amount;
        }

        public void InitStats(float health, float defense, float damage, float mana)
        {
            Health = MaxHealth = health;
            Defense = MaxDefense = defense;
            Damage = MaxDamage = damage;
            Mana = MaxMana = mana;

        }

        public string DisplayStats()
        {

            string statsInfo = $"Health : {Health} / {MaxHealth}\n" +
                               $"Defense : {Defense} / {MaxDefense}\n" +
                               $"Damage : {Damage} / {MaxDamage}\n" +
                               $"Mana : {Mana}" + ((MaxMana == 0) ? "\n" : $" / {MaxMana}\n");

            return statsInfo;

        }

    }
}
