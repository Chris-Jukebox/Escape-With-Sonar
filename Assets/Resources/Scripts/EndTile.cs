using UnityEngine;
using System.Collections;

public class EndTile : MonoBehaviour {

	private ConveyorBelt belt;

	public Transform platformCenter; 

	// Use this for initialization
	void Start ()
	{
		belt = this.transform.parent.gameObject.GetComponent<ConveyorBelt> ();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter (Collider other)
	{
		
	}



	void OnTriggerExit (Collider other) {

	}
}
