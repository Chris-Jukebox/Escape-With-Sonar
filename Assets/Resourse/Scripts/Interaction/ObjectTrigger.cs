using UnityEngine;
using System.Collections;

public class ObjectTrigger : MonoBehaviour {

    /// <summary> Records how many controllers are in the trigger</summary>
    int enterCount;

    /// <summary> 
    /// When a controller enters the trigger, set the object glow. 
    /// </summary>
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == Constant.LAYER_CONTROLLER)
        {
            if (enterCount == 0)
                transform.parent.GetComponent<InteractiveObject>().SetGlow(true);
            enterCount++;
            col.gameObject.GetComponent<HandController>().AddObject(this.transform.parent.gameObject);
        }
    }

    /// <summary> 
    /// When exit the trigger, check remaining controller number.
    /// </summary>
    void  OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == Constant.LAYER_CONTROLLER)
        {
            enterCount--;
            if (enterCount == 0)
                transform.parent.GetComponent<InteractiveObject>().SetGlow(false);
            col.gameObject.GetComponent<HandController>().RemoveObject(this.transform.parent.gameObject);
        }
    }
}
