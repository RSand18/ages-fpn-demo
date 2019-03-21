using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InteractiveObject : MonoBehaviour, IInteractive
{
    [SerializeField]
         protected string displayText = nameof(InteractiveObject);

    public string DisplayText => displayText;
    private AudioSource audioSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public virtual void InteractWith()
    {
        try
        {
            audioSource.Play();
        }
        catch (System.Exception)
        {
            throw new System.Exception("Missing audioSource component: InteractiveObject requires an AudioSource component.");
        }
        Debug.Log($"Player just interacted with" + (gameObject.name) + ".");
    }
}
