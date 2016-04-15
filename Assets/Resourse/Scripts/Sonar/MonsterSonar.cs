using UnityEngine;
using System.Collections;

/*
 this is a simulation of particle elements that will only collide with the monster
 used to trigger the monster
 stores the information of position where the sonar is released
*/

public class MonsterSonar : MonoBehaviour {

    private float speed = 0.15f;  // particle moving speed
    private float lifetime = 1;          // remaining lifetime
    private Vector3 sourcePos;

    // init the lifetime of particle (keep it the same with the main sonar)
    public void Set(float speed, float life, Vector3 pos)
    {
        lifetime = life;
        sourcePos = pos;
    }

    void Update()
    {
        // move forward
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;

        // update lifetime
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
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
