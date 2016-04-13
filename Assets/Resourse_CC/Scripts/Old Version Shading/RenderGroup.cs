using UnityEngine;
using System.Collections;

public class RenderGroup : MonoBehaviour {
    public GameObject particle;

    private static float SIZE_MIN = 0.1f, SIZE_MAX = 0.3f, COLOR_MAX = 1f, COLOR_MIN = 0.6f;

    private float x0, x1, y0, y1, z;
    public RenderTexture tex;

    public void Setup()
    {
        Transform TL = this.transform.GetChild(1);
        Transform BR = this.transform.GetChild(2);
        x0 = TL.localPosition.x;
        y1 = TL.localPosition.y;
        x1 = BR.localPosition.x;
        y0 = BR.localPosition.y;
        z = BR.localPosition.z;

        tex = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        tex.Create();
   
        transform.GetChild(0).GetComponent<Camera>().targetTexture = tex;
    }

	public void AddParticle(Vector2 pixelUV, float value)
    {
        float x = pixelUV.x * (x1 - x0) + x0;
        float y = pixelUV.y * (y1 - y0) + y0;
        ParticleSystem pt = ((GameObject)Instantiate(particle, transform.position + new Vector3(x, y, z), Quaternion.identity)).GetComponent<ParticleSystem>();
        pt.startSize = value * (SIZE_MAX - SIZE_MIN) + SIZE_MIN;
        pt.startColor = new Color(1, 1, 1, value * (COLOR_MAX - COLOR_MIN) + COLOR_MIN);
    }
}
