using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] private Vector3 puntA = new Vector3(127, 25, 56);
    [SerializeField] private Vector3 puntB = new Vector3(127, 25, 26);
    [SerializeField] private float speed = 2;
    private float t;
    private void Update()
    {
        t += Time.deltaTime * speed;
        // Moves the object to target position
        transform.position = Vector3.Lerp(puntA, puntB, t);
        // Flip the points once it has reached the target
        if (t >= 1)
        {
            var b = puntB;
            var a = puntA;
            puntA = b;
            puntB = a;
            t = 0;
        }
    }
    // What Linear interpolation actually looks like in terms of code
    private Vector3 CustomLerp(Vector3 a, Vector3 b, float t)
    {
        return a * (1 - t) + b * t;
    }
}