using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	//싱글톤
	public static CameraMovement Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<CameraMovement>();
				if (instance == null) {
					var instanceContainer = new GameObject("CameraMovement");
					instance = instanceContainer.AddComponent<CameraMovement>();
				}
			}
			return instance;
		}
	}
	private static CameraMovement instance;

	public GameObject Player;

	public float offsetY = 10f;
	public float offsetZ = -1f;

	Vector3 cameraPosition;

	void Start() {
		cameraPosition.x = Player.transform.position.x;
	}

	// Update is called once per frame
	void LateUpdate() {
		//플레이어에 맞추어서 위 아래로만 이동
		cameraPosition.y = Player.transform.position.y + offsetY;
		cameraPosition.z = Player.transform.position.z + offsetZ;

		transform.position = cameraPosition;
	}

	public void CameraNextRoom() {
		//방을 옮기면 좌우 이동
		cameraPosition.x = Player.transform.position.x;
	}
}
