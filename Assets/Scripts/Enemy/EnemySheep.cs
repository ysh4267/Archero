using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySheep : MonoBehaviour {
	GameObject Player;
	RoomCondition RoomConditionGO;

	public GameObject DangerMarker;
	public GameObject EnemyBolt;

	public Transform BoltGenPosition;
	public LayerMask layerMask;

	// Start is called before the first frame update
	void Start() {
		Player = GameObject.Find("JunkoChan");
		RoomConditionGO = transform.parent.transform.parent.GetComponent<RoomCondition>();
		StartCoroutine(WaitPlayer());
	}

	IEnumerator WaitPlayer() {
		yield return null;

		while (!RoomConditionGO.playerInThisRoom) {
			yield return new WaitForSeconds(0.5f);
		}
		yield return new WaitForSeconds(4f);
		transform.LookAt(Player.transform.position);
		Debug.Log(Player.transform.position);
		DangerMarkerShoot();

		yield return new WaitForSeconds(2f);
		Shoot();
	}

	void DangerMarkerShoot() {
		Vector3 NewPosition = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
		Physics.Raycast(NewPosition, transform.forward, out RaycastHit hit, 30f, layerMask);

		if (hit.transform.CompareTag("Wall")) {
			GameObject DangerMarkerClone = Instantiate(DangerMarker, NewPosition, transform.rotation);
			DangerMarkerClone.GetComponent<DangerLine>().EndPosition = hit.point;
		}
	}

	void Shoot() {
		Vector3 CurrentRotation = transform.eulerAngles + new Vector3(-90, 0, 0);
		Instantiate(EnemyBolt, BoltGenPosition.position, Quaternion.Euler(CurrentRotation));
	}

	// Update is called once per frame
	void Update() {

	}
}
