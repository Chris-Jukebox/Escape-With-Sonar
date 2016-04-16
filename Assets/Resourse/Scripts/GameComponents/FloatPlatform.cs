using UnityEngine;
using System.Collections;

public class FloatPlatform : MonoBehaviour {

    private bool running = false;

    /// <summary>
    /// A node that stores information of a single move
    /// </summary>
    private class MoveNode
    {
        public Vector3 dir;
        public float dur;
        public float degree;
        public MoveNode(Vector3 _dir, float _dur, float _degree)
        {
            this.dir = _dir;
            this.dur = _dur;
            this.degree = _degree;
        }
    }

    /// <summary>
    /// A task queue that stores a series of moves
    /// </summary>
    private Queue moveQueue;

    /// <summary>
    /// Add a single move into task queue
    /// </summary>
    /// <param name="dir">direction with length</param>
    /// <param name="dur">duration of the move</param>
	public void AddMove(Vector3 dir, float dur, float rotDegree)
    {
        moveQueue.Enqueue(new MoveNode(dir, dur, rotDegree));
        if (!running)
        {
            StopAllCoroutines();
            StartCoroutine("MoveRoutine");
        }
    }
    
    /// <summary>
    /// Moving coroutine
    /// </summary>
    /// <param name="move">current move</param>
    /// <returns></returns>
    private IEnumerator MoveRoutine()
    {
        MoveNode move = (MoveNode) moveQueue.Dequeue();
        if (move == null)
        {
            running = false;
        }
        else
        {
            running = true;
            Vector3 speed = move.dir / move.dur;
            float t = 0;
            while (t < move.dur)
            {
                t += Time.deltaTime;
                transform.position += speed * Time.deltaTime;
                transform.RotateAround(transform.position, transform.up, move.degree * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            StartCoroutine(MoveRoutine());
        }
    }
}
