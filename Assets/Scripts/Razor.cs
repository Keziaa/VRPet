using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Razor : ItemBase
{
    public const int RazedTearResistance = 1;

    [SerializeField]
    private AudioSource razorLoopSFX;

    [SerializeField]
    private AudioSource razorEndSFX;

    public void CutHair(ObiRope rope)
    {
        rope.tearResistanceMultiplier = RazedTearResistance;
    }

    protected override void StartSFX()
    {
        base.StartSFX();
        razorLoopSFX.Play();
    }

    protected override void EndSFX()
    {
        base.EndSFX();
        razorLoopSFX.Stop();
        razorEndSFX.Play();
    }
}
