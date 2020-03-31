using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCan : ItemBase
{
    [SerializeField]
    private AudioSource airloopSFX;

    protected override void StartSFX()
    {
        base.StartSFX();
        airloopSFX.Play();
    }

    protected override void EndSFX()
    {
        base.EndSFX();
        airloopSFX.Stop();
    }
}
