using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformReset : MonoBehaviour
{
    public float resetHeightOffset = 10f; // Distance below the camera to deactivate the platform

    void Update()
    {
        if (transform.position.y < Camera.main.transform.position.y - resetHeightOffset)
        {
            gameObject.SetActive(false); // Deactivate the platform
        }
    }
}
