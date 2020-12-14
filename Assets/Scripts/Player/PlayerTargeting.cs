using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour {
	//싱글톤
	public static PlayerTargeting Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<PlayerTargeting>();
				if (instance == null) {
					var instanceContainer = new GameObject("PlayerTargeting");
					instance = instanceContainer.AddComponent<PlayerTargeting>();
				}
			}
			return instance;
		}
	}
	private static PlayerTargeting instance;

	public bool getATarget = false; //타겟팅할 적이 있는가
	float currentDist = 0; //현재 타겟팅 하고있는 적과 플레이어의 거리
	float closetDist = 100f; //가장 가까운 적과 플레이어의 거리
	float TargetDist = 100f; //적과 플레이어의 거리
	int closeDistIndex = 0; //가까운 적의 인덱스 번호
	public int TargetIndex = -1; //타겟팅 중인 적의 인덱스 번호
	int prevTargetIndex = 0; //이전에 타겟팅했던 적의 인덱스 번호
	public LayerMask layerMask; //레이캐스트시에 제외시킬 레이어들

	public float atkSpd = 1f; //공격속도

	public List<GameObject> MonsterList = new List<GameObject>(); //RoomCondition에서 받아올 몬스터 리스트

	public GameObject PlayerBolt; //투사체
	public Transform AttackPoint; //투사체 발사지점

	void OnDrawGizmos() {
		//타겟과 플레이어 사이에 선을 그림
		if (getATarget) {
			//적이 있다면
			for (int i = 0; i < MonsterList.Count; i++) {
				if (MonsterList[i] == null) return;
				RaycastHit hit;
				bool isHit = Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), MonsterList[i].transform.position - transform.position, out hit, 20f, layerMask);
				//레이캐스트로 몬스터 리스트에있는 모든 몬스터의 위치에 레이저를 쏨 (바닥에 딱 붙지 않게 0.5만큼 위로 올림)
				if (isHit && hit.transform.CompareTag("Monster")) {
					//레이저가 Monster태그를 한 오브젝트에 직접 맞았다면 녹색
					Gizmos.color = Color.green;
				}
				else {
					//아니라면 빨간색
					Gizmos.color = Color.red;
				}
				//레이캐스트를 따라 선을 그림
				Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), MonsterList[i].transform.position - transform.position);
			}
		}
	}

	// Update is called once per frame
	void Update() {
		SetTarget();
		AtkTarget();
	}

	void Attack() {
		PlayerMovement.Instance.Anim.SetFloat("AttackSpeed", atkSpd); //플레이어의 애니메이션을 AttackSpeed로 변경 atkSpd 의 속도로 공격
		Instantiate(PlayerBolt, AttackPoint.position, transform.rotation); //AttackPoint지점에서 PlayerBolt를 생성후 발사함
	}

	void SetTarget() {
		if (MonsterList.Count != 0) {
			//몬스터가 있다면

			//변수 초기화
			prevTargetIndex = TargetIndex;
			currentDist = 0f;
			closeDistIndex = 0;
			TargetIndex = -1;

			for (int i = 0; i < MonsterList.Count; i++) {
				if (MonsterList[i] == null) return;
				currentDist = Vector3.Distance(transform.position, MonsterList[i].transform.position);
				//플레이어와 적과의 거리 측정
				RaycastHit hit;
				bool isHit = Physics.Raycast(transform.position, MonsterList[i].transform.position - transform.position, out hit, 20f, layerMask); //몬스터리스트 위치에 쏜 레이저가 맞았는가
				if (isHit && hit.transform.CompareTag("Monster")) {
					//맞았으며 맞은 대상이 벽이 아니라 몬스터인가
					if (TargetDist >= currentDist) {
						//현재 타겟팅중인 몬스터외의 거리가  MonsterList[i]의 거리보다 큰가
						TargetIndex = i; //타겟을 i번째 몬스터로 변경

						TargetDist = currentDist; //현재 타겟팅중인 몬스터의 거리 갱신

						if (!JoyStickMovement.Instance.isPlayerMoving && prevTargetIndex != TargetIndex) {
							TargetIndex = prevTargetIndex;
						}
					}
				}

				if (closetDist >= currentDist) {
					//가장 가까웠던 적의 거리가 MonsterList[i]와의 거리보다 큰가
					closeDistIndex = i; //가장 가까운 적의 인덱스 갱신
					closetDist = currentDist; //가장 가까운 적과의 거리 갱신
				}
			}

			if (TargetIndex == -1) {
				TargetIndex = closeDistIndex; //모든 몬스터가 벽 뒤에있다면 가장 가까운 적을 타겟팅함
			}

			//초기화
			closetDist = 100f;
			TargetDist = 100f;
			getATarget = true;
		}

	}

	void AtkTarget() {
		if (TargetIndex == -1 || MonsterList.Count == 0) {
			//타겟리스트가 비었거나 몬스터 카운터가 0이라면
			PlayerMovement.Instance.Anim.SetBool("Attack", false); //공격모션 중지
			return;
		}
		if (getATarget && !JoyStickMovement.Instance.isPlayerMoving && MonsterList.Count != 0) {
			//적이 존재하고 조이스틱이 중앙에 있고 몬스터카운터가 0이아니라면
			transform.LookAt(new Vector3(MonsterList[TargetIndex].transform.position.x, transform.position.y, MonsterList[TargetIndex].transform.position.z)); //몬스터 방향으로 캐릭터 회전
			Attack();
			if (PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
				//플레이어의 애니메이션 상태가 Idle이라면
				PlayerMovement.Instance.Anim.SetBool("Idle", false);
				PlayerMovement.Instance.Anim.SetBool("Walk", false);
				PlayerMovement.Instance.Anim.SetBool("Attack", true);
			}

		}
		else if (JoyStickMovement.Instance.isPlayerMoving) {
			//조이스틱이 중앙에 있지 않다면
			if (!PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
				//플레이어의 애니메이션 상태가 Walk가 아닐때
				PlayerMovement.Instance.Anim.SetBool("Attack", false);
				PlayerMovement.Instance.Anim.SetBool("Idle", false);
				PlayerMovement.Instance.Anim.SetBool("Walk", true);
			}
		}
		else {
			//위의 두 경우가 아니라면 기본값으로 Idle로 상태를 변경
			PlayerMovement.Instance.Anim.SetBool("Attack", false);
			PlayerMovement.Instance.Anim.SetBool("Idle", true);
			PlayerMovement.Instance.Anim.SetBool("Walk", false);
		}
	}
}