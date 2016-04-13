using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameObject playerAnchor;
    

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update() {
        if (GameManager.instance.playerFollowVive)
		    transform.position = playerAnchor.transform.position;
        UpdateWalkSonar();
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
		if (col.gameObject.tag == "Monster") 
			Die ();
		//else if (col.gameObject.tag == "Obstacle")
			// GetHurt ();
		else if (col.gameObject.tag == "Bonus") {
			// WaveGenerator.instance.BonusSpark (col.transform.position);
			// GetBonus ();
		}
	}


    #region WALK_SONAR

    private Vector3 lastPosition;
    private float timer = 0;

    void UpdateWalkSonar()
    {
        // check distance
        // when you walk out of the threshold we release your sonar ~\(≧▽≦)/~
        Vector3 dist = transform.position - lastPosition;
        dist.y = 0;
        if( dist.magnitude > Constant.WALK_MIN_DIST && timer <= 0)
        {
            float min = Constant.WALK_MIN_DIST, max = Constant.WALK_MAX_DIST;
            float value = (dist.magnitude - min) / (max - min);
            if (value > 1)
                value = 1;
            WaveGenerator.instance.SoundWave(transform.position, value);
            lastPosition = transform.position;
            timer = Constant.WALK_INTERVAL;
        }

        // interval
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
                timer = 0;
        }
    }

    #endregion
}
