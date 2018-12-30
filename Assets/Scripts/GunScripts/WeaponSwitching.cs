using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{



    public GameObject Pistol;
    public GameObject Rifle;

    public Animator PlayerAnim;
    //public GameObject PistolCs;
    //public GameObject RifleCs;
    void Start()
    {
        PlayerAnim = GetComponent<Animator>();


        //PistolCs.SetActive(true);

    }


    public void Silah1()
    {
        //RifleCs.SetActive(false);
        //PistolCs.SetActive(true);

        PlayerAnim.SetBool("IsPistol", true);
        PlayerAnim.SetBool("IRifle", false);
        Pistol.SetActive(true);
        Rifle.SetActive(false);


    }
    public void Silah2()
    {
        //RifleCs.SetActive(true);
        //PistolCs.SetActive(false);

        PlayerAnim.SetBool("IsPistol", false);
        PlayerAnim.SetBool("IRifle", true);
        Pistol.SetActive(false);
        Rifle.SetActive(true);


    }


    void Update()
    {



    }
}
