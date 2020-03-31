using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowButton : MonoBehaviour
{
    [SerializeField]
    private bool isGrow;

    public bool isGrowing { get; private set; }
    public bool isShrinking { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
        {
            if (isGrow)
            {
                isGrowing = true;
            }
            else
            {
                isShrinking = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand")
        {
            if (isGrow)
            {
                isGrowing = false;
            }
            else
            {
               isShrinking = false;
            }
        }
    }
}
