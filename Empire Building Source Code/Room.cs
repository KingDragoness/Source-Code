using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	public bool isEnabledElectricity = false;
	public int Level = 0;
	public GameObject lightbulb;
	public TextMesh tm;
	public GameObject LV1;
	public GameObject LV2;
	public GameObject LV3;
	public BuildingCenterCommand bcc;
	public int PopulationRoom = 1;
	public int MaximumPopulation = 5;
	float UpgradingTime = 10f;

	void Start() {
		bcc = FindObjectOfType<BuildingCenterCommand> ();
	}


	void FixedUpdate() {
		if (isEnabledElectricity) {
			lightbulb.SetActive (true);
			tm.text = Level.ToString();
			UpgradingTime -= Time.deltaTime;
		} else {
			lightbulb.SetActive (false);
			//UpgradingTime = 10f;
		}

		if (UpgradingTime < 0) {
				Level++;
			if (Level == 1) {
				UpgradeLV1 ();
			}
			if (Level == 2) {
				UpgradeLV2 ();
			}
			if (Level >= 3 && bcc.AllowLevel3) {
				UpgradeLV3 ();
			} else if (Level > 2) {
				Level = 2;
			}
			UpgradingTime = 10f;
		}
	}

	void UpgradeLV1() {
		bcc.Money += 100;
		LV1.SetActive (true);
	}

	void UpgradeLV2() {
		bcc.Money += 250;

		LV1.SetActive (false);
		LV2.SetActive (true);

	}

	void UpgradeLV3() {
		bcc.Money += 500;

		LV2.SetActive (false);
		LV3.SetActive (true);

	}

}
