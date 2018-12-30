using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeteranZombie : Zombie
{

    [SerializeField]
    private float healthRegen = 2f;
    

    new void Update()
    {
        base.Update();
        HealhtRegen();
    }


    public override void SetVelocity()
    {
        aivelocity = 0.3f;
    }
    public override void PushHit()
    {
        return;
    }
    public override void Dead()
    {
        base.Dead();
    }
    public void HealhtRegen()
    {
        healthBar.fillAmount = health / startHealth;
        if (health < startHealth)
        {
            health += healthRegen * Time.deltaTime*7;
        }
        else if (health >= startHealth)
        {
            health = startHealth;
        }
    }
}
