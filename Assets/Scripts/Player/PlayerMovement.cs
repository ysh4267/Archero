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
	public Animator Anim;

	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody>();
		Anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate() {
		if (JoyStickMovement.Instance.joyVec.x != 0 || JoyStickMovement.Instance.joyVec.y != 0) {
			rb.velocity = new Vector3(JoyStickMovement.Instance.joyVec.x, 0, JoyStickMovement.Instance.joyVec.y) * moveSpeed;
			rb.rotation = Quaternion.LookRotation(new Vector3(JoyStickMovement.Instance.joyVec.x, 0, JoyStickMovement.Instance.joyVec.y));
		}
	}
}
