using UnityEngine;
using System.Collections;

public class PlayerTracking : MonoBehaviour {

	public bool onBelt = false; 

	private TrackingSpaceMovement trackingSpace;
	// Use this for initialization
	void Start () {
		trackingSpace = this.transform.parent.parent.GetComponent<TrackingSpaceMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private bool reachedEnd = false; 
	void OnTriggerEnter(Collider other) {
		if (other.tag == "BeltTile" && !reachedEnd && !onBelt) {
			this.transform.parent.parent.SetParent (other.transform);
			onBelt = true;
		}

		if (other.tag == "BeltEnd") {
			reachedEnd = true;
			onBelt = false;
		}
		if (other.tag == "BeltStart") {
			reachedEnd = false;
		}
	}


	void OnTriggerExit(Collider other) {

	}


}
