using UnityEngine;
using System.Collections;

public class WaveGenerator : MonoBehaviour {
	public GameObject soundWave;
	public GameObject bloodWave;
	public GameObject bonusSpark;

    public GameObject monsterWaveElement;
    public int sonarDensity = 50;
	public static WaveGenerator instance;

	void Start() {
		instance = this;
	}

    // WaveManager: wave settings
    private static float MAX_SPEED = 0.18f;
    private static float MIN_SPEED = 0.05f;
    private static float MAX_ALPHA = 1f;
    private static float MIN_ALPHA = 0.2f;

    private static float HURT_SPEED = 0.3f;
    private static float DEAD_SPEED = 0.8f;

    /// <summary> 
    /// Normal sonar: used for player walking. 
    /// </summary>
	public void SoundWave (Vector3 pos, float value = 1) {

        float speed = 0.18f;
		float alpha = Mathf.Lerp ( 0.2f, 1f, value );
        float life  = Mathf.Lerp ( 1f, 2f, value );
        int density = (int) Mathf.Lerp(10000, 10000, value);

        pos.y = 0.1f;

        ReleaseSonar(pos, life, speed, alpha, density);
        MonsterWave(pos, life-0.3f, speed);
    }

    /// <summary> 
    /// Slight sonar: used when player's controllers touched any objects 
    /// </summary>
    public void SlightSonar(Vector3 pos, float value = 1)
    {
        float speed = Mathf.Lerp ( 0.05f, 0.18f, value );
        float alpha = Mathf.Lerp ( 0.06f, 1.00f, value );
        float life  = Mathf.Lerp ( 0.40f, 0.80f, value );
        int density = (int) Mathf.Lerp(500, 10000, value);
        
        ReleaseSonar(pos, life, speed, alpha, density);
        MonsterWave(pos, life, speed);
    }

    /// <summary> 
    /// Template sonar 
    /// </summary>
    private void ReleaseSonar(Vector3 pos, float life, float speed, float alpha, int density)
    {
        GameObject wave = (GameObject)Instantiate(soundWave, pos, Quaternion.identity);

        ParticleSystem mainSonar = wave.GetComponent<ParticleSystem>(); //.startSpeed = speed;
        ParticleSystem mainSubemitter = wave.transform.GetChild(0).GetComponent<ParticleSystem>();
        ParticleSystem bonusSonar = wave.transform.GetChild(1).GetComponent<ParticleSystem>();
        ParticleSystem bonusSubemitter = bonusSonar.transform.GetChild(0).GetComponent<ParticleSystem>();

        mainSonar.startSpeed = speed;
        mainSonar.startLifetime = life;
        mainSonar.maxParticles = density;
        bonusSonar.startSpeed = speed;
        bonusSonar.startLifetime = life;
        bonusSonar.maxParticles = density;

        mainSubemitter.startColor = new Color(1, 1, 1, alpha);
        bonusSubemitter.startColor = new Color(0, 0.25f, 1, alpha);
    }

    /// <summary> 
    /// Blood wave. Used when caught by the monster. 
    /// </summary>
    public void BloodWave (Vector3 pos, int opt) {
		GameObject wave = (GameObject) Instantiate (bloodWave, pos, Quaternion.identity);
		wave.GetComponent<ParticleSystem> ().startSpeed = opt == 0 ? HURT_SPEED : DEAD_SPEED;
	}

    /// <summary> 
    /// Bonus spark
    /// </summary>
	public void BonusSpark (Vector3 pos) {
		Instantiate (bonusSpark, pos, Quaternion.identity);
	}
    
    
    /// <summary>
    /// Wave that collides with the monster only
    /// </summary>
    private void MonsterWave(Vector3 pos, float life, float speed)
    {
        for (int i=0; i<500; i++)
        {
            GameObject obj = (GameObject)Instantiate(
                monsterWaveElement, 
                pos, 
                new Quaternion(
                    Random.Range(-1f, 1f), 
                    Random.Range(-1f, 1f), 
                    Random.Range(-1f, 1f), 
                    Random.Range(-1f, 1f)
                ));
            obj.GetComponent<MonsterSonar>().Set(speed, life, pos);
        }
    }
}