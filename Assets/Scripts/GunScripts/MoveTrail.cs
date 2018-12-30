using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

    public int moveSpeed = 230;
    private Shooting shooTing;
    // Update is called once per frame
    private void Start()
    {
        shooTing = FindObjectOfType<Shooting>();
    }
    void Update () {
        transform.Translate(Vector3.right*Time.deltaTime*moveSpeed);
        
        if (shooTing.hit.collider.tag =="Zemin" || shooTing.hit.collider.tag == "Enemy")
        {
            Destroy(this.gameObject,0.04f);
        }
        else
        {
            Destroy(this.gameObject,1f);
        }
           
        
	}
   
    
}
