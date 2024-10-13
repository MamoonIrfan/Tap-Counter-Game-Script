using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Gameplayscript : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject GameWinPanel;
    public GameObject GameOverPanel;
    public GameObject InstructionPanel;
    public GameObject Pausebtn;
  //  public GameObject GameWinbtn;
  //  public GameObject GameOverbtn;
    public GameManager other;
    public GameObject TapCounterText;

     public Text TimeCounterText;
    //public Text TapCounterText;

    public Text HighScoreText;

    //check game paused or not:
    public bool ispaused=false;
    // public bool OverPanelOpened=false;
    public Text CountDownTimerText;

    public AudioSource GamePlay;
    public AudioClip ButtonClickSoundClip;
    public AudioClip[] TapCountClickSound;

    public Text LevelNumber;

    public Text LevelTargetText;

    public GameObject[] GamePlayPanel;
    void Start()
    {
        GamePlayMusic();
        PausePanel.SetActive(false);
        GameWinPanel.SetActive(false);  
        GameOverPanel.SetActive(false);
        InstructionPanel.SetActive(true);  
        Pausebtn.SetActive(false);
      //  InstructionPanel.SetActive(false);
    }

    //public void BackBtnClicked()
    //{
    //    SceneManager.LoadScene("MainMenuScene");
    //}

    public void OnPauseBtnClicked()
    {
            GamePlay.Stop();
            ButtonClickSound();
            PausePanel.SetActive(true);
         //   GameWinbtn.SetActive(false);
          //  GameOverbtn.SetActive(false);
            Pausebtn.SetActive(false);
        InstructionPanel.SetActive(false);
        ispaused = true;
            Time.timeScale = 0;
    }

    public void OnResumeBtnClicked()
    {
             GamePlayMusic();
             ButtonClickSound();
            PausePanel.SetActive(false);
        InstructionPanel.SetActive(false);
        //  GameWinbtn.SetActive(true);
        //  GameOverbtn.SetActive(true);
        Pausebtn.SetActive(true);
            ispaused = false;
            Time.timeScale = 1;

    }
    //public void TogglePause()
    //{ 
    //    if (ispaused)
    //    {
    //        // Resume game
    //        PausePanel.SetActive(false);
    //        GameWinbtn.SetActive(true);
    //        GameOverbtn.SetActive(true);
    //        Pausebtn.SetActive(true);
    //        ispaused = false;
    //        Time.timeScale = 1;
    //    }
    //    else
    //    {
    //        // Pause game
    //        PausePanel.SetActive(true);
    //        GameWinbtn.SetActive(false);
    //        GameOverbtn.SetActive(false);
    //        Pausebtn.SetActive(false);
    //        ispaused = true;
    //        Time.timeScale = 0;
           
    //    }
    //}
    public void OnMainMenuBtnClicked()
    {
        ButtonClickSound();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnGameWinPanel()
    {
        GamePlay.Stop();
        GameWinPanel.SetActive(true);
       // GameOverbtn.SetActive(false);
        Pausebtn.SetActive(false);
        InstructionPanel.SetActive(false);
        // GameWinbtn.SetActive(false);
        ispaused = true;
        Time.timeScale = 0;
    }

    public void OnGameOvertnPanel()
    {
        GamePlay.Stop();
        GameOverPanel.SetActive(true);
     //   GameWinbtn.SetActive(false);
        Pausebtn.SetActive(false);
        InstructionPanel.SetActive(false);
        //   GameOverbtn.SetActive(false);
        ispaused =true;
        Time.timeScale = 0;
    }

    public void UpdateTapCount()
    {
        TapCounterText.GetComponent<Text>().text = other.tapCount.ToString();
        //TapCounterText.text = tapCount.ToString();
    }
    public void UpdateHighScore()
    {
       HighScoreText.text = "High Score:" + GameManager.HighScore.ToString();
        
    }
    public void DisableCountDownTimer()
    {
        //CountDownTimerText.GetComponent<Text>().text = " " + ((int)other.CountDownTimer); Alternate way
        if(other.CountDownTimerEnd)
        {
        //If here the countdown is text type then we used enable perportie:
        //CountDownTimeText.gameobject.SetActive(false);
            CountDownTimerText.gameObject.SetActive(false);
        }

    }
    public void OnRestartBtnClicked()
    {
        ButtonClickSound();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    //Alternate way to show win or lose screens:
    //public void win_or_lost()
    //{
    //    if (other.lose_Won==true)
    //    {
    //        GameWinPanel.SetActive(true);
    //    }
    //    else
    //    {
    //        GameOverPanel.SetActive(true);
    //    }
    //}

    public void GamePlayMusic()
    {
        if (MainMenu.GameplayMusicControl==1)
        {
            GamePlay.Play();
        }
        else
        {
            GamePlay.Stop();
        }
    }
    public void ButtonClickSound()
    {
        GamePlay.PlayOneShot(ButtonClickSoundClip);
    }
    public void OnTapCountSoundEffect()
    {
        int random=Random.Range(0,TapCountClickSound.Length);  
        GamePlay.PlayOneShot(TapCountClickSound[random]);
    }

    public void OnNextBtnClicked()
    {
        other.LevelIncrease();
        other.TimeOnLevelIncrease();
        // Debug.Log("Level Number:"+other.GetlevelIncreament());
        Debug.Log("Time:"+other.GetTimeOnlevelIncreament());
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateLevelNumber()
    {
        LevelNumber.text = "Level:" + other.GetlevelIncreament().ToString();
    }
    public void UpdateLevelTargetText()
    {
        LevelTargetText.text = "Your Target is: " + other.GetLevelTarget().ToString();
    }

    public void GamePanelColorChange()
    {
        // Generate a random index between 0 and the length of the array
        int randomIndex = Random.Range(0, GamePlayPanel.Length);

        // Loop through all panels
        for (int i = 0; i < GamePlayPanel.Length; i++)
        {
            // Check if the panel is the randomly selected one
            if (i == randomIndex)
            {
                // Activate the randomly selected panel
                GamePlayPanel[i].SetActive(true);
            }
            else
            {
                // Deactivate the other panels
                GamePlayPanel[i].SetActive(false);
            }
        }

    }
}
