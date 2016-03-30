using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

	public string sceneName; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GoToScene() {
		SceneManager.LoadScene (sceneName);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "MainCamera") {
			GoToScene ();
		}
	}
}
