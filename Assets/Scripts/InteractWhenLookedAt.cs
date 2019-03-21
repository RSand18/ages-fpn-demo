using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects when the player presses the interact button while looking at an interactive,
/// and then calls that IInteractive's InteractWith method.
/// </summary>
public class InteractWhenLookedAt : MonoBehaviour
{
    [SerializeField]
    private DetectLookedAtInteractive detectLookedAtInteractive;

    //Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("interact") && detectLookedAtInteractive.LookedAtInteractive != null)
        {
            Debug.Log("Player pressed the interact button.");
            detectLookedAtInteractive.LookedAtInteractive.InteractWith();
        }
    }

    ///<summary>
    ///Event handler for DetectLookedAtInteract,LookedAtInteractiveChanged
    /// </summary>
    
    private void OnLookedAtinteractiveChanged(IInteractive newLookedAtInteractive)
    {
        lookedAtInteractive = newLookedAtInteractive;
    }

    #region Event subscription / unsubscription
    private void OnEnable()
    {
        detectLookedAtInteractive.LookedAtInteractive += OnLookedAtinteractiveChanged;
    }
    private void OnDisable()
    {
        DetectLookedAtInteractive.LookedAtInteractiveChanged -= OnLookedAtinteractiveChanged;
    }
    #endregion
}
