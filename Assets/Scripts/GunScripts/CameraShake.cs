using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Camera cam;
    private float shakeAmount;

    void Awake()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }
    public void Shake(float amt, float length)
    {
        shakeAmount = amt;
        InvokeRepeating("DoShake", 0, 0.1f);
        Invoke("StopShake", length);
    }
    void DoShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 campPos = cam.transform.position;
            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            campPos.x += offsetX;
            campPos.y += offsetY;

            cam.transform.position = campPos;
        }
    }
    
    void StopShake()
    {
        CancelInvoke("DoShake");
        cam.transform.localPosition = new Vector3(-29,9.25f,-10);
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.T))
        {
            Shake(0.1f, 0.2f);
        }
	}
}
