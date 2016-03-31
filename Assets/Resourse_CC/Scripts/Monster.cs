using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
	private Vector3 targetPos;
	private ParticleSystem particle;
	private bool awake = false;

	private static float SPEED = 0.05f;

	// Use this for initialization
	void Start () {
		particle = GetComponent<ParticleSystem> ();
		particle.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (awake) {
			Vector3 dir = Vector3.Normalize(targetPos - transform.position);
			Vector3 step = dir * SPEED * Time.deltaTime;
			if (Vector3.Distance(transform.position, targetPos) < step.magnitude) {
				SetAwake (false);
			}
			transform.position += step;
		}
	}

	public void Chase () {
		targetPos = GameManager.instance.player.gameObject.transform.position;
		SetAwake (true);
	}

	void SetAwake (bool isAwake) {
		particle.enableEmission = isAwake;
		awake = isAwake;
	}

	void OnCollide (Collider col) {
		Chase ();
	}
}
