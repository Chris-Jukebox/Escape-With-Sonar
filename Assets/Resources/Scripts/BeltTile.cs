using UnityEngine;
using System.Collections;

public class BeltTile : MonoBehaviour
{
	private ConveyorBelt belt;

	public Vector3 moveVector = Vector3.zero;

	private float wrapDistance = 0.01f;
	// Use this for initialization
	void Start ()
	{
		belt = this.transform.parent.gameObject.GetComponent<ConveyorBelt> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "BeltEnd" && belt) {
			if (Vector3.Distance (other.transform.position, this.transform.position) < wrapDistance) {
				belt.WrapAround (this.gameObject);
			}
		}
	}

}
