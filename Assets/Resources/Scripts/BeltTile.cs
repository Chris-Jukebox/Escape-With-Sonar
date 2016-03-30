using UnityEngine;
using System.Collections;

public class BeltTile : MonoBehaviour
{
	private ConveyorBelt belt;

	public Vector3 moveVector = Vector3.zero;

	// Use this for initialization
	void Start ()
	{
		belt = this.transform.parent.gameObject.GetComponent<ConveyorBelt> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
		

	void OnTriggerExit(Collider other) {
		if (other.tag == "BeltEnd" && belt) {
				belt.WrapAround (this.gameObject);
		}
	}

}
