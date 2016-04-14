using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="Player")
        {
            GameManager.instance.LevelComplete();
        }
    }
}
