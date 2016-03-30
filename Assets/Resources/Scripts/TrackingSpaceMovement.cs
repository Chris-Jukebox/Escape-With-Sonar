using UnityEngine;
using System.Collections;

public class TrackingSpaceMovement : MonoBehaviour {
	public Transform player;
	private PlayerTracking playerTracking; 

	private Vector3 curCenterPos; 
	private Quaternion curCenterRot;

	// Use this for initialization
	void Start () {
		playerTracking = player.gameObject.GetComponent<PlayerTracking> ();
		curCenterPos = this.transform.position;
		curCenterRot = this.transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log (curCenterPos);
		MoveSpaceForward ();
		PlayerFalling ();
	}


	private bool move = false;
	private Vector3 moveDir; 
	private int frameCount = 0;
	private int maxFrameCount = 90;


	public void MovePastBelt(Vector3 dir) {
		
		move = true;
		moveDir = dir;
	}

	private void MoveSpaceForward() {
		if (move && frameCount < maxFrameCount) {
			this.transform.Translate (moveDir);
			frameCount++;
			if (frameCount == maxFrameCount) {
				// we have reached new center so change curcenter
				curCenterPos = this.transform.position;
				curCenterRot = this.transform.rotation;
			}
		} else {
			frameCount = 0;
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

		Debug.Log ("gets here");
	}


}
