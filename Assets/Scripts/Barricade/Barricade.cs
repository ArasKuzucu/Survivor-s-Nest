using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Barricade : MonoBehaviour
{

    public float barrackHealth;
    private float startHealth;
    public Image brckHealhtBar;
    public GameObject gameOverContainer;
   

    void Start()
    {
        startHealth = barrackHealth;
       

        gameOverContainer.SetActive(false);

    }
   
    public void TakenDamage(float dmg)
    {
        barrackHealth -= dmg;
        brckHealhtBar.fillAmount = barrackHealth / startHealth;
        if (barrackHealth <= 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        gameOverContainer.SetActive(true);
        
    }
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

}
