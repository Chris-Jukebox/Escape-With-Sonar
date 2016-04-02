using UnityEngine;
using System.Collections;

public class Constant : MonoBehaviour {
	// wave settings
	public static float MAX_SPEED = 0.18f;
	public static float MIN_SPEED = 0.05f;
	public static float MAX_ALPHA = 1f;
	public static float MIN_ALPHA = 0.2f;
	
	public static float HURT_SPEED = 0.3f;
	public static float DEAD_SPEED = 0.8f;

	// positions
	public static float WAVE_HEIGHT = 0.07f;
	public static float MONSTER_HEIGHT = 0.1f;

	// controller
	public static float MAX_HOLDING = 1f;
	public static float MIN_HOLDING = 0.2f;

    // scenes
    public static string SCENE_PALACE = "MindPalace";
}
