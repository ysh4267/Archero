﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMovement : MonoBehaviour {
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

	public GameObject smallStick;
	public GameObject bGStick;
	Vector3 stickFirstPosition;
	public Vector3 joyVec;
	Vector3 joyStickFirstPosition;
	float stickRadius;
	public bool isPlayerMoving = false;

	// Start is called before the first frame update
	void Start() {
		stickRadius = bGStick.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
		joyStickFirstPosition = bGStick.transform.position;
	}

	public void PointDown() {
		bGStick.transform.position = Input.mousePosition;
		smallStick.transform.position = Input.mousePosition;
		stickFirstPosition = Input.mousePosition;

		if (!PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
			PlayerMovement.Instance.Anim.SetBool("Attack", false);
			PlayerMovement.Instance.Anim.SetBool("Idle", false);
			PlayerMovement.Instance.Anim.SetBool("Walk", true);
		}
		isPlayerMoving = true;
		//PlayerTargeting.Instance.getATarget = false;
	}

	public void Drag(BaseEventData baseEventData) {
		PointerEventData pointerEventData = baseEventData as PointerEventData;
		Vector3 DragPosition = pointerEventData.position;
		joyVec = (DragPosition - stickFirstPosition).normalized;
		float stickDistance = Vector3.Distance(DragPosition, stickFirstPosition);

		if (stickDistance < stickRadius) {
			smallStick.transform.position = stickFirstPosition + joyVec * stickDistance;
		}
		else {
			smallStick.transform.position = stickFirstPosition + joyVec * stickRadius;
		}
	}

	public void Drop() {
		joyVec = Vector3.zero;
		bGStick.transform.position = joyStickFirstPosition;
		smallStick.transform.position = joyStickFirstPosition;

		if (!PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			PlayerMovement.Instance.Anim.SetBool("Attack", false);
			PlayerMovement.Instance.Anim.SetBool("Walk", false);
			PlayerMovement.Instance.Anim.SetBool("Idle", true);
		}
		isPlayerMoving = false;
	}
}
