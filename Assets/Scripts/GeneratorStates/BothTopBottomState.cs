using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothTopBottomState : State
{
    public BothTopBottomState(GameObject topGenerator, GameObject bottomGenerator) : base(topGenerator, bottomGenerator)
    {

    }

    public override void StartState()
    {
        base.StartState();
        _topGenerator.generatePlatform = true;
        _bottomGenerator.generatePlatform = true;

    }

    public override void UpdateState()
    {
        base.UpdateState();

    }

    public override void EndState()
    {
        base.EndState();

    }
}
