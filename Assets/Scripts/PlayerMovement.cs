using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	//싱글톤 방식
	public static PlayerMovement Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<PlayerMovement>();
				if (instance == null) {
					var instanceContainer = new GameObject("PlayerMovemant");
					instance = instanceContainer.AddComponent<PlayerMovement>();
				}
			}
			return instance;
		}
	}
	private static PlayerMovement instance;

	Rigidbody rb;
	public float moveSpeed = 25f;

	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Debug.Log("가로" + moveHorizontal + " / 세로" + moveVertical);
		rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);
	}
}
