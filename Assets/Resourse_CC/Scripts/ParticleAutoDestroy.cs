using UnityEngine;
using System.Collections;

public class ParticleAutoDestroy : MonoBehaviour {
	ParticleSystem particleSystem;

	void Start () {
		particleSystem = GetComponent<ParticleSystem> ();
	}

	void Update () {
		if (particleSystem != null && particleSystem.isStopped)
			Destroy (this.gameObject);
	}
}
