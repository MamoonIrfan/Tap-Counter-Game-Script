using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //When and where we use gameobject class basically we this when we don't know on which
    //component we want to work.
    public GameObject Mainmenu;
    public GameObject Settings;
    public GameObject Credit; 
    public GameObject Howtoplay;
    public GameObject SettingsBtn;

    public GameObject GameTitle;
    public Animator SettingSlider;
    public Animator HowtoplaySlider;
    public Animator CreditSlider;

    public Animator FadePanel;

    //Game Background Sound: 
    public AudioSource gameAudio;
    //Game Button Click Sound: 
    public AudioClip ButtonSoundClip;

    public GameObject MusicOn;

    public GameObject MusicOff;

    public static int GameplayMusicControl;
    void Start()
    {
        FadeIn();
        Mainmenu.SetActive(true);
        Settings.SetActive(false);  
        Credit.SetActive(false);
        Howtoplay.SetActive(false);
        SettingsBtn.SetActive(true);
        GameTitle.SetActive(true);
        int musicstate = PlayerPrefs.GetInt("Music",1);
        if (musicstate == 1)
        {
        BackGroundMusicPlay();
        }
        else
        {
            BackGroundMusicStop();
        }
    }

    //Below all the function or methods are perform meaningful information

    //OnSettingbtnClicked() method will control Settings button in game
    public void OnSettingbtnClicked()
    {
        MusicOn.SetActive(true);
        MusicOff.SetActive(true);
        ButtonClickSound();
        Mainmenu.SetActive(false);
        GameTitle.SetActive(false);
        Settings.SetActive(true);
        SettingsBtn.SetActive(false);
        SettingSlider.SetTrigger("Slide-In");

    }


    //OnHowtoPlaybtnClicked() method will control How to play button in game
    public void OnHowtoPlaybtnClicked()
    {
        ButtonClickSound();
        CommonOffthings();
        GameTitle.SetActive(false);
        Howtoplay.SetActive(true);
        HowtoplaySlider.SetTrigger("Slide-In");
    }

    //OnCreditsbtnClicked() method will control credits button in game
    public void OnCreditsbtnClicked()
    {
        ButtonClickSound();
        CommonOffthings();
        GameTitle.SetActive(false);
        Credit.SetActive(true);
        CreditSlider.SetTrigger("Slide-In");
    }

    //OnBackbtnClicked() method will control all the back button in game
    public void OnBackbtnClicked() 
    {
        Mainmenu.SetActive(true);
        GameTitle.SetActive(true);
        CreditSlider.SetTrigger("Slide-Out");
        HowtoplaySlider.SetTrigger("Slide-Out");
        SettingSlider.SetTrigger("Slide-Out");
        // Invoke("DisableSettingsPanel", 0.5f); // Adjust the delay based on your slide-out animation length
        StartCoroutine(DisableSettingsPanel());
    }

    // Disable the settings panel after the slide-out animation completes
    //public void DisableSettingsPanel()
    //{
    //    Settings.SetActive(false);
    //    SettingsBtn.SetActive(true);
    //}

    //Use of Coroutines:
    public  IEnumerator DisableSettingsPanel()
    {
        yield return new WaitForSeconds(1);
        Settings.SetActive(false);
        SettingsBtn.SetActive(true);
        Howtoplay.SetActive(false);
        Credit.SetActive(false);
  
    }

    public void OnPlayBtnClicked()
    {
        ButtonClickSound();
        int musicstate=  PlayerPrefs.GetInt("Music",1);
        GameplayMusicControl = musicstate;
        FadeOut();
        StartCoroutine(LoadLevelWithDelay());

    }

    private void FadeIn()
    {
        FadePanel.GetComponent<Animator>().SetTrigger("Fade-In");
    }
    private void FadeOut()
    {
        FadePanel.GetComponent<Animator>().SetTrigger("Fade-Out");
    }
    private IEnumerator LoadLevelWithDelay()
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(1);
    }

    public void BackGroundMusicPlay()
    {
            gameAudio.Play();
            MusicOn.SetActive(false);
            MusicOff.SetActive(true);
            PlayerPrefs.SetInt("Music", 1);
    }
    public void BackGroundMusicStop()
    {
            gameAudio.Stop();
            MusicOn.SetActive(true);
            MusicOff.SetActive(false);
            PlayerPrefs.SetInt("Music", 0);
    }

    public  void  ButtonClickSound()
    {
        gameAudio.PlayOneShot(ButtonSoundClip);   
    }
    private void CommonOffthings()
    {
        Mainmenu.SetActive(false);
        MusicOn.SetActive(false);
        MusicOff.SetActive(false);
    }

    public void ResetGame()
    {
        ButtonClickSound();
        PlayerPrefs.DeleteKey("LevelNum");
        PlayerPrefs.DeleteKey("BaseOnLevelTimeIncreament");
    }
}
