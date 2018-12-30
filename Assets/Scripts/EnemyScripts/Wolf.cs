using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    private float debuffcount = 2f;
    


    public override void SetVelocity()
    {
        aivelocity = 3f;
    }
    public override void PushHit()
    {
        base.PushHit();
        SlowDeBuff();
    }

    private void SlowDeBuff()
    {
        if (health >= 0)
        {
            StartCoroutine(WaitToNormalVelocity());
        }
        else
        {
            aivelocity = 0f;
        }
    }

    IEnumerator WaitToNormalVelocity()
    {

        aivelocity = 0.3f;
        yield return new WaitForSeconds(debuffcount);

        if (health <= 0)
        {

            yield break;
        }
        else
        {
            this.aivelocity = this.constVelocity;
        }

    }

}
