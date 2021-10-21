using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    public float stateDuration { get; protected set; }
    public float stateStartTime { get; private set; } 

    float minDuration = 6f;
    float maxDuration = 10f;

    protected PlatformGenerator _topGenerator, _bottomGenerator;

    public State(GameObject topGenerator, GameObject bottomGenerator)
    {
        _topGenerator = topGenerator.GetComponent<PlatformGenerator>();
        _bottomGenerator = bottomGenerator.GetComponent<PlatformGenerator>();
    }

    public virtual void StartState()
    {
        stateStartTime = Time.time;
        stateDuration = Random.Range(minDuration, maxDuration);
        _topGenerator.generatePlatform = true;
        _bottomGenerator.generatePlatform = true;
    }

    public virtual void EndState()
    {
        _topGenerator.generatePlatform = true;
        _bottomGenerator.generatePlatform = true;
    }

    public virtual void UpdateState()
    {

    }
}
