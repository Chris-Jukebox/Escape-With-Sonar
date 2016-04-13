using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour {

    public static StageManager instance;
    public Transform portalAnchor;
    

    void Awake()
    {
        instance = this;
    }


    private enum Stage
    {
        DEFAULT,
        SHOW_PORTAL,
        DROP_BOX,
        SHOW_ELEMENTS,
        END
    }
    private Stage stage = Stage.DEFAULT;

    public void NextStage()
    {
        switch(stage)
        {
            case Stage.DEFAULT: TurnToPortal(); break;
            case Stage.SHOW_PORTAL: TurnToDrop(); break;
            case Stage.DROP_BOX: TurnToElements(); break;
            case Stage.SHOW_ELEMENTS: TurnToEnd(); break;
            default: break;
        }
    }

    private void TurnToPortal()
    {
        stage = Stage.SHOW_PORTAL;
        // activate the monster
        GameManager.instance.ActivateNextMonster();
        // show the portal
        GameManager.instance.ShowPortal(portalAnchor.position);
        // wait until the monster is touched and turn to drop stage
        Invoke("TurnToDrop", 10f);
    }

    public GameObject[] dropBox;

    private void TurnToDrop()
    {
        stage = Stage.DROP_BOX;

        // create a box object
        dropBox[0].SetActive(true);
        dropBox[0].GetComponent<InteractiveObject>().SetShine(5);
        // when the monster move to the box, turn to next stage, or we count numbers
        Invoke("TurnToElements", 4f);
    }

    public GameObject textThrow;
    private void TurnToElements()
    {
        stage = Stage.SHOW_ELEMENTS;
        textThrow.SetActive(true);
        // glow the elements
        dropBox[1].SetActive(true);
        dropBox[2].SetActive(true);
        dropBox[0].GetComponent<InteractiveObject>().SetShine(6);
        dropBox[0].GetComponent<InteractiveObject>().SetShine(6);
        // add text to it
    }

    private void TurnToEnd()
    {
        stage = Stage.END;

        // whatever..
    }
}
