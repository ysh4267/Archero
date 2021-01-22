using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeFSM : EnemyBase {
    public enum State {
        Idle,
        Move,
        Attack,
    };

    public State currentState = State.Idle;

    WaitForSeconds Delay500 = new WaitForSeconds(0.5f);
    WaitForSeconds Delay250 = new WaitForSeconds(0.25f);

    // Start is called before the first frame update
    void Start() {
        StartCoroutine (FSM());
    }

    protected virtual IEnumerator FSM() {
        yield return null;

        while(!parentRoom.GetComponent<RoomCondition>().playerInThisRoom) {
            yield return Delay500;
        }

        while(true) {
            yield return StartCoroutine(currentState.ToString());
        }
    }

    protected virtual IEnumerator Idle() {
        yield return null;

        currentState = State.Attack;
        currentState = State.Idle;
        currentState = State.Move;
    }

    // Update is called once per frame
    void Update() {

    }
}
