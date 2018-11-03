using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE = 0,
    RUN,
    CHASE,
    ATTACK
}

public class FSMManager : MonoBehaviour {

    public PlayerState currentState;
    public PlayerState startState;

    Dictionary<PlayerState, PlayerFSMState> states
        = new Dictionary<PlayerState, PlayerFSMState>();

    public Transform marker;

    private void Awake()
    {
        marker = GameObject.FindGameObjectWithTag("Marker").transform;

        currentState = startState;
        states.Add(PlayerState.IDLE, GetComponent<PlayerIDLE>());
        states.Add(PlayerState.RUN, GetComponent<PlayerRUN>());
        states.Add(PlayerState.CHASE, GetComponent<PlayerCHASE>());
        states.Add(PlayerState.ATTACK, GetComponent<PlayerATTACK>());
    }

    private void Start()
    {
        states[PlayerState.IDLE].enabled = false;
        states[PlayerState.RUN].enabled = false;
        states[PlayerState.CHASE].enabled = false;
        states[PlayerState.ATTACK].enabled = false;

        states[startState].enabled = true;
    }

    public void StateModify(PlayerState state)
    {
        foreach(PlayerFSMState fsm in states.Values)
        {
            fsm.enabled = false;
        }

        states[state].enabled = true;
        currentState = state;
    }

    private void Update()
    {
        transform.LookAt(marker);
        if (Input.GetMouseButton(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(r, out hit, 1000f))
            {
                marker.position = hit.point;

                StateModify(PlayerState.RUN);
            }
        }
    }
}
