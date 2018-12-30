using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField]
    protected Light torch;
    [SerializeField]
    protected float minInsRange, maxInsRange;

    protected float constLight;
    protected float falter = 2f;

    public virtual void Update()
    {
        FlameFalter();
    }

    protected void FlameFalter()
    {
        constLight = Random.Range(minInsRange, maxInsRange);
        torch.intensity = constLight;
        falter = 0.1f;
    }
}



