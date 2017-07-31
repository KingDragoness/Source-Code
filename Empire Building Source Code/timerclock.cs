using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerclock : MonoBehaviour {

	public float TimeStop = 6f;
	float Cooldown = 6f;

	void Start() {
		Cooldown = TimeStop;
	}

	void Update () {
		Cooldown -= Time.deltaTime;
		if (Cooldown < 0) {
			this.gameObject.SetActive (false);
			Cooldown = TimeStop;
		}
	}
}
