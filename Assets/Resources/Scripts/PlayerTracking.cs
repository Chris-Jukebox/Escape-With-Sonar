using UnityEngine;
using System.Collections;

public class PlayerTracking : MonoBehaviour {

	public bool onBelt = false; 

	public bool reachedEnd = true; 

	private TrackingSpaceMovement trackingSpace;
	private Vector3 originalPos; 
	// Use this for initialization
	void Start () {
		trackingSpace = this.transform.parent.parent.GetComponent<TrackingSpaceMovement> ();
		originalPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "BeltTile" && !reachedEnd && !onBelt) {
			// we have gotten on the start tile and can get on a belt tile
			trackingSpace.transform.SetParent (other.transform);
			onBelt = true;
		}

		if (other.tag == "BeltEnd") {
			// we have reached the end
			// get of the belt
			reachedEnd = true;
			onBelt = false;
		}
		if (other.tag == "BeltStart") {
			// set variable so that we can start getting on belt 
			reachedEnd = false;
		}
	}

	private bool falling = false;

	void OnTriggerExit(Collider other) {
		if (other.tag == "BeltTile" && onBelt && !reachedEnd) {
			// we were on the belt, we haven't hit the end tile and we left the belt tile
			// so we fell off 
			trackingSpace.StartPlayerFall(3f);
			Invoke ("ResetVariables", 2f);
		}
	}

	void ResetVariables() {
		reachedEnd = true;
		onBelt = false;
	}
}
