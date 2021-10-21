using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public int bestScoreEasy { get; private set; }
    public int bestScoreNormal { get; private set; }
    public int bestScoreHard { get; private set; }

    public int gameModeID { get; private set; }

    public SaveData(int _bestScoreEasy, int _bestScoreNormal, int _bestScoreHard, int _gameModeID)
    {
        bestScoreEasy = _bestScoreEasy;
        bestScoreNormal = _bestScoreNormal;
        bestScoreHard = _bestScoreHard;
        gameModeID = _gameModeID;
    }



}
