using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject player;

	public static GameManager instance;

	private float wavePower = 0;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	void Update () {
		// CheckPressButton ();
		if (Input.GetKey (KeyCode.S)) {
			wavePower += Time.deltaTime;
		}
		if (Input.GetKeyUp (KeyCode.S)) {
			wavePower = wavePower < 0.5f ? 0 : (wavePower > 2f ? 1.5f : wavePower - 0.5f);
			player.GetComponent<Player>().ReleaseWave (wavePower / 1.5f);
			wavePower = 0;
		}
		// Test Monster.Chase()
		if (Input.GetKeyDown (KeyCode.M)) {
			GameObject.Find("Monster").GetComponent<Monster>().Chase();
		}
	}

	public Player GetPlayer() {
		return player.GetComponent<Player> ();
	}
}
