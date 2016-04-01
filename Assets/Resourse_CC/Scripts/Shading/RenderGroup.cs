using UnityEngine;
using System.Collections;

public class RenderGroup : MonoBehaviour {
    public GameObject particle;

    private float x0, x1, y0, y1, z;
    public RenderTexture tex;

    public void Setup()
    {
        Transform TL = this.transform.GetChild(1);
        Transform BR = this.transform.GetChild(2);
        x0 = TL.localPosition.x;
        y0 = TL.localPosition.y;
        x1 = BR.localPosition.x;
        y1 = BR.localPosition.y;
        z = BR.localPosition.z;

        tex = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        tex.Create();
   
        transform.GetChild(0).GetComponent<Camera>().targetTexture = tex;
    }

	public void AddParticle(Vector2 pixelUV)
    {
        float x = pixelUV.x * (x1 - x0) + x0;
        float y = pixelUV.y * (y1 - y0) + y0;
        Instantiate(particle, transform.position + new Vector3(x, y, z), Quaternion.identity);
    }
}
