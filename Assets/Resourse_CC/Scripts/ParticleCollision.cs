using UnityEngine;
using System.Collections;

public class ParticleCollision : MonoBehaviour {

	void OnParticleCollision (GameObject other) {
		if (other.tag == "Monster")
			other.GetComponent<Monster>().Chase();
	}
}
