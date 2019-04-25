using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects interactive elements the player is looking at.
/// https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
/// </summary>
public class DetectLookedAtInteractive : MonoBehaviour
{
    [Tooltip("Starting point of raycast used to detect interactives.")]
    [SerializeField]
    private Transform raycastOrigin;

    [Tooltip("How far from the raycastOrigin we will search for interactive elements.")]
    [SerializeField]
    private float maxRange = 3.0f;

    /// <summary>
    /// Event raised when the player looks at a different interactive.
    /// </summary>
    public static event Action<IInteractive> LookedAtInteractiveChanged;

    public IInteractive LookedAtInteractive
    {
        get
        {
            return lookedAtInteractive;
        }
        private set
        {
            bool isInteractiveChanged = value != lookedAtInteractive;
            if(isInteractiveChanged)
            {
                lookedAtInteractive = value;
                LookedAtInteractiveChanged.Invoke(lookedAtInteractive);         
            }         
        }
    }

    private IInteractive lookedAtInteractive;

    private void FixedUpdate()
    {
        LookedAtInteractive = GetLookedAtInteractive();
    }

    /// <summary>
    /// Raycasts forward from the camera to look for IInteractives.
    /// </summary>
    /// <returns>The first IInteractive detected, or null if none are found.</returns>
    private IInteractive GetLookedAtInteractive()
    {
        Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward * maxRange, Color.red);
        RaycastHit hitInfo;
        bool objectWasDetected = Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hitInfo, maxRange);

        IInteractive interactive = null;

        LookedAtInteractive = interactive;

        if (objectWasDetected && hitInfo.collider.gameObject.name != "FPSPLAYER")
        {
            //Debug.Log($"Player is looking at: {hitInfo.collider.gameObject.name}");
            interactive = hitInfo.collider.gameObject.GetComponent<IInteractive>();
        }
        return interactive;
    }
}
