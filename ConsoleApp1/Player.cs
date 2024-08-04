namespace ConsoleApp1;

public class Player
{
    public string Name;
    public Unit PlayerUnit;
    public Unit EnemyUnit;
    public Input Input;
    public ConsoleWriter Writer;
    public int FireballDamage = 200;

    public Player(string name, Unit playerUnit, Unit enemyUnit, Input input, ConsoleWriter writer)
    {
        Name = name;
        PlayerUnit = playerUnit;
        EnemyUnit = enemyUnit;
        Input = input;
        Writer = writer;
        
        PlayerUnit.AbilityDescriprion.Add($"Ударить оружием (урон {playerUnit.Damage})");
        PlayerUnit.AbilityDescriprion.Add($"Щит:  следующая атака противника не наносит урона");
        PlayerUnit.AbilityDescriprion.Add($"Огеный шар: наносит урон в размере{FireballDamage}");
    }

    public void Turn()
    {
        
        Writer.WriteHealthStatus("Ваше здоролье: ", PlayerUnit);
        Writer.WriteHealthStatus("Противник здоровье: ", EnemyUnit);
        
        Writer.WriteAllAbilities($"{Name}! Выьерете действие: ", PlayerUnit);
        
        switch (Input.GetPlayerCommandNumber())
        {
            case 1:
                EnemyUnit.TakeDamage(PlayerUnit.Damage, PlayerUnit, true);
                break;
            case 2:
                PlayerUnit.ShieldCount = 1;
                break;
            case 3:
                EnemyUnit.TakeDamage(FireballDamage, PlayerUnit, true);
                break;
        }
    }


}