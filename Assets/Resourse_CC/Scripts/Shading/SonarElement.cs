using UnityEngine;
using System.Collections;

public class SonarElement : MonoBehaviour {

    private static float SPEED = 0.12f;
    private static int MASK = (1 << 8) | (1 << 11);
    private float lifetime = 3;

	// Use this for initialization
	void Start () {
	
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
        Debug.Log("collide with layer:" + col.gameObject.layer + " " + col.gameObject);
        // collide with obstacles
        if (col.gameObject.layer == 8 || col.gameObject.layer == 11)
        {
            Debug.Log("collide checked");
            // get texture coord
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, transform.forward, out hit, 1, MASK, QueryTriggerInteraction.UseGlobal))
            {
                Debug.Log("Raycast failed");
                return;
            }

            // texture
            Renderer rend = hit.transform.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                return;
            Vector2 pixelUV = hit.textureCoord;
            col.gameObject.GetComponent<ColorShade>().AddColorPoint(pixelUV);

            Destroy(this.gameObject);
        }
    }
}
