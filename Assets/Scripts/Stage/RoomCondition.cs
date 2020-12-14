using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCondition : MonoBehaviour {
	List<GameObject> MonsterListInRoom = new List<GameObject>(); //해당 방에 존재하는 몬스터 리스트
	public bool playerInThisRoom = false; //플레이어가 스테이지에 진입 했는가
	public bool isClearRoom = false; //클리어 확인을 위한 변수
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		//스테이지 클리어 판별
		if (playerInThisRoom) {
			if (MonsterListInRoom.Count <= 0 && !isClearRoom) {
				isClearRoom = true;
			}
		}
	}
	//방에 존재하는 몬스터와 플레이어를 인식하기위한 콜라이더 함수
	private void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")) {
			//플레이어가 방에 들어왔는가
			playerInThisRoom = true;
			PlayerTargeting.Instance.MonsterList = new List<GameObject>(MonsterListInRoom); //방에 존재하는 몬스터 리스트를 PlayerTargeting에 전달
		}
		if (other.CompareTag("Monster")) {
			//몬스터가 방에 있는가
			MonsterListInRoom.Add(other.gameObject); //콜라이더에 부딪힌 몬스터를 MonsterListInRoom에 추가
			Debug.Log(other.gameObject.name);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")) {
			//플레이어가 빠져나감
			playerInThisRoom = false;
		}
		if (other.CompareTag("Monster")) {
			//몬스터를 쓰러트림
			MonsterListInRoom.Remove(other.gameObject);
		}
	}
}
