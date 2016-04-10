﻿using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    private ParticleSystem particle;

    private float timer = 0;
    private enum State {
        HIDE,
        STAY,
        SPEED_UP,
        SPEED_DOWN
    };
    private State state = State.HIDE;
    // monster setting
    private static float MAX_SPEED = 1f;
    private static float SPEED_ACCEL = 1f;
    private static float SPEED_DECAY = -0.3f;
    private static float TIME_STAY = 2f;
    private static float TIME_SPEED_UP = 0.5f;

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
            case State.SPEED_UP:
                timer -= Time.deltaTime;
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

    // called when touched, change monster's state
    public void Chase(Vector3 target) {
        dir = (target - transform.position).normalized;
        switch (state)
        {
            case State.HIDE:
                ChangeState(State.STAY);
                break;
            case State.STAY:
            case State.SPEED_UP:
            case State.SPEED_DOWN:
                ChangeState(State.SPEED_UP);
                break;
            default: break;
        }
    }

    void OnCollisionEnter(Collision col) {
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
