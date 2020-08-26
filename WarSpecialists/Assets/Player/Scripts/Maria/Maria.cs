using UnityEngine;

public class Maria : PlayerBase
{
    protected override void Start()
    {
        Attack = 5;
        Damage = 3;
        Deffense = 4;
        AttackSpeed = 2f;
        SpecialtyPoints = 0;
        Gold = 0;
    }
    protected override void Update()
    {
        if (!IsAlive)
        {
            Died();
        }
    }
}