using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class BlowDryerAir : MonoBehaviour
{
    [SerializeField]
    private Material[] materials;

    public void BlowHair(ObiRope rope)
    {
        //set new material
        int index = Random.Range(0, materials.Length);
        Color currentColor = rope.gameObject.GetComponent<Renderer>().material.color;

        rope.gameObject.GetComponent<Renderer>().material = materials[index];
        rope.gameObject.GetComponent<Renderer>().material.color = currentColor;
    }

}
