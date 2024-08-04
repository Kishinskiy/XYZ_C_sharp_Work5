namespace ConsoleApp1;

public enum AttackType
{
    Damage,
    Self,
    Heal
}

public class Unit
{
    public float maxHealth;
    public float currentHealth;
    public int Damage;
    public bool isAlive = true;
    public int ShieldCount = 0;
    public bool LastDamageFromWeapon = false;
    public List<string> AbilityDescriprion = new();
    public Dictionary<AttackType, List<float>> DamageHistory = new();

    public Unit(int damage, float maxHealth)
    {
        Damage = damage;
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        DamageHistory[AttackType.Damage] = new List<float>();
        DamageHistory[AttackType.Self] = new List<float>();
        DamageHistory[AttackType.Heal] = new List<float>();
    }

    public void TakeDamage(int damage, Unit origin, bool isWeaponDamage = false)
    {
        LastDamageFromWeapon = isWeaponDamage;
        bool isHealth = damage < 0;
        AttackType attackType = AttackType.Damage;
        if (isHealth)
        {
            attackType = AttackType.Heal;
        }
        else if (origin == this)
        {
            attackType = AttackType.Self;
        }

        if (inShield() && !isHealth)
        {
            ShieldCount--;
            DamageHistory[attackType].Add(0);
            return;
        }

        currentHealth -= damage;
        DamageHistory[attackType].Add(damage);
        
        if (currentHealth <= 0)
        {
            isAlive = false;
            return;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public string GetAbilityDescription(int abilityNumber)
    {
        return AbilityDescriprion[abilityNumber];
    }

    public int GetAbilityCount()
    {
        return AbilityDescriprion.Count;
    }

    public bool inShield()
    {
        return ShieldCount > 0;
    }
}