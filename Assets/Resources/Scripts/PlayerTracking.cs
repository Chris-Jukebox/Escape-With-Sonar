using UnityEngine;
using System.Collections;

public class PlayerTracking : MonoBehaviour {

	public bool onBelt = false; 

	public bool reachedEnd = true; 

	private TrackingSpaceMovement trackingSpace;

	private bool falling = false;

	// Use this for initialization
	void Start () {
		trackingSpace = this.transform.parent.parent.GetComponent<TrackingSpaceMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (IsFalling()) {
			trackingSpace.StartPlayerFall(3f);
			Invoke ("ResetVariables", 2f);
		}
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


	void ResetVariables() {
		reachedEnd = true;
		onBelt = false;
	}

	private bool IsFalling(){
		RaycastHit hit; 
		if (Physics.Raycast (this.transform.position, Vector3.down, out hit, 4f)) {
			return false;
		} else
			return true;
	}
}
