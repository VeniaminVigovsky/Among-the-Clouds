using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score;
    private static int bestScoreEasy;
    private static int bestScoreNormal;
    private static int bestScoreHard;
    public static bool canUpdateScore;
    public static bool dead = false;
    int scoreBenchMark = 0;
    static float scoreUpdateSpeed;
    public enum GameMode
    {
        Easy,
        Normal,
        Hard
    }

    static GameMode currentGameMode = GameMode.Normal;

    [SerializeField] PlayerAudioManager audioManager;
    private void Awake()
    {
        switch (Score.GetGameMode())
        {
            case Score.GameMode.Easy:
                scoreUpdateSpeed = 0.95f;
                break;
            case Score.GameMode.Normal:
                scoreUpdateSpeed = 1f;
                break;
            case Score.GameMode.Hard:
                scoreUpdateSpeed = 1.35f;
                break;
            default:
                scoreUpdateSpeed = 1f;

                break;
        }
    }
    private void Start()
    {
        canUpdateScore = true;
        StartCoroutine(ScoreUpdate());
    }

    // Update is called once per frame
    void  Update()
    {
        if (score - scoreBenchMark >= 100)
        {
            scoreBenchMark = score;
            PlayerDeath.liveCount++;
            audioManager?.PlayLiveGainSound();
        }
        
        if (score < 0)
        {
            score = 0;
        }

        if (dead)
        {
            canUpdateScore = false;
            StopAllCoroutines();

        }
    }

    public static IEnumerator ScoreUpdate()
    {
        while (true)
        {
            if (canUpdateScore)
            {
                score++;
            }
            yield return new WaitForSeconds(scoreUpdateSpeed);
            
        }
    }

    public static void UpdateBestScore()
    {
        switch (currentGameMode)
        {
            case GameMode.Easy:
                bestScoreEasy = score > bestScoreEasy ? score : bestScoreEasy;
                break;
            case GameMode.Normal:
                bestScoreNormal = score > bestScoreNormal ? score : bestScoreNormal;
                break;
            case GameMode.Hard:
                bestScoreHard = score > bestScoreHard ? score : bestScoreHard;
                break;
            default: break;
        }

        SaveScore();
        
    }

    public static int GetBestScore()
    {
        switch (currentGameMode)
        {
            case GameMode.Easy:
                return bestScoreEasy;
                
            case GameMode.Normal:
                return bestScoreNormal;                
            case GameMode.Hard:
                return bestScoreHard;
            default: return 0;    
        }
    }

    public static void SetHardMode()
    {
        currentGameMode = GameMode.Hard;
    }

    public static void SetNormalMode()
    {
        currentGameMode = GameMode.Normal;
    }

    public static void SetEasyMode()
    {
        currentGameMode = GameMode.Easy;
    }

    public static GameMode GetGameMode()
    {
        return currentGameMode;
    }

    private static int GetGameModeID()
    {
        switch (currentGameMode)
        {
            case GameMode.Easy:
                return 0;

            case GameMode.Normal:
                return 1;
            case GameMode.Hard:
                return 2;
            default: return 1;
        }
    }

    private static void OnApplicationQuit()
    {
        UpdateBestScore();
        SaveScore();
    }

    private static void SaveScore()
    {
        SaveData saveData = new SaveData(bestScoreEasy, bestScoreNormal, bestScoreHard, GetGameModeID());

        SaveLoadSystem.SaveGameData(saveData);
    }

    public static void LoadScore(SaveData data)
    {
        bestScoreEasy = data != null ? data.bestScoreEasy : 0;
        bestScoreNormal = data != null ? data.bestScoreNormal : 0;
        bestScoreHard = data != null ? data.bestScoreHard : 0;
    }
}
