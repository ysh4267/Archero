using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//몬스터가 갖는 기본 스텟과 공격 쿨타임 등의 값들이 저장되는 베이스
public class EnemyBase : MonoBehaviour {
    public float maxHp = 1000f;
    public float currentHp = 1000f;

    public float damage = 100f;

    protected float playerRealizeRange = 10f; //플레이어를 인식하는 범위
    protected float attackRange = 5f; //공격사거리
    protected float attackCoolTime = 5f; //공격쿨타임 기본값
    protected float attackCoolTimeCacl = 5f; //공격쿨타임
    protected bool canAtk = true; //공격쿨타임중인지 판별하기위한 불값

    protected float moveSpeed = 2f; //이동속도

    protected GameObject Player;
    protected NavMeshAgent nvAgent;
    protected float distance; //플레이어와의 거리값

    protected GameObject parentRoom; //Enemy가 속한 맵

    protected Animator Anim;
    protected Rigidbody rb;

    //public LayerMask layerMask; //레이캐스트를 위한 레이어마스크

    // Start is called before the first frame update
    protected void Start() {
        Player = GameObject.FindGameObjectWithTag("Player"); //플레이어 태그로 지정
        Debug.Log("Player : " + Player);
        Debug.Log("Player.transform.position : " + Player.transform.position);

        //EnemyBase의 하위객체에서 쓰는 변수들의 초기화
        nvAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();

        //Enemy가 소속된 맵
        parentRoom = transform.parent.transform.parent.gameObject;
        //공격 쿨타임을 코루틴을 활용하여 계속 실행시킴
        StartCoroutine(CalcCoolTime());
    }

    // Update is called once per frame
    void Update() {

    }
    //공격 사거리안에 Player를 감지하는 함수
    protected bool CanAtkStateFun() {
        RaycastHit hit;
        Vector3 targetDir = new Vector3(Player.transform.position.x - transform.position.x, 0f, Player.transform.position.z - transform.position.z); //Enemy에서 Player쪽 방향 벡터값
        Physics.Raycast(new Vector3(transform.position.x, 0.5f, transform.position.z), targetDir, out hit, 30f); //Enemy에서 targetDir방향으로 30거리만큼만 레이캐스트
        distance = Vector3.Distance(Player.transform.position, transform.position); //Enemy와 Player사이의 거리값
        Debug.DrawRay(new Vector3(transform.position.x, 0.5f, transform.position.z), targetDir * 30f, Color.green);
        //30거리안에 플레이어가 없을시 false
        if(hit.transform == null) {
            Debug.Log("hit.transForm == null");
            return false;
        }
        //플레이어에게 레이캐스트가 적중하고 Enemy와 Player사이의 거리값이 공격사거리보다 작으면 true
        if(hit.transform.CompareTag("Player")&&distance <= attackRange) {
            Debug.Log("사거리 안의 플레이어 감지됨");
            return true;
        }
        //예외처리용 false
        else {
            return false;
        }
    }

    //공격 쿨타임을 계산하는 코루틴
    protected virtual IEnumerator CalcCoolTime() {
        while (true) {
            yield return null;
            //공격이 시작됬다면 쿨타임 계산 시작
            if(!canAtk) {
                //Time.deltaTime만큼 attackCoolTimeCacl계산
                attackCoolTimeCacl -= Time.deltaTime;
                //attackCoolTimeCacl이 0에 근접하면 attackCoolTime값으로 초기화
                if(attackCoolTimeCacl <= 0){
                    attackCoolTimeCacl = attackCoolTime;
                    canAtk = true; //공격이 다시 가능함
                }
            }
        }
    }
}
