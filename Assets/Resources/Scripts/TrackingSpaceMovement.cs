using UnityEngine;
using System.Collections;

public class TrackingSpaceMovement : MonoBehaviour {
	public Transform player;
	private PlayerTracking playerTracking; 

	private GameObject curTile; 

	// Use this for initialization
	void Start () {
		playerTracking = player.gameObject.GetComponent<PlayerTracking> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (move && frameCount < 100) {
			this.transform.Translate (moveDir);
			frameCount++;
		} else {
			frameCount = 0;
			move = false;
		}
	}


	private bool move = false;
	private Vector3 moveDir; 
	private int frameCount = 0;

	public void MovePastBelt(Vector3 dir) {
		
		move = true;
		moveDir = dir;
	}





}
