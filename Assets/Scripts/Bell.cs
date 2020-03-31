using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) {
        foreach (ContactPoint contact in collision.contacts) {
            OVRGrabber grabber = contact.otherCollider.GetComponentInParent<OVRGrabber>();
            // if (!grabber) { continue; }
            GameLoop.Instance.BellDonged();
        }
    }
}
