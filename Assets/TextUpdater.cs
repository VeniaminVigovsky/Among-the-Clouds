using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{

    Text text;
    int bestScore;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        bestScore = Score.GetBestScore();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"SCORE:{Score.score} \nBEST:{bestScore} \nLIVES:{PlayerDeath.liveCount}";
    }
}
