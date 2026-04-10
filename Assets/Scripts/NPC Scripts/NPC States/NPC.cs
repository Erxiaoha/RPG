using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public enum NPCState { Default, Patrol, Wander, Talk, Idle}
    public NPCState currentState;
    private NPCState defaultState;
    public NPC_Patrol patrol;
    public NPC_Wander wander;
    public NPC_Talk talk;
    public NPC_Idle idle;

    void Start()
    {
        defaultState = currentState;
        SwitchState(currentState);
    }

    public void SwitchState(NPCState newState)
    {
        currentState = newState;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        patrol.enabled = newState == NPCState.Patrol;
        wander.enabled = newState == NPCState.Wander;
        talk.enabled = newState == NPCState.Talk;
        idle.enabled = newState == NPCState.Idle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwitchState(NPCState.Talk);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwitchState(defaultState);
        }
    }
}
