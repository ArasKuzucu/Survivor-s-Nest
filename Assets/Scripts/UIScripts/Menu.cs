using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public Sound[] sounds;

    public AudioSource audioSource;
    public AudioSource audioSourceSfx;

    public Slider volMusicSlider;
    public Slider volSfxSlider;

    public Animator animator;
 
    public void Awake()
    {
        audioSource.volume = sounds[0].volume;
        audioSourceSfx.volume = sounds[1].volume;

        volMusicSlider.value = audioSource.volume;
        volSfxSlider.value = audioSourceSfx.volume;
    }
    public void StartGame()
    {
      animator.SetBool("IsFade", true);

    }
    public void Quit()
    {
          
        Application.Quit();
    }


    public void VolumeMusicSettings()
    {
        audioSource.volume = volMusicSlider.value;
        sounds[0].volume = audioSource.volume;
    }
    public void VolumeSfxSetttings()
    {
        audioSourceSfx.volume = volSfxSlider.value;
        sounds[1].volume = audioSource.volume;
    }

    public void OnFadeComplete()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
