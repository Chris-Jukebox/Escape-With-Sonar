using UnityEngine;
using System.Collections;

public class BonusPoint : MonoBehaviour {

    public GameObject bonusSparkle;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Hands")
        {
            Instantiate(bonusSparkle, transform.position, Quaternion.identity);
            GameManager.instance.GetBonus();
        }
    }
}
