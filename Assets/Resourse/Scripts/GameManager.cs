using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool playerFollowVive = true;

	public GameObject player;

	public static GameManager instance;

    public int renderCount = 0;

    public GameObject portal;




	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	public Player GetPlayer() {
		return player.GetComponent<Player> ();
	}

	public void LoadScene()
    {
		SceneManager.LoadScene(Constant.SCENE_PALACE);
    }




    #region MONSTER
    public GameObject[] monsterList;
    private int monsterIndex = 0;
    public void ActivateNextMonster()
    {
        monsterList[monsterIndex++].SetActive(true);
    }

    /// <summary>
    /// Get the vector of player movement in last frame
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPlayerMove()
    {
        return GetPlayer().DeltaMove();
        
    }
    #endregion




    #region ORBS
    // bonus collection
    public int bonusCount = 0;    
    public void GetBonus()
    {
        bonusCount--;
        if (bonusCount <= 0)
            StageManager.instance.NextStage();
    }
    #endregion





    #region PORTAL
    /// <summary>
    /// Create a portal.
    /// </summary>
    /// <param name="pos">position of the portal</param>
    public void ShowPortal(Vector3 pos)
    {
        Instantiate(portal, pos, Quaternion.identity);
        WaveGenerator.instance.SoundWave(pos, 1);
    }
    #endregion

	public void LevelComplete(){
		Invoke ("LoadScene", 1.5f);
	}
	public Camera mainCamera;
	public void GameOver() {
		mainCamera.cullingMask = 1 << Constant.LAYER_BLOOD;
		Invoke ("LoadScene", 4f);
	}
}
