using UnityEngine;
using System.Collections;

/*
 this is a simulation of particle elements that will only collide with the monster
 used to trigger the monster
 stores the information of position where the sonar is released
*/

public class MonsterSonar : MonoBehaviour {

    private static float SPEED = 0.15f;  // particle moving speed
    private float lifetime = 3;          // remaining lifetime
    private Vector3 sourcePos;

    // init the lifetime of particle (keep it the same with the main sonar)
    public void Set(float value, Vector3 pos)
    {
        lifetime = 2 * value + 1;
        sourcePos = pos;
    }

    void Update()
    {
        // move forward
        transform.position = transform.position + transform.forward * SPEED * Time.deltaTime;

        // update lifetime
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
            Destroy(this.gameObject);
    }

    // When collide with other stuff
    void OnTriggerEnter(Collider col)
    {
        // check if collides with obstacles
        foreach (int layer in Constant.LAYER_OBSTACLES)
        {
            if (col.gameObject.layer == layer)
            {
                Destroy(this.gameObject);
                return;
            }
        }
        if (col.gameObject.layer == Constant.LAYER_MONSTER)
        {
            col.gameObject.GetComponent<Monster>().Chase(sourcePos);
            Destroy(this.gameObject);
        }
    }
}
