using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Beater : ItemBase
{
    [SerializeField]
    private AudioSource beaterSFX;

    public void TwistHair(ObiRope rope)
    {
        if (!beaterSFX.isPlaying)
        {
            beaterSFX.Play();
        }
        rope.GetComponent<ObiRopeExtrudedRenderer>().sectionTwist = 50.0f;
    }
}
