

public class Strike : Skill, IUsable
{

    int totalDamage;

    private void Awake()
    {
    }

    private void Start()
    {
        Use();
    }

    public void Use()
    {
        totalDamage = damage + level * increasedDamagePerLevel;
    }


}