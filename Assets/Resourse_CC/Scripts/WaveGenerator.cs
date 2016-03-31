using UnityEngine;
using System.Collections;

public class WaveGenerator : MonoBehaviour {
	public GameObject soundWave;
	public GameObject bloodWave;
	public GameObject bonusSpark;

    public GameObject soundElement;

	public static WaveGenerator instance;

	void Start() {
		instance = this;
	}

    
	public void SoundWave (Vector3 pos, float value = 1) {
		pos.y = Constant.WAVE_HEIGHT;
		float speed = (Constant.MAX_SPEED - Constant.MIN_SPEED) * value + Constant.MIN_SPEED;
		float alpha = (Constant.MAX_ALPHA - Constant.MIN_ALPHA) * value + Constant.MIN_ALPHA;
		GameObject wave = (GameObject) Instantiate (soundWave, pos, Quaternion.identity);
		wave.GetComponent<ParticleSystem> ().startSpeed = speed;
		wave.transform.GetChild (0).GetComponent<ParticleSystem> ().startColor = new Color (1, 1, 1, alpha);
		wave.transform.GetChild (1).GetComponent<ParticleSystem> ().startSpeed = speed;
		wave.transform.GetChild (1).GetChild (0).GetComponent<ParticleSystem> ().startColor = new Color (0, 0.25f, 1, alpha);
		wave.transform.GetChild (2).GetComponent<ParticleSystem> ().startSpeed = speed;

        SoundElements(pos, value);
	}

	/** opt: 0-hurt 1-die */
	public void BloodWave (Vector3 pos, int opt) {
		GameObject wave = (GameObject) Instantiate (bloodWave, pos, Quaternion.identity);
		wave.GetComponent<ParticleSystem> ().startSpeed = opt == 0 ? Constant.HURT_SPEED : Constant.DEAD_SPEED;
	}

	public void BonusSpark (Vector3 pos) {
		Instantiate (bonusSpark, pos, Quaternion.identity);
	}

    public void SoundElements(Vector3 pos, float falue = 1)
    {
        for(int i=0; i<200; i++)
        {
            GameObject sonarElem = (GameObject)Instantiate(
                soundElement, 
                pos, 
                new Quaternion(
                    Random.value * 2 - 1, 
                    Random.value * 2 - 1, 
                    Random.value * 2 - 1, 
                    Random.value * 2 - 1
                ));
        }
    }
}