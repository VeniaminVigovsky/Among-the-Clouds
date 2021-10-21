using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSideSwitchState : State
{
    float switchTime = 3f;
    float timeSwitched;

    bool v = false;

    public OneSideSwitchState(GameObject topGenerator, GameObject bottomGenerator) : base(topGenerator, bottomGenerator)
    {
    }

    public override void StartState()
    {
        base.StartState();        
        _topGenerator.generatePlatform = v;
        _bottomGenerator.generatePlatform = !v;
        timeSwitched = Time.time;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (Time.time > timeSwitched + switchTime)
        {
            v = !v;
            _topGenerator.generatePlatform = v;
            _bottomGenerator.generatePlatform = !v;
            timeSwitched = Time.time;
        }
    }

    public override void EndState()
    {
        base.EndState();
        _topGenerator.generatePlatform = true;
        _bottomGenerator.generatePlatform = true;
    }
}
