using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class BlowDryer : ItemBase
{
    [SerializeField]
    private AudioSource dryerStartSFX;

    [SerializeField]
    private AudioSource dryerLoopSFX;

    [SerializeField]
    private AudioSource dryerEndSFX;

    protected override void StartSFX()
    {
        base.StartSFX();
        dryerStartSFX.Play();
        StartCoroutine(play_loop_cr());
    }

    protected override void EndSFX()
    {
        base.EndSFX();
        dryerStartSFX.Stop();
        dryerLoopSFX.Stop();
        dryerEndSFX.Play();
    }

    private IEnumerator play_loop_cr()
    {
        yield return new WaitForSeconds(0.5f);
        dryerLoopSFX.Play();
        yield return null;
    }
}
