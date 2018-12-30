using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakTorch : Torch
{

    public Transform FlameScale;
    private float burnOut = 3f;
    private float lightTime = 3f;

    void Start()
    {
        torch = GetComponent<Light>();
        FlameScale = GetComponent<Transform>();
    }


    new void Update()
    {
        base.Update();
        //Faltering the light like real life
        constLight = Random.Range(minInsRange, maxInsRange);

        burnOut -= Time.smoothDeltaTime;
        lightTime -= Time.smoothDeltaTime;
        falter -= Time.smoothDeltaTime;

        //Particle Scale less than zero
        if (burnOut <= 0.0f)
        {
            Ash();
        }
        //Light Range
        if (torch.range > 5.0f)
        {
            torch.range = 5.0f;
        }
        else if (torch.range < 2)
        {
            torch.range = 2f;
        }
        //Every 3 sec(depends on light time) light will be decrease 0.10 range
        if (lightTime <= 0.0f)
        {
            LightDecrease();
        }
        //Light falter timer 
        if (falter <= 0.0f)
        {
            FlameFalter();
        }

        //These scales are the fit for this particle effect, max scale and min scale
        if (FlameScale.transform.localScale.x >= 0.76809f || FlameScale.transform.localScale.z >= 0.4460005f)
        {
            FlameScale.transform.localScale = new Vector3(0.76809f, 2.275812f, 0.4460005f);
        }

        else if (FlameScale.transform.localScale.x <= 0.4920895f || FlameScale.transform.localScale.z <= 0.2160006)
        {
            FlameScale.transform.localScale = new Vector3(0.4920895f, 1.015812f, 0.2160006f);
        }
    }


    private void Ash()
    {
        FlameScale.transform.localScale -= new Vector3(0.06f, 0.11f, 0.01f);
        burnOut = 3f;


    }
    //Button Action
    public void FlameUp()
    {
        FlameScale.transform.localScale += new Vector3(0.06f, 0.11f, 0.01f);
    }


    private void LightDecrease()
    {
        torch.range = torch.range - 0.10f;
        lightTime = 3f;
    }
    //Button Action
    public void TorchLightUp()
    {
        if (torch.range < 5)
        {
            torch.range += 0.10f;
        }
    }








}
