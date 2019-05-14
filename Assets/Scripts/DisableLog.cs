using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableLog : InteractiveObject
{
    public GameObject log; // Used to disable log for final note
    void Start()
    {
        log = GetComponent<GameObject>();
    }

    public override void InteractWith()
    {
        base.InteractWith();
        log.SetActive(false);
    }
}
