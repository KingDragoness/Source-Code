using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	public void Story() {
		Application.LoadLevel(1);
	}

	public void Endless() {
		Application.LoadLevel(1);
	}

	public void Exit() {
		Application.Quit ();
	}
}
