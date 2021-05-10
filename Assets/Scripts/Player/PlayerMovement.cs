using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	//싱글톤
	public static PlayerMovement Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<PlayerMovement>();
				if (instance == null) {
					var instanceContainer = new GameObject("PlayerMovement");
					instance = instanceContainer.AddComponent<PlayerMovement>();
				}
			}
			return instance;
		}
	}
	private static PlayerMovement instance;

	Rigidbody rb;
	public float moveSpeed = 25f; //이동속도
	public Animator Anim; //모션 애니메이션

	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody>();
		Anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate() {
		if (JoyStickMovement.Instance.joyVec.x != 0 || JoyStickMovement.Instance.joyVec.y != 0) {
			//조이스틱이 중심에 있지 않을때
			rb.velocity = new Vector3(JoyStickMovement.Instance.joyVec.x, 0, JoyStickMovement.Instance.joyVec.y) * moveSpeed; //조이스틱의 x, z벡터로 캐릭터를 이동시킴 (가시성을 위해 z의 y표기)
			rb.rotation = Quaternion.LookRotation(new Vector3(JoyStickMovement.Instance.joyVec.x, 0, JoyStickMovement.Instance.joyVec.y)); //캐릭터의 앞을 조이스틱의 x, z각도만큼 돌림
		}
	}

	private void OnTriggerEnter (Collider other) {
		if (other.transform.CompareTag("NextRoom")) {
			Debug.Log("NextRom Tag");
			StageManager.Instance.NextStage();
		}
	}
}
