using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeToThreeState : State
{
    PlatformGenerator topGenerator, bottomGenerator;

    int platformCount = 0;
    int maxPlatformCount = 3;

    bool v = false;

    public ThreeToThreeState(GameObject topGenerator, GameObject bottomGenerator) : base(topGenerator, bottomGenerator)
    {
        this.topGenerator = topGenerator.GetComponent<PlatformGenerator>();
        this.bottomGenerator = bottomGenerator.GetComponent<PlatformGenerator>();
    }



    public override void StartState()
    {
        base.StartState();
        topGenerator.PlatformGenerated += OnPlatformGenerated;
        bottomGenerator.PlatformGenerated += OnPlatformGenerated;
        _topGenerator.generatePlatform = v;
        _bottomGenerator.generatePlatform = !v;

    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (platformCount >= maxPlatformCount)
        {
            platformCount = 0;
            v = !v;
            _topGenerator.generatePlatform = v;
            _bottomGenerator.generatePlatform = !v;
        }


    }
    public override void EndState()
    {
        base.EndState();
        topGenerator.PlatformGenerated -= OnPlatformGenerated;
        bottomGenerator.PlatformGenerated -= OnPlatformGenerated;
    }

    private void OnPlatformGenerated()
    {
        platformCount++;
    }
}
