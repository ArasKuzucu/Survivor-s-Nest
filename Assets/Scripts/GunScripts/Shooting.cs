using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using TMPro;
public class Shooting : MonoBehaviour
{
    //Scriptable Object

    public Weapon[] silah;
    public int CurrentWeapon = 0;
    public GameObject bloodFx;
    public TextMeshProUGUI bulletTxt;
    [HideInInspector]
    public List<string> purchasedWeapons = new List<string>();
    public Transform BulletTrailPrefab;
    public Transform MuzzleFlashPrefab;
    public float camShakeAmt = 0.1f;
    CameraShake camShake;

    private float effectSpawnRate;
    private float fireRate;
    private float Damage;
    private LayerMask whatToHit;
    private int BulletCapacity;
    private float ReloadingTime;
    private bool IsReloading = false;
    private float nextFire = 0;
    private float effectRate = 0;
    private GameObject hasdirection;
    private GameObject hasfirepoint;
    private int RestoreMag;
    private WaveSpawner ws;
    private Experience xp;

    private AudioSource audioSource;
    private AudioClip gunShotSfx;
    private AudioClip reloadSfx;

    private void Start()
    {
        PlayerData data = SaveSystem.LoadPlayerLevel();
        //These values for contructer PlayerData script
        ws = FindObjectOfType<WaveSpawner>();
        xp = GameObject.FindObjectOfType<Experience>();
        if (data != null && data.weaponName.Length > 0)
        {

            for (int i = 0; i < data.weaponName.Length; i++)
            {
                purchasedWeapons.Add(data.weaponName[i].ToString());
                Debug.Log(purchasedWeapons[i]);
            }
        }

        //deserialization the string array and set the values which were purchased by the player
        if (purchasedWeapons.Count > 0)
        {
            for (int i = 0; i < purchasedWeapons.Count; i++)
            {
                for (int j = 0; j < silah.Length; j++)
                {
                    if (purchasedWeapons[i] == silah[j].name)
                    {
                        silah[j].CostOfExp = 0;
                    }
                }
            }
        }

        audioSource = GetComponent<AudioSource>();
        this.GetComponent<SpriteRenderer>().sprite = silah[CurrentWeapon].artwork;
        effectSpawnRate = silah[CurrentWeapon].effectSpawnRate;
        this.fireRate = silah[CurrentWeapon].fireRate;
        this.Damage = silah[CurrentWeapon].Damage;
        this.whatToHit = silah[CurrentWeapon].whatToHit;
        this.BulletCapacity = silah[CurrentWeapon].BulletCapacity;
        this.ReloadingTime = silah[CurrentWeapon].ReloadingTime;
        RestoreMag = BulletCapacity;
        this.gunShotSfx = silah[CurrentWeapon].gunShotSfx;
        this.reloadSfx = silah[CurrentWeapon].reloadingSfx;
        //firePoint = transform.Find("FirePoint");
        hasdirection = GameObject.FindGameObjectWithTag("Direction");
        hasfirepoint = GameObject.FindGameObjectWithTag("FirePoint");

        camShake = GameObject.FindObjectOfType<CameraShake>();

        StartReloadingTime = ReloadingTime;
    }
    //Change weapon when player bought on runtime
    public void ChangeWeapon(int NewIndex)
    {
        //if player already buy it there is no more data add on list
        if (!purchasedWeapons.Contains(silah[NewIndex].name))
        {
            purchasedWeapons.Add(silah[NewIndex].name);
        }

        SaveSystem.SavePlayerProgress(xp, ws, this);
        this.GetComponent<SpriteRenderer>().sprite = silah[NewIndex].artwork;
        effectSpawnRate = silah[NewIndex].effectSpawnRate;
        this.fireRate = silah[NewIndex].fireRate;
        this.Damage = silah[NewIndex].Damage;
        this.whatToHit = silah[NewIndex].whatToHit;
        this.BulletCapacity = silah[NewIndex].BulletCapacity;
        this.ReloadingTime = silah[NewIndex].ReloadingTime;
        this.gunShotSfx = silah[NewIndex].gunShotSfx;
        this.reloadSfx = silah[NewIndex].reloadingSfx;
        RestoreMag = BulletCapacity;
        //Store main reload time for showing the player the reload time
        StartReloadingTime = ReloadingTime;

    }


    public Image reloadBtnImg;
    private float StartReloadingTime;
    //Reload time method and reload showing method are different indeed another datatype to store reload time and showing the reload time with image.This value needed.
    private float CycleReloadImg;
    public void ReloadWeapon()
    {
        CycleReloadImg = StartReloadingTime;
        StartCoroutine(Reloading());
    }

    void Update()
    {

        bulletTxt.text = BulletCapacity + "/" + "";
        if (IsReloading)
        {
            CycleReloadImg -= Time.smoothDeltaTime;
            reloadBtnImg.fillAmount = CycleReloadImg / StartReloadingTime;

            return;
        }
        if (BulletCapacity <= 0)
        {
            ReloadingTime = StartReloadingTime;
            StartCoroutine(Reloading());
            return;
        }
        if (Time.time > nextFire && CrossPlatformInputManager.GetButton("Hold") && BulletCapacity > 0)
        {
            BulletCapacity--;
            bulletTxt.text = BulletCapacity + "/" + "";
            nextFire = Time.time + 1 / fireRate;
            Shoot();
        }


    }
    public RaycastHit2D hit;
    void Shoot()
    {
        Vector2 FirePoint = new Vector2(hasfirepoint.transform.position.x, hasfirepoint.transform.position.y);
        Vector2 DirectionPoint = new Vector2(hasdirection.transform.position.x, hasdirection.transform.position.y);


        hit = Physics2D.Raycast(FirePoint, DirectionPoint - FirePoint, 100, whatToHit);

        Enemy health = hit.transform.GetComponent<Enemy>();

        if (health != null && hit.collider.tag == "Target")
        {
            health.ReduceHealth(Damage);
            GameObject bloodEffect = Instantiate(bloodFx, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(bloodEffect, 0.5f);
        }

        if (Time.time > effectRate && CrossPlatformInputManager.GetButton("Hold"))
        {
            effectRate = Time.time + 1 / effectSpawnRate;
            Effect();
        }

    }



    private Transform clone;

    void Effect()
    {
        // Bullet trail and ray have got different angles, this code block provide us to align each other(difference is 69 degree so ı put the negative value)
        Instantiate(BulletTrailPrefab, hasfirepoint.transform.position, hasfirepoint.transform.rotation * Quaternion.Euler(0f, 0f, -69f));
        clone = Instantiate(MuzzleFlashPrefab, hasfirepoint.transform.position, hasfirepoint.transform.rotation * Quaternion.Euler(0f, 0f, -69f)) as Transform;
        Destroy(clone.gameObject, 0.02f);

        audioSource.PlayOneShot(gunShotSfx);
        camShake.Shake(camShakeAmt, 0.2f);

    }

    IEnumerator Reloading()
    {
        IsReloading = true;
        bulletTxt.text = BulletCapacity + "Reloading...";
        audioSource.PlayOneShot(reloadSfx);
        yield return new WaitForSeconds(ReloadingTime);
        //Restore reload time for image
        CycleReloadImg = ReloadingTime;
        //After reload, image will be restored
        reloadBtnImg.fillAmount = 1;

        BulletCapacity = RestoreMag;
        IsReloading = false;
        bulletTxt.text = BulletCapacity + "/" + "";

    }



}

