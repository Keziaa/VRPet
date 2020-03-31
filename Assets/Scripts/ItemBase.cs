using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    protected OVRGrabbable grabbable;
    private bool playingMusic = false;

    protected virtual void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
    }

    protected virtual void Update()
    {
        if (grabbable != null)
        {
            if (grabbable.isGrabbed)
            {
                if (!playingMusic)
                {
                    playingMusic = true;
                    StartSFX();
                }
            }
            else
            {
                if (playingMusic)
                {
                    playingMusic = false;
                    EndSFX();
                }
            }
        }
    }

    protected virtual void StartSFX()
    {

    }

    protected virtual void EndSFX()
    {

    }
}
