using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	private float wavePower = 0;
	private bool isPressDown = false;

	void Update() {
		CheckPressButton ();
	}

	void CheckPressButton() {
		int index = SteamVR_Controller.GetDeviceIndex (SteamVR_Controller.DeviceRelation.Rightmost);
		if (SteamVR_Controller.Input (index).GetPress (SteamVR_Controller.ButtonMask.Trigger)) {
			isPressDown = true;
			wavePower += Time.deltaTime;
		}
		if (SteamVR_Controller.Input (index).GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
			isPressDown = false;
			float min = Constant.MIN_HOLDING;
			float range = Constant.MAX_HOLDING - Constant.MIN_HOLDING;
			wavePower = wavePower < min ? 0 : (wavePower > (min + range) ? range : wavePower - min);
			GameManager.instance.GetPlayer().ReleaseWave (wavePower / range);
			wavePower = 0;
		}
	}
}
