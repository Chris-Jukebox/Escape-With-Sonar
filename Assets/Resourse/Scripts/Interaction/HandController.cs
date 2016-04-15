using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour
{
    public bool isLeft;

    

    void Update()
    {
        UpdateCollideTimer();
        CheckPressButton();
    }


    #region Controller_Events

    void CheckPressButton()
    {
        int index;
        if (isLeft)
            index = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        else
            index = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        if (SteamVR_Controller.Input(index).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            OnPressDown();
        if (SteamVR_Controller.Input(index).GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            OnPressUp();
    }

    void OnPressDown()
    {
        Pick();
    }

    void OnPressUp()
    {
        Release();
    }

    #endregion



    #region Pick_Objects

    private GameObject grabObj;

    public void AddObject(GameObject obj)
    {
        if (grabObj == null)
            grabObj = obj;
    }
    public void RemoveObject(GameObject obj)
    {
        if (grabObj == obj)
            grabObj = null;
    }

    void Pick()
    {
        if (!grabObj)
            return;
        InteractiveObject inte = grabObj.GetComponent<InteractiveObject>();
        if (!inte)
            return;
        inte.PickedUpBy(this.gameObject);
    }

    void Release()
    {
        if (grabObj && grabObj.GetComponent<InteractiveObject>())
        {
            grabObj.GetComponent<InteractiveObject>().Released();
            grabObj = null;
        }
    }

    #endregion

    // Collission with normal obstacles

    private static float COLLIDE_INTERVAL = 0.5f;
    private float timer = 0;

    void UpdateCollideTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
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
                    WaveGenerator.instance.SlightSonar(transform.position, col.impulse.magnitude / 0.3f + 0.1f);
                    timer = COLLIDE_INTERVAL;
                }
                return;
            }
        }
    }
}
