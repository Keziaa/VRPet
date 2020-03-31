using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Head : MonoBehaviour
{
    [SerializeField]
    private Renderer head;

    [SerializeField]
    private Renderer body;

    [SerializeField]
    private Material[] materials;

    [SerializeField]
    private ObiRope[] hairStrands;

    private void Start()
    {
        Color hairColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        Color headColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        Color bodyColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        float thiccness = Random.Range(0.6f, 1.3f);
        int randIndex = Random.Range(0, materials.Length);

        head.materials[3].color = headColor;

        body.material = materials[randIndex];
        body.material.color = bodyColor;

        foreach(ObiRope strand in hairStrands)
        {
            strand.gameObject.GetComponent<ObiRopeExtrudedRenderer>().sectionThicknessScale = thiccness;
            strand.gameObject.GetComponent<Renderer>().material = materials[randIndex];
            strand.gameObject.GetComponent<Renderer>().material.color = hairColor;
        }
    }
}
