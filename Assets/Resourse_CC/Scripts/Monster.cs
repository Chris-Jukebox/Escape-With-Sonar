using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    private ParticleSystem particle;

    private float timer = 0;
    private enum State {
        HIDE,
        STAY,
        WAIT_TO_MOVE,
        SPEED_UP,
        SPEED_DOWN
    };
    private State state = State.HIDE;
    
    // monster setting
    private static float MAX_SPEED = 0.03f;
    private static float SPEED_ACCEL = 1f;
    private static float SPEED_DECAY = -0.3f;
    private static float TIME_STAY = 15f;
    private static float TIME_WAIT = 1f;
    private static float TIME_SPEED_UP = 1.3f;
    private static float CHASE_INTERVAL = 1f;

    private float chaseColdDown = 0;

    // chase
    private Vector3 dir;
    private Vector3 target;
    private float speed = 0f;
    private float accel = 0f;

    void Start() {
        particle = GetComponent<ParticleSystem>();
        particle.enableEmission = false;
    }

    void Update() {
        Action();
        if (chaseColdDown > 0)
        {
            chaseColdDown -= Time.deltaTime;
            if (chaseColdDown < 0)
                chaseColdDown = 0;
        }
    }

    // called in Update(), depending on monster's state
    void Action() {
        switch (state)
        {
            case State.HIDE:
                return;
            case State.STAY:
                timer -= Time.deltaTime;
                if (timer <= 0)
                    ChangeState(State.HIDE);
                break;
            case State.WAIT_TO_MOVE:
                timer -= Time.deltaTime;
                if (timer <= 0)
                    ChangeState(State.SPEED_UP);
                break;
            case State.SPEED_UP:
                timer -= Time.deltaTime;
                if (speed < MAX_SPEED)
                speed += accel * Time.deltaTime;
                transform.position += dir * speed * Time.deltaTime;
                if (timer <= 0)
                    ChangeState(State.SPEED_DOWN);
                break;
            case State.SPEED_DOWN:
                speed += accel * Time.deltaTime;
                transform.position += dir * speed * Time.deltaTime;
                if (speed <= 0)
                    ChangeState(State.STAY);
                break;
            default: break;
        }
    }

    void ChangeState(State s)
    {
        switch (s)
        {
            case State.HIDE:
                particle.enableEmission = false;
                timer = 0;
                speed = 0;
                accel = 0;
                break;
            case State.STAY:
                particle.enableEmission = true;
                timer = TIME_STAY;
                speed = 0;
                accel = 0;
                break;
            case State.WAIT_TO_MOVE:
                timer = TIME_WAIT;
                speed = 0;
                accel = 0;
                break;
            case State.SPEED_UP:
                timer = TIME_SPEED_UP;
                accel = SPEED_ACCEL;
                break;
            case State.SPEED_DOWN:
                accel = SPEED_DECAY;
                break;
            default: break;
        }
        state = s;
    }

    public GameObject monsterSparkle;

    // called when touched, change monster's state
    public void Chase(Vector3 target) {
        if (chaseColdDown > 0)
            return;
        chaseColdDown = CHASE_INTERVAL;
        dir = (target - transform.position).normalized;
        Instantiate(monsterSparkle, transform.position, Quaternion.identity);
        switch (state)
        {
            case State.HIDE:
                ChangeState(State.STAY);
                break;
            case State.STAY:
                ChangeState(State.WAIT_TO_MOVE);
                break;
            case State.SPEED_UP:
            case State.SPEED_DOWN:
                ChangeState(State.SPEED_UP);
                break;
            default: break;
        }
    }

    void OnTriggerEnter(Collider col) {
        // if collides with player, gameover lol
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
