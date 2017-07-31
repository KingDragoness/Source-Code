using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildingCenterCommand : MonoBehaviour {

	public int Floor = 32;
	public int MaximumElectricityLevel = 1;
	public int Population = 0;
	public GameObject[] civilSpawner;
	public GameObject messageSuccess;
	public Text powerusageconsumption;
	public Text Populationtext;
	public Text totalmoney;
	public Text insufficient;

	public float Money = 0;
	public float PowerUsage = 0;
	public float MaximumPowerUsage = 0;
	public bool AllowLevel3 = false;
	public bool MonsterMode = false;

	public int RoomsEachFloor = 6;
	public GameObject brilliantInterface;
	public GameObject FloorObject;
	int amountofElectricityEnabled = 0;
	float cooldownScan = 1.1f;
	bool Riot = false;
	public List<Room> therooms = new List<Room>();
	List<Room> theroomsLV3 = new List<Room>();
	Rigidbody rb;


	Room[] allrooms;

	void Awake () {
		rb = GetComponent<Rigidbody> ();
		SpawningRooms ();
		allrooms = FindObjectsOfType<Room> ();
	}

	public void ElectricityPowerUpgrade() {
		if (Money >= 10000) {
			MaximumElectricityLevel += 1;
			Money -= 10000;

		} else {
			insufficient.gameObject.SetActive (true);
		}
	}

	public void Upgrade() {
		if (Money >= 100000) {
			AllowLevel3 = true;
			Money -= 100000;
			brilliantInterface.gameObject.SetActive (true);

		} else {
			insufficient.gameObject.SetActive (true);
		}
	}
	
	void SpawningRooms () {
		for (int i = 0; i < Floor; i++) {
			for (int x = 0; x < RoomsEachFloor; x++) {
				Vector3 theSpawn = new Vector3 (48.6f - (x * 13.84f), i * 7, 48.6f);
				GameObject floorApartment = Instantiate (FloorObject, theSpawn, FloorObject.transform.rotation);
				floorApartment.transform.parent = this.transform;
			}
		}
	}

	public GameObject MonsterComponent;

	void Update(){
		if (Input.GetMouseButtonDown(0)){ 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if (hit.collider.CompareTag ("room")) {
					Room room = hit.collider.gameObject.GetComponent<Room> ();
					room.isEnabledElectricity = !room.isEnabledElectricity;
				}
			}
		}

		if (MaximumPowerUsage > 1000000) {
			MonsterMode = true;
		}

		if (MonsterMode) {
			messageSuccess.SetActive (true);
			MonsterComponent.SetActive (true);

		}

	}

	void FixedUpdate() {
		cooldownScan -= Time.deltaTime;
		if (cooldownScan < 0) {
			CheckRoomhasElectricity ();
			cooldownScan = 1.1f;
		}
		if (Riot) {
			foreach (GameObject go in civilSpawner) {
				go.SetActive (false);
			}
		}
	}

	void CheckRoomhasElectricity() {

		if (PowerUsage > MaximumPowerUsage) {
			foreach (Room rm in allrooms) {
				rm.isEnabledElectricity = false;
				therooms.Clear ();
			}
		}

		if (MaximumPowerUsage > 1000000) {
			Moving[] monsters = FindObjectsOfType<Moving> ();
			foreach (Moving mts in monsters) {
				mts.RiotMode ();
			}
		}

		foreach (Room rm in allrooms) {
			if (rm.isEnabledElectricity && !therooms.Contains (rm)) {

				therooms.Add (rm);
			} else if (!rm.isEnabledElectricity) {
				therooms.Remove (rm);
			}
			if (rm.Level > 0 && rm.Level < 3 && !therooms.Contains (rm) && rm.isEnabledElectricity) {
				therooms.Add (rm);

			} else if (rm.Level > 0 && rm.Level < 3 && !therooms.Contains (rm) && !rm.isEnabledElectricity) {
				therooms.Remove (rm);

			} else if (rm.Level == 3 && !theroomsLV3.Contains (rm)) {
				therooms.Remove (rm);
				theroomsLV3.Add (rm);
			}
		}

		Money += 40 * (float)therooms.Count;
		PowerUsage = (float)therooms.Count * 200;
		MaximumPowerUsage = (float)theroomsLV3.Count * 2000 * MaximumElectricityLevel;
		totalmoney.text = "$" + Money.ToString ();
		powerusageconsumption.text = "Power Usage: " + PowerUsage + "/" + MaximumPowerUsage + " mHZ";
		Populationtext.text = "Population: " + Population.ToString ();

		//print (therooms.Count);

	}
}
