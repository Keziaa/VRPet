using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class StrandController : MonoBehaviour
{
    public float minLength = 0.1f;
    private ObiRopeCursor cursor;
    private ObiRope rope;

    public bool justTeared = false;

    void Update()
    {
        if (!cursor || !rope) {
            cursor = GetComponentInChildren<ObiRopeCursor>();
            rope = cursor.GetComponent<ObiRope>();
        }

        bool shrinking = Input.GetKey(KeyCode.W);
        bool growing = Input.GetKey(KeyCode.S);

        float ropeGrowthRate = HairomizerHandle.Instance.HairVelocity + 
            (shrinking ? -1 : 0) + (growing ? +1 : 0) +
            (GameLoop.Instance.ShrinkButton.isShrinking ? -1 : 0) + 
            (GameLoop.Instance.GrowButton.isGrowing ? +1 : 0);

        if (ropeGrowthRate != 0 && (ropeGrowthRate > 0 || rope.RestLength > minLength)) {
            cursor.ChangeLength(rope.RestLength + ropeGrowthRate * Time.deltaTime);
        }
    }

    public bool IsWorkable() {
        //Can you still do stuff with this hair?
        return rope.pooledParticles > 0 && rope.tearResistanceMultiplier > Razor.RazedTearResistance;
    }
}
