using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUp : MonoBehaviour {

   
    public float DestroyTime = 0.8f;
    public Vector2 offSet = new Vector2(0, 10);
    public Vector2 RandomizeIntensity = new Vector2(0, 8);
	void Start () {

        Destroy(gameObject, DestroyTime);
        transform.localPosition = offSet;

        transform.localPosition = new Vector2(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x), Random.Range(3, RandomizeIntensity.y));
	}
	
}
