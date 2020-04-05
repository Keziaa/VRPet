using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimate : MonoBehaviour
{
	private const float headRaycastLength = 0.5f;
	private const float mouthRaycastLength = 0.5f;

	private const float medScale = 1.2f;
	private const float maxScale = 1.4f;

	public bool isFlying;
	public bool inStartPos;

	private enum GrowStates { Small, Medium, Large}
	private GrowStates growState;

	private OVRPlayerController player;
	private OVRGrabbable grabbable;
	private Animator animator;
	private Vector3 startPos;

	private bool pettingHead;
	private bool onFall;
	private bool isEating;
	private bool isGrowing = false;

    // Start is called before the first frame update
    private void Start()
    {
		growState = GrowStates.Small;
		inStartPos = true;
		startPos = this.transform.position;
		grabbable = GetComponent<OVRGrabbable>();
		animator = GetComponent<Animator>();

		StartCoroutine(check_if_fall_cr());
    }

	public void ResetPos()
	{
		this.transform.position = startPos;
	}

    private void Update()
	{
		if (grabbable.isGrabbed)
		{
			animator.SetInteger("animation", 3);
			inStartPos = false;
		}
		else if (pettingHead)
		{
			animator.SetInteger("animation", 12);
		}
		else if(isEating)
		{
			animator.SetInteger("animation", 5);
			if(growState != GrowStates.Large && !isGrowing)
			{
				StartCoroutine(grow_big_cr());
				isGrowing = true;
			}
		}
		else if(isFlying)
		{
			animator.SetInteger("animation", 14);
		}
		else if(onFall)
		{
			animator.SetInteger("animation", 8);
		}
		else
		{
			animator.SetInteger("animation", 1);
		}

		if(Vector3.Distance(this.transform.position, startPos) > 0.5f)
		{
			inStartPos = false;
		}
    }

	private void FixedUpdate()
	{
		CheckHeadRaycast();
		CheckMouthRaycast();
	}

	private void CheckHeadRaycast()
	{
		RaycastHit hit;

		Vector3 center = this.transform.position;
		Debug.DrawRay(center, Vector3.up * headRaycastLength, Color.yellow);

		if (Physics.Raycast(center, Vector3.up, out hit, headRaycastLength))
		{
			if (hit.transform.tag == "Hand" && hit.transform != this.transform)
			{
				Debug.Log("Did Hit");
				pettingHead = true;
			}
			else
			{
				pettingHead = false;
			}
		}
		else
		{
			pettingHead = false;
		}
	}

	private void CheckMouthRaycast()
	{
		RaycastHit hit;

		Vector3 center = this.transform.position + GetComponent<BoxCollider>().center;
		Debug.DrawRay(center, transform.forward * mouthRaycastLength, Color.red);

		if (Physics.Raycast(center, transform.forward, out hit, mouthRaycastLength))
		{
			if (hit.transform.tag == "Food" && hit.transform != this.transform)
			{
				Debug.Log("Eating");
				isEating = true;
			}
			else
			{
				isEating = false;
			}
		}
		else
		{
			isEating = false;
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

	private IEnumerator check_if_fall_cr()
	{
		YieldInstruction wait = new WaitForSeconds(4.0f);
		while (true)
		{
			yield return wait;

			int val = Random.Range(0, 10);

			if(val == 0)
			{
				onFall = true;
				yield return wait;
				onFall = false;
			}

			yield return null;
		}
	}

	private IEnumerator grow_big_cr()
	{
		float growScale = 1.0f;

		if(growState == GrowStates.Small)
		{
			growState = GrowStates.Medium;
			growScale = medScale;
		}
		else if(growState == GrowStates.Medium)
		{
			growState = GrowStates.Large;
			growScale = maxScale;
		}

		Vector3 newScale = new Vector3(growScale, growScale, growScale);

		float t = 0.0f;
		float time = 3.0f;

		while(t < time)
		{
			t += Time.deltaTime;
			this.transform.localScale = Vector3.Lerp(this.transform.localScale, newScale, t / time);
			yield return null;
		}

		isGrowing = false;
		yield return null;
	}
}
