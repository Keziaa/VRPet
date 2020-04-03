using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private OvrAvatar player;

	[SerializeField]
	private OVRGrabbable phone;

	[SerializeField]
	private AudioSource chirp;

	[SerializeField]
	private Transform[] spawnPoints;

	[SerializeField]
	private PetAnimate[] birds;

	private PetAnimate[] activePets;

	private void Start()
    {
		StartCoroutine(chirp_sound_cr());

		activePets = new PetAnimate[spawnPoints.Length];
		List<PetAnimate> randomizedPets = new List<PetAnimate>(birds);
	
		for(int i = 0; i < spawnPoints.Length; i++)
		{
			PetAnimate pet = randomizedPets[Random.Range(0, randomizedPets.Count)];
			activePets[i] = GameObject.Instantiate<PetAnimate>(pet);
			activePets[i].transform.position = spawnPoints[i].position;
			activePets[i].transform.rotation = spawnPoints[i].rotation;
			randomizedPets.Remove(pet);
		}
	}

	private void Update()
	{
		if(phone.isGrabbed)
		{
			for(int i = 0; i < activePets.Length; i++)
			{
				if(!activePets[i].isFlying && !activePets[i].inStartPos)
				{
					activePets[i].MoveBack();
				}
			}
		}
	}

	private IEnumerator chirp_sound_cr()
	{
		YieldInstruction wait = new WaitForSeconds(1.0f);
		int val = 0;

		while (true)
		{
			val = Random.Range(0, 8);

			if (val == 0)
			{
				chirp.Play();
			}

			yield return wait;
		}
	}
}
