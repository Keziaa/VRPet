using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;

public class PlatformManager : MonoBehaviour
{
    //https://developer.oculus.com/documentation/platform/latest/concepts/pgsg-unity-gsg/

    const string appID = "2137878189621606";

    void Awake()
    {
        try {
            Oculus.Platform.Core.AsyncInitialize(appID);
            Oculus.Platform.Entitlements.IsUserEntitledToApplication().OnComplete(EntitlementCallback);
        } catch (UnityException e) {
            Debug.LogError("Platform failed to initialize due to exception.");
            Debug.LogException(e);
            // Immediately quit the application.
            UnityEngine.Application.Quit();
        }
    }

    void EntitlementCallback(Message msg)
    {
        if (msg.IsError) {
            Debug.LogError("You are NOT entitled to use this app.");
            UnityEngine.Application.Quit();
        }
        else {
            Debug.Log("You are entitled to use this app.");
        }
    }
}
