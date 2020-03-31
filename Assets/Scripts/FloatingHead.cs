using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHead : MonoBehaviour
{
    [SerializeField]
    float amplitude = 0.03f;
    [SerializeField]
    float wavelength = 10;

    Vector3 initialPosition;

    void Awake()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.position = initialPosition + Vector3.up * amplitude * Mathf.Sin(Time.time / wavelength);
    }
}
