using UnityEngine;
using System.Collections;

public class SonarElement : MonoBehaviour {
    /*
    private static float SPEED = 0.15f;
    private static int MASK = (1 << 8) | (1 << 11);
    private float lifetime = 3;
    private float strength = 1;

	// Use this for initialization
	public void Set (float value) {
        strength = value;
        lifetime = 2 * value + 1;
	}
	
	void Update () {
        // move forward
        transform.position = transform.position + transform.forward * SPEED * Time.deltaTime;

        // lifetime
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
            Destroy(this.gameObject);
	}

    void OnTriggerEnter(Collider col)
    {
        // collide with obstacles
        if (col.gameObject.layer == 8 || col.gameObject.layer == 11)
        {
            // get texture coord
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, transform.forward, out hit, 1, MASK, QueryTriggerInteraction.UseGlobal))
            {
                return;
            }

            // texture
            Renderer rend = hit.transform.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                return;
            Vector2 pixelUV = hit.textureCoord;
            col.gameObject.GetComponent<ColorRender>().AddColorPoint(pixelUV, strength);

            Destroy(this.gameObject);
        }
    }*/
}
