using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectSpawner : MonoBehaviour {

	public GameObject objecttoSpawn;
	float cooldown = 1f;

	void Start () {
		
	}

	void Update () {
		cooldown -= Time.deltaTime;
		if (cooldown < 0) {
			Instantiate (objecttoSpawn, transform.position, transform.rotation);
			float random = Random.Range (-0.2f , 3f);
			cooldown = 0.2f + random;
		}
	}
}
