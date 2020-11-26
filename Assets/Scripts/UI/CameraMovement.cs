﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
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

	// Update is called once per frame
	void LateUpdate() {
		cameraPosition.y = Player.transform.position.y + offsetY;
		cameraPosition.z = Player.transform.position.z + offsetZ;

		transform.position = cameraPosition;
	}

	public void CameraNextRoom() {
		cameraPosition.x = Player.transform.position.x;
	}
}
