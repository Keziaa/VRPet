using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Scissors : ItemBase
{
    [SerializeField]
    private AudioSource scissorsSFX;

    [SerializeField]
    private ObiRope DebugAutoCutThisRope;

    protected override void Update()
    {
        base.Update();
        bool cutNow = Input.GetKeyDown(KeyCode.C);
        if (DebugAutoCutThisRope && cutNow) {
            CutHair(DebugAutoCutThisRope, 1);
        }
    }

    public void CutHair(ObiRope rope, int indexToCut)
    {    
        ObiDistanceConstraintBatch distanceBatch = rope.DistanceConstraints.GetFirstBatch();

        if (indexToCut < distanceBatch.ConstraintCount)
        {
            rope.DistanceConstraints.RemoveFromSolver(null);
            rope.BendingConstraints.RemoveFromSolver(null);

            if (!scissorsSFX.isPlaying)
            {
                scissorsSFX.Play();
            }
            rope.Tear(indexToCut);

            rope.BendingConstraints.AddToSolver(this);
            rope.DistanceConstraints.AddToSolver(this);

            // update active bending constraints:
            rope.BendingConstraints.SetActiveConstraints();

            // upload active particle list to solver:
            rope.Solver.UpdateActiveParticles();

        }
      
        if (rope.JustTeared && rope.GetComponent<ObiRopeCursor>() != null)
        {
            ObiRopeCursor cursor = rope.GetComponent<ObiRopeCursor>();

            float i = (indexToCut - 2) < 0 ? 0 : indexToCut - 2;
            float count = (float)distanceBatch.ConstraintCount;
            float coord = (float)i / count;

            cursor.normalizedCoord = coord;
            cursor.ChangeLength(rope.RestLength + 0.0001f * Time.deltaTime);
        }

        // Useful for diagnosing rope issues:
        // Debug.Log("Rope: Pooled " + rope.pooledParticles + " " + rope.UsedParticles);
    }
}
