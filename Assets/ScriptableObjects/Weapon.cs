using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Weapon : ScriptableObject {

    public new string name;
    public string description;
    public int CostOfExp;
    public Sprite artwork;

    public float effectSpawnRate;
    public float fireRate;
    public float Damage;
    public LayerMask whatToHit;
    public int BulletCapacity;
    public float ReloadingTime;

    public AudioClip gunShotSfx;
    public AudioClip reloadingSfx;

}
