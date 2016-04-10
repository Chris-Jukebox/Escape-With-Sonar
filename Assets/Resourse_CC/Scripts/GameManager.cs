using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject player;

	public static GameManager instance;

	private float wavePower = 0;

    public int renderCount = 0;

    public GameObject portal;

	// Use this for initialization
	void Awake () {
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
	}

	public Player GetPlayer() {
		return player.GetComponent<Player> ();
	}

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // bonus collection
    public int bonusCount = 0;
    public void GetBonus()
    {
        bonusCount--;
        if (bonusCount <= 0)
            Instantiate(portal, new Vector3(-0.02368622f, 0.1426f, -0.7489983f), Quaternion.identity);
    }
}
