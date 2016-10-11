using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float rotationSpeed;

	public Camera playerCam;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		float ver = Input.GetAxis("Vertical");
		float hor = Input.GetAxis("Horizontal");
		float mouseX = Input.GetAxis ("Mouse X");
		float mouseY = Input.GetAxis ("Mouse Y");

		Vector3 direction = new Vector3 (hor, 0.0f, ver);
		Vector3 rotationHor = new Vector3 (0.0f, mouseX, 0.0f);
		Vector3 rotationVer = new Vector3 (mouseY, 0.0f, 0.0f);

		transform.Translate (direction*moveSpeed*Time.deltaTime);
		transform.Rotate (rotationHor * rotationSpeed * Time.deltaTime);
		Vector3 futRotation = (-rotationVer * rotationSpeed * Time.deltaTime);

//		if (playerCam.transform.eulerAngles.x + futRotation.x <= 80) {
//			if (playerCam.transform.eulerAngles.x + futRotation.x >= -80) {
				playerCam.transform.Rotate (futRotation);
//			}
//
//		}

	}
}
