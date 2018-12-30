using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ArmRotation : MonoBehaviour
{

    public float lookAngle;
    public float armRotation;
 
    private void Start()
    {
        armRotation = transform.rotation.z;
    }
  
    void Update()
    {

        armRotation = CrossPlatformInputManager.GetAxis("Vertical");
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle*armRotation);

    }
   


}
