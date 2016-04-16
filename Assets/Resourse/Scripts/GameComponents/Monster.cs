using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    private ParticleSystem particle;
    private enum State {
        SLEEP,
        AWAKE,
        MOVE,
        SPEED_UP,
        SPEED_DOWN,
        DIE
    };
    // Timer 
    private float timer;  
    // Current Monster State
    private State state = State.SLEEP;
    // monster setting
    private static float WAIT_TO_MOVE = 1.5f;
    private static float PLAY_TIME = 10f;
    private static float TIME_SPEED_UP = 5f;
    private static float TIME_COOL_DOWN = 5f;
    private static float SPEED_ACCEL = 1.2f;
    private static float SPEED_DECAY = 0.8f;
    private static float CHASE_INTERVAL = 1f;
    // move
    private Vector3 speed;
 	private float accel;
 	private int accel_times;

    void Start() {
        particle = GetComponent<ParticleSystem>();
        particle.enableEmission = false;
    }

    void Update() {
        Action();
    }

    // called in Update(), depending on monster's state
    void Action ()
	{
		switch (state) {
		case State.SLEEP:
			return;
		case State.AWAKE:
			timer -= Time.deltaTime;
			if (timer <= 0f)
				ChangeState (State.MOVE);
			break;
		case State.MOVE:
			timer -= Time.deltaTime;
			if (timer <= 0f)
				ChangeState (State.SLEEP);
            break;
        case State.SPEED_UP:
        	timer -= Time.deltaTime;
        	if (timer <= 0f)
        		ChangeState (State.MOVE);
			break;
		case State.SPEED_DOWN:
			timer -= Time.deltaTime;
        	if (timer <= 0f)
        		ChangeState (State.MOVE);
            default: break;
        }
    }

    void ChangeState(State s)
    {
        switch (s)
        {
            case State.SLEEP:
                particle.enableEmission = false;
                timer = 0f;
                speed = Vector3.zero;
                accel = 0f;
                accel_times = 0;
                break;
            case State.AWAKE:
                particle.enableEmission = true;
				timer = WAIT_TO_MOVE;
                speed = 0f;
                accel = 0f;
                accel_times = 0;
                break;
            case State.MOVE:
                timer = PLAY_TIME;
                speed = 0f;
                accel = 0f;
                accel_times = 0;
                break;
            case State.SPEED_UP:
                timer = TIME_SPEED_UP;
                accel *= SPEED_ACCEL;
                accel_times += 1;
                break;
            case State.SPEED_DOWN:
            	timer = TIME_COOL_DOWN;
                accel *= SPEED_DECAY;
                accel_times -= 1;
                break;
            default: break;
        }
        state = s;
    }

    public GameObject monsterSparkle;
    // called when touched, change monster's state
    public void Touched (Vector3 target)
	{
		if (chaseCoolDown > 0)
			return;
		chaseCooLDown = CHASE_INTERVAL;
		chaseCoolDown--;
		dir = (target - transform.position).normalized;
		Instantiate (monsterSparkle, transform.position, Quaternion.identity);
		switch (state) {
			case State.SLEEP:
				ChangeState(State.AWAKE);
				break;
			case State.MOVE:
				ChangeState(State.SPEED_UP);
				break;
			case State.SPEED_UP:
				ChangeState(State.SPEED_UP);
				break;
			case State.SPEED_DOWN:
				ChangeState(State.MOVE);
				break;
			default: break;
		}
	}

   	// called when monster's status changes to move
   	public void Move () {
   		// get player's move
		speed = -GameManager.instance.GetPlayerMove();
		// set y to 0
		speed.y = 0f;
		particle.transform.position += speed;
	}

	public void ModifySpeed () {
		// get the hit times
		speed = -GameManager.instance.GetPlayerMove();
		speed.y = 0f;
		speed = speed * accel;
		particle.transform.position += speed;
		// particle gets brighter or darker based accel


	}

    // called when touched, change monster's state
    void OnTriggerEnter(Collider col) {
        // if collides with player, gameover
        if (col.gameObject.layer == Constant.LAYER_PLAYER)
        {
            GameManager.instance.GetPlayer().Die();
        }

        // if collides with obstacles, stop
        foreach (int layer in Constant.LAYER_OBSTACLES)
        {
            if (col.gameObject.layer == layer)
            {
                ChangeState(State.STAY);
                return;
            }
        }
    }
}
