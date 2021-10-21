using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static bool loaded = false;

    private void Awake()
    {
        if (!loaded)
        {
            SaveData saveData = SaveLoadSystem.LoadGameData();
            if (saveData != null)
            {
                switch (saveData.gameModeID)
                {
                    case 0: Score.SetEasyMode(); break;
                    case 1: Score.SetNormalMode(); break;
                    case 2: Score.SetHardMode(); break;
                    default: Score.SetNormalMode(); break;

                }

                Score.LoadScore(saveData);
                loaded = true;
            }
        }




    }

    private void OnApplicationQuit()
    {
        Score.UpdateBestScore();
        
    }

    private void OnApplicationPause()
    {
        Score.UpdateBestScore();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            Score.UpdateBestScore();
    }

}


