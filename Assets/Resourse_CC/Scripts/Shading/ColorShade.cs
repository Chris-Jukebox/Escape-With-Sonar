using UnityEngine;
using System.Collections;

public class ColorShade : MonoBehaviour {

	private static int POINT_SIZE = 64;		// the size of new point
	private static float INTERVAL = 0.04f;	// update interval
	private static float FADE_TIME = 2;		// time to fade from white to black

	// brush
	public Texture2D brush;
	private float[,] brushData;
	private int bW, bH;

	public int size = 512;
	private int length;
	private Texture2D tex;
	private float[,] texData;
	private float[,] data;
	
	void Start () {
		// create material
		Material mat = GetComponent<Renderer> ().material;
		GetComponent<Renderer> ().material.CopyPropertiesFromMaterial (mat);

		length = size * size;

		// create alpha texture
		tex = new Texture2D (size, size);
		Color[] colors = new Color[length];
		for (int i = 0; i < length; i++)
			colors [i] = Color.black;
		tex.SetPixels (colors);
		tex.Apply ();

		// apply texture
		GetComponent<Renderer> ().material.mainTexture = tex;

		// init data
		data = new float[size, size];
		texData = new float[size, size];
		for (int i = 0; i < size; i++) {
			for (int j=0; j<size; j++) {
				data [i,j] = 0;
				texData [i,j] = 0;
			}
		}

		// init brush
		bW = brush.width;
		bH = brush.height;
		brushData = new float[bH, bW];
		colors = brush.GetPixels();
		for (int i=0; i<bH; i++) {
			for (int j=0; j<bW; j++) {
				brushData [i, j] = colors[i*bW + j].r;
			}
		}

		// start color updater
		StartCoroutine (UpdateColor ());
	}


	void Update () {
		if (Input.GetKeyDown (KeyCode.K)) {
			int x = Random.Range(0, size - 1);
			int y = Random.Range(0, size - 1);
			AddColorPoint(x, y);
		}
	}

    void OnParticleCollision(GameObject other) {
        
    }


	void AddColorPoint(int x, int y) {
		int y1 = y - bH / 2;
		int x1 = x - bW / 2;
		for (int i = y1>=0 ? y1 : 0; i < size && i < y1+bH; i++) {
			for (int j = x1>=0 ? x1 : 0; j < size && j < x1+bW; j++) {
				data[i, j] += brushData[i-y1, j-x1];
			}
		}
	}

	IEnumerator UpdateColor() {

		while (true) {

			tex.Apply ();

			for (int i = 2; i < size-2; i++) {
				for (int j = 2; j < size-2; j++) {
					if (data [i, j] > 0) {

						data [i, j] -= INTERVAL / FADE_TIME;
						if (data [i, j] < 0)
							data [i, j] = 0;
						tex.SetPixel (i, j, new Color (data [i, j], data [i, j], data [i, j], 1));
					}
				}
			}

			yield return new WaitForSeconds (INTERVAL);
		}
	}
}
