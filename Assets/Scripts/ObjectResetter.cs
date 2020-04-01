using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectResetter : MonoBehaviour
{
    [SerializeField]
    Rigidbody[] extraRBs;

    Vector3 initialPosition;
    Quaternion initialRotation;
    Rigidbody rb;

    const float boundsLimit = 2.0f;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    void ResetInitial()
    {
        transform.position = initialPosition + Vector3.up * 0.05f; //style
        transform.rotation = initialRotation;
        if (rb) {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            ResetOthers();
        }
    }

    void Update()
    {
        if ((transform.position - initialPosition).sqrMagnitude > boundsLimit*boundsLimit ||
            transform.position.y < -1.0f) {
            ResetInitial();
        }
    }

    private void ResetOthers()
    {
        foreach(Rigidbody rb in extraRBs)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
