using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public int level;
    public int experience;   
    public string[] weaponName;

    public PlayerData(Experience exp , WaveSpawner waveSpawner, Shooting ws)
    {
        experience = exp.PlayerExperience;
        level = waveSpawner.nextWave;
        weaponName = ws.purchasedWeapons.ToArray(); 
    }
   

}
