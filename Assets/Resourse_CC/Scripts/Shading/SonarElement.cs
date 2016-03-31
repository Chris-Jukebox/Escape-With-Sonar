using UnityEngine;
using System.Collections;

public class SonarElement : MonoBehaviour {

    private static float SPEED = 0.12f;
    private static int MASK = (1 << 8);

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
        // move forward
        transform.position = transform.position + transform.forward * SPEED * Time.deltaTime;
	}

    void OnCollisionEnter(Collision col)
    {
        // collide with obstacles
        if (col.gameObject.layer == 8)
        {
            // get texture coord
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, transform.forward, out hit))
                return;

            // texture
        }
    }
}
