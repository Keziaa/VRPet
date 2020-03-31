using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
        {
            GameLoop.Instance.DonePressed();
        }
    }
}
