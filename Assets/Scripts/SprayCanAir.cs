using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class SprayCanAir : MonoBehaviour
{
    public void SprayHair(ObiRope rope)
    {
        //set new material and color
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        rope.gameObject.GetComponent<Renderer>().material.color = color;
    }
}
