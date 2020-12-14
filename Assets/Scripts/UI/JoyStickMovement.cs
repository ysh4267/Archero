using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMovement : MonoBehaviour {
	//싱글톤
	public static JoyStickMovement Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<JoyStickMovement>();
				if (instance == null) {
					var instanceContainer = new GameObject("JoyStickMovement");
					instance = instanceContainer.AddComponent<JoyStickMovement>();
				}
			}
			return instance;
		}
	}
	private static JoyStickMovement instance;

	public GameObject smallStick; //스틱
	public GameObject bGStick; //스틱배경
	Vector3 stickFirstPosition; //터치를 시작한 스틱 위치
	public Vector3 joyVec; //조이스틱 위치
	Vector3 joyStickFirstPosition; //스틱 시작위치
	float stickRadius; //스틱크기
	public bool isPlayerMoving = false; //조이스틱을 움직이는 중인지 판별하는 변수

	// Start is called before the first frame update
	void Start() {
		stickRadius = bGStick.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2; //스틱사이즈는 배경의 절반
		joyStickFirstPosition = bGStick.transform.position; //스틱의 시작위치는 스틱배경의 정 중앙
	}

	public void PointDown() {
		//마우스위치에 스틱이동후 위치 저장
		bGStick.transform.position = Input.mousePosition;
		smallStick.transform.position = Input.mousePosition;
		stickFirstPosition = Input.mousePosition;

		if (!PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
			//플레이어의 상태가 Walk가 아니라면
			PlayerMovement.Instance.Anim.SetBool("Attack", false);
			PlayerMovement.Instance.Anim.SetBool("Idle", false);
			PlayerMovement.Instance.Anim.SetBool("Walk", true);
		}
		isPlayerMoving = true; //조이스틱 사용중
		PlayerTargeting.Instance.getATarget = false; //PlayerTargeting의 타겟팅 중지
	}

	public void Drag(BaseEventData baseEventData) {
		PointerEventData pointerEventData = baseEventData as PointerEventData; //BaseEventData를 사용해 마우스 위치 받아옴
		Vector3 DragPosition = pointerEventData.position;
		joyVec = (DragPosition - stickFirstPosition).normalized; //stickFirstPosition과 DragPosition의 차를 Vector3의 normalized함수를 이용해 벡터의 형태로 바꿈
		float stickDistance = Vector3.Distance(DragPosition, stickFirstPosition); //배경 밖으로 스틱이 빠져나가지 않게하기위한 변수

		if (stickDistance < stickRadius) {
			//스틱위치가 stickRadius보다 작으면 마우스 위치에 스틱을 배치
			smallStick.transform.position = stickFirstPosition + joyVec * stickDistance;
		}
		else {
			//아니라면 해당 방향에 끝부분에 스틱을 배치
			smallStick.transform.position = stickFirstPosition + joyVec * stickRadius;
		}
	}

	public void Drop() {
		//0, 0, 0으로 조이스틱 이동
		joyVec = Vector3.zero;
		bGStick.transform.position = joyStickFirstPosition;
		smallStick.transform.position = joyStickFirstPosition;

		if (!PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			//플레이어의 애니메이션이 Idle상태가 아니라면
			PlayerMovement.Instance.Anim.SetBool("Attack", false);
			PlayerMovement.Instance.Anim.SetBool("Walk", false);
			PlayerMovement.Instance.Anim.SetBool("Idle", true);
		}
		isPlayerMoving = false; //조이스틱에서 손을 뗌
	}
}
