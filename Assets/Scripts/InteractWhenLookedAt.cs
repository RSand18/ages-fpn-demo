using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWhenLookedAt : MonoBehaviour
{
    private IInteractive lookedAtInteractive;
    void Update()
    {
        if (Input.GetButtonDown("interact") && lookedAtInteractive != null)
        {
            Debug.Log("Player pressed the interact button.");
            lookedAtInteractive.InteractWith();
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
        DetectLookedAtInteractive.LookedAtInteractiveChanged += OnLookedAtinteractiveChanged;
    }
    private void OnDisable()
    {
        DetectLookedAtInteractive.LookedAtInteractiveChanged -= OnLookedAtinteractiveChanged;
    }
    #endregion
}
