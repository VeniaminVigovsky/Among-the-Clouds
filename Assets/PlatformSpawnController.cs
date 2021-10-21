using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnController : MonoBehaviour
{
    [SerializeField]
    GameObject topGenerator, bottomGenerator;
    BothTopBottomState bothTopBottomState;
    OneSideSwitchState oneSideSwitchState;
    ThreeToThreeState threeToThreeState;
    List<State> states = new List<State>();
    List<State> usedStates = new List<State>();

    State currentState;
    void Start()
    {
        if (topGenerator == null || bottomGenerator == null) return;
        bothTopBottomState = new BothTopBottomState(topGenerator, bottomGenerator);
        oneSideSwitchState = new OneSideSwitchState(topGenerator, bottomGenerator);
        threeToThreeState = new ThreeToThreeState(topGenerator, bottomGenerator);
        states.Add(bothTopBottomState);
        states.Add(oneSideSwitchState);
        states.Add(threeToThreeState);
        InitializeStateMachine(bothTopBottomState);



    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();

        if (states.Count == 0)
        {
            foreach (var state in usedStates)
            {
                states.Add(state);
                
            }
            usedStates.Clear();
        }

        if (currentState.stateDuration + currentState.stateStartTime < Time.time && states.Count != 0)
        {
            int r = Random.Range(0, states.Count);
            State newState = states[r];
            usedStates.Add(newState);
            states.Remove(states[r]);
            ChangeState(newState);
        }

    }

    private void ChangeState(State newState)
    {
        if (newState == null) return;
        currentState.EndState();
        currentState = newState;
        currentState.StartState();

    }

    private void InitializeStateMachine(State startingState)
    {
        if (startingState == null) return;
        usedStates.Add(startingState);
        states.Remove(startingState);
        currentState = startingState;
        currentState.StartState();
    }

}
