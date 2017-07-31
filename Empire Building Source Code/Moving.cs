using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

	bool SearchingforRoom = false;
	bool SuccessfulManipulation = false;
	public bool CrazyMode = false;
	public GameObject deadcitizen;
	public GameObject alivecitizen;
	public GameObject axe;
	Vector3 RoomPos;
	Room targetRoom;
	float RoomInterest = 0;

	public float TimeStop = 6f;
	float Cooldown = 6f;

	void Start() {
		Cooldown = TimeStop;
		rb = GetComponent<Rigidbody> ();

		RoomInterest = Random.value;
		BuildingCenterCommand bcc = FindObjectOfType<BuildingCenterCommand> ();
		if (bcc.therooms.Count > 0 && RoomInterest > 0.5f)
		{
			targetRoom = bcc.therooms [Random.Range (0, bcc.therooms.Count)];
			RoomPos = targetRoom.gameObject.transform.position;
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag ("enterace")) {
			BuildingCenterCommand bcc = FindObjectOfType<BuildingCenterCommand> ();

			if (bcc.therooms.Count > 0 && RoomInterest > 0.5f) {
				targetRoom = bcc.therooms [Random.Range (0, bcc.therooms.Count)];
				if (targetRoom.PopulationRoom <= targetRoom.MaximumPopulation) {
					RoomPos = new Vector3 (targetRoom.gameObject.transform.position.x + Random.Range (-6f, 6f), targetRoom.gameObject.transform.position.y + 1.5f, targetRoom.gameObject.transform.position.z);
					SearchingforRoom = true;
					targetRoom.PopulationRoom++;
					bcc.Population++;
				}
			}
		}

		if (col.gameObject.CompareTag ("citizen") && !CrazyMode) {
			Moving cit = col.gameObject.GetComponent<Moving> ();
			if (cit.CrazyMode) {
				deadcitizen.SetActive (true);
				alivecitizen.SetActive (false);
				this.enabled = false;
			}
		}
	}
		

	bool Done = false;
	bool DoneSearching = false;
	Vector3 citizenPos;
	Moving citizen;

	public void RiotMode() {
		if (SuccessfulManipulation) {
			CrazyMode = true;
		}
	}

	void Update () {
		if (Cooldown < 0 && !SearchingforRoom) {
			Destroy (this.gameObject);
			Cooldown = TimeStop;
		}
		if (!CrazyMode) {
			if (!SearchingforRoom && !Done) {
				Cooldown -= Time.deltaTime;
				transform.Translate (Vector3.forward * 15 * Time.deltaTime);
			} else {
				float xdistance = transform.position.x - RoomPos.x;

				if (transform.position.z < RoomPos.z) {
					transform.Translate (Vector3.right * 15 * Time.deltaTime);
				} else if (transform.position.y < RoomPos.y) {
					transform.Translate (Vector3.up * 15 * Time.deltaTime);
				} else if (xdistance > 0.5f) {
					transform.Translate (Vector3.forward * 15 * Time.deltaTime);
				} else if (xdistance < -0.5f) {
					transform.Translate (Vector3.forward * -15 * Time.deltaTime);
				} else {
					SuccessfulManipulation = true;
					Done = true;
				}
			}
		}

		if (CrazyMode) {
			rb.constraints = RigidbodyConstraints.None;
			axe.SetActive (true);
			if (citizen == null) {
				DoneSearching = false;

			}
			if (!DoneSearching) {
				if (FindObjectOfType<Moving> () != null) {
					Moving[] citizenss = FindObjectsOfType<Moving> ();
					citizen = citizenss [Random.Range (0, citizenss.Length)];
				}

				DoneSearching = true;
			}

			citizenPos = citizen.gameObject.transform.position;
			transform.LookAt (citizenPos);
			transform.Translate (Vector3.forward * 20 * Time.deltaTime);
		}
	}
}
