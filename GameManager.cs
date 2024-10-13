using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public int tapCount;
    public Gameplayscript other;
    private float Timer;
    private int DefaultTime=5;
    private int BaseOnLevelTimeIncreament=1;
    public bool TimeEnd= false;
    //  public bool lose_Won;
    public static int HighScore;

    private float CountDownTimer;
    public bool CountDownTimerEnd = false;

    public AudioClip CountDownBeep;

    private int TargetCount;
    private int LevelNum=1;
    private int BasedOnLevel=10;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteKey("LevelNum");
        //PlayerPrefs.DeleteKey("BaseOnLevelTimeIncreament");

        CountDownTimer = 3;
        GetHighScore();
        CountDownBeepSound();

        LevelNum= PlayerPrefs.GetInt("LevelNum",1);
        other.UpdateLevelNumber();
        TargetCount =TargetCalculation(LevelNum);
        other.UpdateLevelTargetText();

        BaseOnLevelTimeIncreament = PlayerPrefs.GetInt("BaseOnLevelTimeIncreament", 1);
        Timer = TimeCalculation(BaseOnLevelTimeIncreament);


    }

    private void Update() 
    {
        if (CountDownTimerEnd == false)
        {
            
             CountDownTimer -= Time.deltaTime;
            other.CountDownTimerText.text = " " + ((int)CountDownTimer);  
            // Debug.Log("CoundDownTimer:" + CountDownTimer);
            if (CountDownTimer<=0)
            {
            CountDownTimerEnd=true;
            other.DisableCountDownTimer();
            other.InstructionPanel.SetActive(false);
            other.Pausebtn.SetActive(true);
            //  Debug.Log("CoundDownTimer:" + CountDownTimerEnd);
            // CountDownTimer=0;

            }
        }
        if (CountDownTimerEnd&&TimeEnd==false)
        {
            Timer -= Time.deltaTime;
            other.TimeCounterText.text = "Time:" +((int)Timer);
            if (Timer<=0)
            {
                TimeEnd=true;
                Timer = 0;
                if(tapCount>=TargetCount)
                {
                    // lose_Won = true; antoher way
                    other.OnGameWinPanel();
                } 
                else
                {
                    // lose_Won = false;   another way
                    other.OnGameOvertnPanel();
                }
                // other.win_or_lost(); another way

                //Following statement set the High score
                if (HighScore<tapCount)
                {
                    HighScore = tapCount;
                }
                    other.UpdateHighScore();
                    SaveHighScore();    
            }
        }
        ControlTapCount();
    }
    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
    }
    public void GetHighScore()
    {
       HighScore= PlayerPrefs.GetInt("HighScore");
    }
    public void ControlTapCount()
    {
        if (!other.ispaused && Input.GetMouseButtonDown(0)&&CountDownTimerEnd)
        {
            other.GamePanelColorChange();
            other.OnTapCountSoundEffect();
            tapCount++;
            other.UpdateTapCount(); 
        }
    }

    private void CountDownBeepSound()
    {
        other.GamePlay.PlayOneShot(CountDownBeep);
    }

    private  int TargetCalculation(int levelnum)
    {
        int temp;
        temp = BasedOnLevel * levelnum;
        return temp;
    }

    public void LevelIncrease()
    {
        LevelNum++;
        PlayerPrefs.SetInt("LevelNum",LevelNum);
    }
    public int GetlevelIncreament()
    {
        return LevelNum;
    }
    public int GetLevelTarget()
    {
        return TargetCount;
    }

    private float TimeCalculation(int TimeIncreament)
    {
        float temp;    
        temp = DefaultTime + TimeIncreament;
        return temp;
   
    }

    public void TimeOnLevelIncrease()
    {
        if (LevelNum %3==0)
        {
        BaseOnLevelTimeIncreament++;
        }
        PlayerPrefs.SetInt("BaseOnLevelTimeIncreament", BaseOnLevelTimeIncreament);
    
 
    }
    public int GetTimeOnlevelIncreament()
    {
        return BaseOnLevelTimeIncreament;
    }

   
}
