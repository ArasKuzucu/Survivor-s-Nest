using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy {

    private void OnTriggerStay2D(Collider2D col)
    {
        //Zombiler ışıkta daha çok hasar yer
        if (col.gameObject.tag == "Torch")
        {
            armor = Random.Range(0, armor) - 10;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        armor = startArmor;
    }
    
}
