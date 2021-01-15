using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
	//싱글톤
	public static StageManager Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<StageManager>();
				if (instance == null) {
					var instanceContainer = new GameObject("StageManager");
					instance = instanceContainer.AddComponent<StageManager>();
				}
			}
			return instance;
		}
	}
	public static StageManager instance;

	public GameObject Player;

	[System.Serializable]
	public class StartPositionArray {
		public List<Transform> StartPosition = new List<Transform>();
	}//Normal Stage 시작위치들

	public StartPositionArray[] startPositionArrays;//Floor 구분을 위한 2차원 배열
	//startPositionArrays[1] 1~9 startPositionArrays[2] 2~9 방식

	public List<Transform> StartPositionAngel = new List<Transform>();//천사방
	public List<Transform> StartPositionBoss = new List<Transform>();//중보스 방
	public Transform StartPositionFinalBoss;//막보방

	public int currentStage = 0;
	int LastStage = 20;

	// Start is called before the first frame update
	void Start() {
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update() {

	}

	public void NextStage() {
		currentStage++;
		if (currentStage > LastStage) return;//막보를 잡았으면 리턴

		if (currentStage % 5 != 0) {
			//normal Stage
			int arrayIndex = currentStage / 10;//현재 Floor가 1층인지 2층인지 판별
			int randomIndex = Random.Range(0, startPositionArrays[arrayIndex].StartPosition.Count);//해당 층수에서 랜덤한 맵 선별
			Player.transform.position = startPositionArrays[arrayIndex].StartPosition[randomIndex].position;//플레이어 이동
			startPositionArrays[arrayIndex].StartPosition.RemoveAt(randomIndex);//현재 방을 앞으로 나올 리스트에서 삭제
		}
		else {
			if (currentStage % 10 == 5) {
				//Angel Stage
				int randomIndex = Random.Range(0, StartPositionAngel.Count);
				Player.transform.position = StartPositionAngel[randomIndex].position;
			}
			else {
				//FinalBoss Stage
				if (currentStage == LastStage) {
					Player.transform.position = StartPositionFinalBoss.position;
				}
				else {
					//Boss Stage
					int randomIndex = Random.Range(0, StartPositionBoss.Count);
					Player.transform.position = StartPositionBoss[randomIndex].position;
					StartPositionBoss.RemoveAt(currentStage / 10);
				}
			}
		}
		CameraMovement.Instance.CameraNextRoom();
	}
}
