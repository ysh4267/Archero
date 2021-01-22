using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour {
    public float maxHp = 1000f;
    public float currentHp = 1000f;

    public float damage = 100f;

    protected float playerRealizeRange = 10f;
    protected float attackRange = 5f;
    protected float attackCoolTime = 5f;
    protected float attackCoolTimeCacl = 5f;
    protected bool canAtk = true;

    protected float moveSpeed = 2f;

    protected GameObject Player;
    protected NavMeshAgent nvAgent;
    protected float distance;

    protected GameObject parentRoom;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
