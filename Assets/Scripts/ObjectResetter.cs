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
	PetAnimate pet;

    const float boundsLimit = 3.0f;
	private bool movingBack = false;
	private bool colliding = false;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
		pet = GetComponent<PetAnimate>();
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
            transform.position.y < -1.0f)
		{
			if (!movingBack)
			{
				MoveBack();
			}
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

	public void MoveBack()
	{
		pet.isFlying = true;
		movingBack = true;

		this.GetComponent<Collider>().enabled = false;
		this.transform.LookAt(initialPosition);
		if (rb)
		{
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			ResetOthers();
		}

		StartCoroutine(move_back_cr());
	}

	private IEnumerator move_back_cr()
	{
		float t = 0.0f;
		float time = 5.0f;

		Vector3 startPos = this.transform.position;

		while(t < time)
		{
			t += Time.fixedDeltaTime;
			this.transform.position = Vector3.Lerp(startPos, initialPosition, t / time);
			yield return new WaitForFixedUpdate();
		}

		ResetInitial();

		this.GetComponent<Collider>().enabled = true;
		movingBack = false;
		pet.isFlying = false;
		pet.inStartPos = true;

		yield return null;
	}
}
