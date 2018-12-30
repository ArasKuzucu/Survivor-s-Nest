using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    [HideInInspector]
    public Shooting shooting;
    public int weaponNumber;
    

    public Text weaponnName;
    public Text cost;
    public Text description;
    public Image weaponArt;
    private Experience xp;

    public Text buyBtnText;
    
    private void Start()
    {

        //These values for contructer PlayerData script
        xp = GameObject.FindObjectOfType<Experience>();
        shooting = GameObject.FindObjectOfType<Shooting>();

        SetButton();
    }


    public void Resume()
    {
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    //Set the weapon values on the ui 
    void SetButton()
    {

        string costString = shooting.silah[weaponNumber].CostOfExp.ToString();
        cost.text = "Exp:  " + costString.ToString();

        weaponArt.sprite = shooting.silah[weaponNumber].artwork;
        this.weaponnName.text = shooting.silah[weaponNumber].name.ToString();
        description.text = shooting.silah[weaponNumber].description.ToString();


    }

    public void OnClick()
    {

        if (xp.PlayerExperience >= shooting.silah[weaponNumber].CostOfExp)
        {
            xp.PlayerExperience -= shooting.silah[weaponNumber].CostOfExp;
            shooting.CurrentWeapon = weaponNumber;
            shooting.silah[weaponNumber].CostOfExp = 0;
            cost.text = "Exp:  " + shooting.silah[weaponNumber].CostOfExp;
           
            //Set the value for the player on the shooting script
            shooting.ChangeWeapon(weaponNumber);

            buyBtnText.text = "Equip";

        }
        else
        {
            Debug.Log("Not enough money to buy.");
        }
    }
}


