using UnityEngine;
using UnityEngine.UI;

public class Maria : PlayerBase
{
    protected override void Start()
    {
        Damage = 3;
        Deffense = 4;
        AttackSpeed = 2f;
        SpecialtyPoints = 0;
        Gold = 0;
        Health = 100;
    }
    public Image healthBar;

    //public void TakeDamage(float amount)
    //{
    //    Health -= amount;
    //    healthBar.fillAmount = Health / 100f;
    //    if(Health <= 0)
    //    {
    //        IsAlive = false;
    //    }
    //}
    protected override void Update()
    {
        if (!IsAlive)
        {
            Died();
        }
    }
}