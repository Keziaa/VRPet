using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimate : MonoBehaviour
{
	private OVRGrabbable grabbable;
	private Animator animator;
	private bool pettingHead;

    // Start is called before the first frame update
    private void Start()
    {
		grabbable = GetComponent<OVRGrabbable>();
		animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
	{
		CheckRaycast();
		if (grabbable.isGrabbed)
		{
			animator.SetInteger("animation", 3);
		}
		else if (pettingHead)
		{
			animator.SetInteger("animation", 12);
		}
		else
		{
			animator.SetInteger("animation", 1);
		}
    }

	private void CheckRaycast()
	{
		RaycastHit hit;
	
		if (Physics.Raycast(transform.position, Vector3.up, out hit))
		{
			Debug.DrawRay(transform.position, Vector3.up * hit.distance, Color.yellow);
			if (hit.transform.tag == "Hand")
			{
				Debug.Log("Did Hit");
				pettingHead = true;
			}
			else
			{
				pettingHead = false;
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Hand")
		{
			
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "Hand")
		{
			
		}
	}
}
