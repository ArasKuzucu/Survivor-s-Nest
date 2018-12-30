using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{

    public int PlayerExperience;
    private WaveSpawner ws;
    private Shooting st;
    public Text ExpTxt;


    void Start()
    {
        //These values for contructer PlayerData script
        ws = FindObjectOfType<WaveSpawner>();
        st = FindObjectOfType<Shooting>();
        PlayerData data = SaveSystem.LoadPlayerLevel();
        if (data == null)
        {
            ExpTxt.text = "Exp: " + 0;
        }
        else
        {
            PlayerExperience = data.experience;
            ExpTxt.text = "Exp: " + PlayerExperience;
        }

    }
   
    public void GainExp(int Exp)
    {
        //Save
        PlayerExperience += Exp;
        SaveSystem.SavePlayerProgress(this,ws, st);
        
    }
    private void Update()
    {
        

        ExpTxt.text = "Exp: " + PlayerExperience;
    }
}
