using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraMovement : MonoBehaviour {


	void Update () {
		float movement = Input.GetAxis ("Mouse ScrollWheel") * 20;

		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (Vector3.up * 55 * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.right * -35 * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (Vector3.up * -55 * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector3.right * 35 * Time.deltaTime);
		}
		if (transform.position.y < 3) {
			transform.position = new Vector3 (transform.position.x, 3, transform.position.z);
		}
		if (transform.position.y > 220) {
			transform.position = new Vector3 (transform.position.x, 220, transform.position.z);
		}
		if (transform.position.z > 65) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, 65);
		}
		if (transform.position.z < -130) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, -130);
		}
		transform.Translate (Vector3.forward * movement * 100 * Time.deltaTime);
	}
}
