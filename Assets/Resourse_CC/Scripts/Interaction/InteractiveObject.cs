using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour {

    private Rigidbody rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        mat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        UpdateCollideTimer();
	}




    #region Color_Change

    private Material mat;
    private static float COLOR_ALPHA = 160f / 255f;
    private static float EMISSION = 1f;
    private static float GLOW_DURATION = 0.6f;
    private float glowValue = 0;
    private bool isGlow = false;
    private int shineTimes = 0;

    public void SetGlow(bool activateGlow) {
        if (!isGlow && activateGlow) {
            isGlow = true;
            StopCoroutine("StopGlow");
            StartCoroutine("StartGlow");
        }
        else if (isGlow && !activateGlow)
        {
            isGlow = false;
            StopCoroutine("StartGlow");
            StartCoroutine("StopGlow");
        }
    }

    public void SetShine(int time)
    {
        shineTimes += time;
        if (shineTimes <= time)
            StartCoroutine(Shine());
    }

    private void SetMaterial()
    {
        mat.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, COLOR_ALPHA), glowValue);
        // mat.SetColor("_EmissionColor", Color.Lerp(new Color(0, 0, 0), new Color(EMISSION, EMISSION, EMISSION), glowValue));
    }

    private IEnumerator StartGlow()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / GLOW_DURATION;
            glowValue = t;
            SetMaterial();
            yield return new WaitForEndOfFrame();
        }
        glowValue = 1;
        SetMaterial();
    }

    private IEnumerator StopGlow()
    {
        float t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime / GLOW_DURATION;
            glowValue = t;
            SetMaterial();
            yield return new WaitForEndOfFrame();
        }
        glowValue = 0;
        SetMaterial();
    }

    private IEnumerator Shine()
    {
        if (shineTimes > 0)
        {
            StartCoroutine("StartGlow");
            yield return new WaitForSeconds(0.8f);
            StartCoroutine("StopGlow");
            yield return new WaitForSeconds(0.8f);
            shineTimes -= 1;
            StartCoroutine(Shine());
        }
    }
    #endregion


    #region Controller_Interactions

    private Vector3 lastPos;
    private Vector3 deltaMove;

    public bool PickedUpBy(GameObject parent)
    {
        if (transform.parent != null)
            return false;
        rigid.isKinematic = true;
        transform.parent = parent.transform;
        lastPos = transform.position;
        StartCoroutine("recordPath");
        return true;
    }

    IEnumerator recordPath ()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            deltaMove = (transform.position - lastPos)/0.1f;
            lastPos = transform.position;
        }
    }

    public void Released ()
    {
        transform.parent = null;
        StopCoroutine("recordPath");
        rigid.isKinematic = false;
        rigid.velocity = deltaMove * 2 / 0.3f;
    }

    #endregion


    #region Collision
    
    // Collission with normal obstacles
    private static float COLLIDE_INTERVAL = 0.5f;
    private float timer = 0;

    void UpdateCollideTimer()
    {
        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            if (timer < 0)
                timer = 0;
        }
    }

    // when collide with other objects, release a slight sound
    void OnCollisionEnter(Collision col)
    {
        foreach (int layer in Constant.LAYER_OBSTACLES)
        {
            if (col.gameObject.layer == layer)
            {
                if (timer <= 0)
                {
                    WaveGenerator.instance.SoundWave(transform.position, 0.5f);
                    timer = COLLIDE_INTERVAL;
                    SetShine(1);
                }
                return;
            }
        }
    }
    #endregion
}
