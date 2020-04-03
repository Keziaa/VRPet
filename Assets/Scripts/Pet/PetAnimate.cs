using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimate : MonoBehaviour
{
	public bool isFlying;
	public bool inStartPos;

	private OVRPlayerController player;
	private OVRGrabbable grabbable;
	private Animator animator;
	private Vector3 startPos;

	private bool pettingHead;

    // Start is called before the first frame update
    private void Start()
    {
		inStartPos = true;
		startPos = this.transform.position;
		grabbable = GetComponent<OVRGrabbable>();
		animator = GetComponent<Animator>();
    }

	public void ResetPos()
	{
		this.transform.position = startPos;
	}

    // Update is called once per frame
    private void Update()
	{
		CheckRaycast();
		if (grabbable.isGrabbed)
		{
			animator.SetInteger("animation", 3);
			inStartPos = false;
		}
		else if (pettingHead)
		{
			animator.SetInteger("animation", 12);
		}
		else if(isFlying)
		{
			animator.SetInteger("animation", 14);
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

	public void MoveBack()
	{
		this.GetComponent<ObjectResetter>().MoveBack();
	}
}
