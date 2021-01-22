using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDuck : EnemyMeleeFSM {
    public GameObject enemyCanvasGo;
    public GameObject meleeAtkArea;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerRealizeRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // Start is called before the first frame update
    void Start() {
        //base.Start();

        attackCoolTime = 2f;
        attackCoolTimeCacl = attackCoolTime;

        attackRange = 3f;
        nvAgent.stoppingDistance = 1f;

        StartCoroutine(ResetAtkArea());
    }

    IEnumerator ResetAtkArea() {
        while(true){
            yield return null;
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
