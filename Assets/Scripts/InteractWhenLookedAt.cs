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
    private IInteractive lookedAtInteractive;

    //Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && lookedAtInteractive != null)
        {
            Debug.Log("Player pressed the interact button.");
            lookedAtInteractive.InteractWith();
        }
    }

    ///<summary>
    ///Event handler for DetectLookedAtInteract,LookedAtInteractiveChanged
    /// </summary>
    
    private void OnLookedAtInteractiveChanged(IInteractive newLookedAtInteractive)
    {
        lookedAtInteractive = newLookedAtInteractive;
    }

    #region Event subscription / unsubscription
    private void OnEnable()
    {
        DetectLookedAtInteractive.LookedAtInteractiveChanged += OnLookedAtInteractiveChanged;
    }
    private void OnDisable()
    {
        DetectLookedAtInteractive.LookedAtInteractiveChanged -= OnLookedAtInteractiveChanged;
    }
    #endregion
}
