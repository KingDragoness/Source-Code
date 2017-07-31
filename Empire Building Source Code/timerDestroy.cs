using UnityEngine;
using System.Collections;

public class timerDestroy : MonoBehaviour
{

	public float TimeStop = 6f;
	float Cooldown = 6f;

	void Start() {
		Cooldown = TimeStop;
	}

	void Update () {
		Cooldown -= Time.deltaTime;
		if (Cooldown < 0) {
			Destroy (this.gameObject);
			Cooldown = TimeStop;
		}
	}
}

