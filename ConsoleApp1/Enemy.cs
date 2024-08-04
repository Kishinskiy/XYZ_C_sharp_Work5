namespace ConsoleApp1;

public class Enemy
{
    public Unit PlayerUnit;
    public Unit EnemyUnit;
    public Input Input;
    public int AbilitiesCount;
    public int SecondAbilitiesModifier;
    public int ThirdAbilitiesValue;

    public Enemy(Unit playerUnit, Unit enemyUnit, Input input)
    {
        PlayerUnit = playerUnit;
        EnemyUnit = enemyUnit;
        Input = input;
        AbilitiesCount = 3;
        SecondAbilitiesModifier = 3;
        ThirdAbilitiesValue = 100;
    }

    public void Turn()
    {
        switch (Input.GetAICommandNumber(AbilitiesCount))
        {
            case 1:
                PlayerUnit.TakeDamage(EnemyUnit.Damage, EnemyUnit, true);
                break;
            case 2:
                int modifiedDamage = EnemyUnit.Damage * SecondAbilitiesModifier;
                EnemyUnit.TakeDamage(EnemyUnit.Damage, EnemyUnit);
                PlayerUnit.TakeDamage(modifiedDamage, EnemyUnit);
                break;
            case 3:
                if (EnemyUnit.LastDamageFromWeapon)
                {
                    EnemyUnit.TakeDamage(ThirdAbilitiesValue, EnemyUnit);
                }
                else
                {
                    EnemyUnit.TakeDamage(-ThirdAbilitiesValue, EnemyUnit);
                }
                break;
        }
    }
}