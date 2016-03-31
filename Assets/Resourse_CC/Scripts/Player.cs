using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameObject playerAnchor;

	void Update() {
		transform.position = playerAnchor.transform.position;
	}

	public void ReleaseWave(float value) {
		WaveGenerator.instance.SoundWave (transform.position, value);
	}

	public void Die() {
		WaveGenerator.instance.BloodWave (transform.position, 1);
	}

	public void GetHurt() {
		WaveGenerator.instance.BloodWave (transform.position, 0);
	}

	public void GetBonus() {
		Debug.Log ("Player.GetBonus()");
	}

	void OnCollisionEnter (Collision col) {
		Debug.Log (col.gameObject);
		if (col.gameObject.tag == "Monster") 
			Die ();
		else if (col.gameObject.tag == "Obstacle")
			GetHurt ();
		else if (col.gameObject.tag == "Bonus") {
			WaveGenerator.instance.BonusSpark (col.transform.position);
			GetBonus ();
		}
	}
}
