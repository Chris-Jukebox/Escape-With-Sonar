using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class ConveyorBelt : MonoBehaviour
{
	// always rotate this gameobject such that moving forward for tiles is always z direction


	public GameObject tileOne;
	public GameObject tileTwo;

	public GameObject start;
	public GameObject end;


	public int length;
	[Range (1f, 10f)] public float beltSpeed = 1f;
	private GameObject[] tiles;

	private float tileSize = 0f;


	private float slowFactor = 0.001f;
	private Vector3 move;

	// Use this for initialization
	void Start ()
	{
		tileSize = tileOne.transform.localScale.x;
		//tileSize = 0.1f;
		MakeInitialBelt ();
	}

	void MakeInitialBelt ()
	{

		move = this.transform.forward.normalized * slowFactor * beltSpeed; 
		GameObject tile; 
		tiles = new GameObject[length];
		for (int i = 0; i < length; i++) {
			if (i % 2 == 0) {
				tile = GameObject.Instantiate (tileOne, GetIndexPos (i), Quaternion.identity) as GameObject;
			} else {
				tile = GameObject.Instantiate (tileTwo, GetIndexPos (i), Quaternion.identity) as GameObject;
			}
			tile.transform.SetParent (this.transform);
			tiles [i] = tile;

			tile.GetComponent<BeltTile> ().moveVector = move;
		}
	}



	// Update is called once per frame
	void FixedUpdate ()
	{
		MoveBelt ();
	}



	void MoveBelt ()
	{
		foreach (GameObject tile in tiles) {
			tile.transform.Translate (move);
		}
	}


	/*
	 * once the last tile passes wrap around point, it goes back to the front of belt.
	 * */
	public void WrapAround (GameObject tile)
	{

		float val = Mathf.Abs (tile.transform.localPosition.z) - Mathf.Abs (end.transform.localPosition.z);
	
		if (val >= 0) {
			// first check if tracking space attached to it
			// if so, then remove it
			if (tile.transform.childCount > 0) {
				Transform trackingSpace = tile.transform.GetChild (0);
				trackingSpace.SetParent (null);

				// now move the tracking space a bit more past the belt 
			
				trackingSpace.GetComponent<TrackingSpaceMovement> ().MovePastBelt (tile.transform.position);

				// stop belt
				Vector3 temp = move;
				move = Vector3.zero;
				// reverse and start it after 2 seconds. 
				StartCoroutine(ReverseBeltDirection(temp, 2f));
			}

			tile.transform.position = GetIndexPos (0);
		}
	}

	private Vector3 GetIndexPos (int tileIndex)
	{
		Vector3 temp = this.transform.position + this.transform.forward * (tileIndex) * tileSize;
		temp.y -= 0.001f;
		return temp;
	}

	IEnumerator ReverseBeltDirection(Vector3 dir, float waitTime) {
		yield return new WaitForSeconds (waitTime);

	
		Vector3 temp2 = end.transform.position;
		this.transform.Rotate (new Vector3 (0f, 180.0f, 0f));
		this.transform.position = temp2;

		// start moving belt again
		move = dir;
	}
}
