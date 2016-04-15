using UnityEngine;
using System.Collections;

public class TrackingSpaceMovement : MonoBehaviour {
	public Transform player;
	private PlayerTracking playerTracking; 

	private Vector3 curCenterPos; 
	private Quaternion curCenterRot;

	public static Vector3 spawnPos; 

	// Use this for initialization
	void Start () {
		playerTracking = player.gameObject.GetComponent<PlayerTracking> ();
		curCenterPos = this.transform.position;
		curCenterRot = this.transform.rotation;
		spawnPos = curCenterPos;
		this.transform.position = spawnPos;
	}
	
	// Update is called once per frame
	void Update () {

		MoveSpaceForward ();
		PlayerFalling ();
	}


	private bool move = false;
	private Vector3 newPos, moveDir = Vector3.zero; 


	public void MovePastBelt(Vector3 curPos) {
		
		move = true;
		newPos = curPos;
		moveDir = newPos - this.transform.position;
	}

	private void MoveSpaceForward() {
		if (move && Vector3.Distance(this.transform.position, newPos) > 0.01f) {
			this.transform.Translate (moveDir*Time.deltaTime);
		
			if (Vector3.Distance(this.transform.position, newPos) <= 0.01f) {
				// we have reached new center so change curcenter
				curCenterPos = this.transform.position;
				curCenterRot = this.transform.rotation;
				spawnPos = curCenterPos;
			}
		} else {
			move = false;

		}
	}

	private bool falling = false;
	public void StartPlayerFall(float respawnTime) {
		this.transform.SetParent (null);
		falling = true;
		Invoke ("RespawnPlayer", respawnTime);

	}
	private void PlayerFalling() {
		if (falling)
			this.transform.Translate (Vector3.down * Time.deltaTime);
	}

	private void RespawnPlayer() {
		falling = false;
		// move tracking space back first
		this.transform.position = curCenterPos;
		this.transform.rotation = curCenterRot;

	}


}
