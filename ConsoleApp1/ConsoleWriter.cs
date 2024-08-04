namespace ConsoleApp1;

enum TextColor
{
    Danger = ConsoleColor.Red,
    Warning = ConsoleColor.Yellow,
    OK = ConsoleColor.Green
}

public class ConsoleWriter
{
    void WriteLine(string message, TextColor color)
    {
        Console.ForegroundColor = (ConsoleColor)color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    void DamageMessage(string damager, string defender, float damage)
    {
        WriteLine($"Игрок {damager} наносит угроку {defender} урон в размере {damage} едениц.", TextColor.Danger);
    }

    void HealMessage(string healer, float healValue)
    {
        WriteLine($"Игрок {healer} востановил себе здоровье в размере {healValue} едениц.", TextColor.OK);
    }
    
    void WriteDamageType(AttackType attackType, string damagerName, string defenderName,
        List<float> damages)
    {
        foreach (var damage in damages)
        {
            switch (attackType)
            {
                case AttackType.Damage:
                    DamageMessage(damagerName, defenderName, damage);
                    break;
                case AttackType.Self:
                    DamageMessage(defenderName, defenderName, damage);
                    break;
                case AttackType.Heal:
                    HealMessage(defenderName, damage);
                    break;
            }
        }
                    
        damages.Clear();
    }

    public void WriteDamageFromTo(Dictionary<AttackType, List<float>> damages, string damagerName, string defenderName)
    {
        foreach (var damage in damages)
        {
            WriteDamageType(damage.Key, damagerName, defenderName, damage.Value);
        }
    }
    
    public void WriteHealthStatus(string owner, Unit unit)
    {
        if (unit.currentHealth <= (0.3f * unit.maxHealth))
        {
            Console.ForegroundColor = (ConsoleColor)TextColor.Danger;
        }
        else if (unit.currentHealth <= (0.5f * unit.maxHealth))
        { 
            Console.ForegroundColor = (ConsoleColor)TextColor.Warning;        }
        else
        {
            Console.ForegroundColor = (ConsoleColor)TextColor.OK; 
        }
        
        Console.WriteLine($"{owner}: {unit.currentHealth}");
        Console.ResetColor();
    }
    
    public void WriteAllAbilities(string message, Unit unit)
    {
        Console.WriteLine($"{message}");
        for (int i = 0; i < unit.GetAbilityCount(); i++)
        {
            Console.WriteLine($"{i + 1}. {unit.GetAbilityDescription(i)}");
        }
        Console.WriteLine();
    }
}