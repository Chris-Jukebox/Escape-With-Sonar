using UnityEngine;
using System.Collections;

public class Constant : MonoBehaviour {

    // Monster: positions
	public static float WAVE_HEIGHT = 0.07f;
	public static float MONSTER_HEIGHT = 0.1f;
	public static float OFFSET = 5F;

    // Global constants
    public static string SCENE_PALACE = "MindPalace";
    // Layers
    public static int[] LAYER_OBSTACLES = new int[] {8, 11, 15};
    public static int LAYER_MONSTER = 10;
    public static int LAYER_PLAYER = 13;
    public static int LAYER_CONTROLLER = 14;
    public static int LAYER_OBJECTS = 15;
    public static int LAYER_BLOOD = 16;

    // Player: sonar setting when walking
    /// <summary> The minimum distance of walking to activate the smallest sonar </summary>
    public static float WALK_MIN_DIST = 0.005f;
    /// <summary> The distance of walking to activate the biggest sonar </summary>
    public static float WALK_MAX_DIST = 0.1f;
    /// <summary> The minimum interval between walking sonars </summary>
    public static float WALK_INTERVAL = 0.5f;
}
