using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public bool keyPickedUp;

    private Collider _collider;
    private Renderer _renderer;

    private bool _isEnabled;
    
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _renderer = GetComponentInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider playerCollider)
    {
        if (playerCollider.CompareTag("Player"))
        {
            //NOTE: check for MODL_AUTOMATIC_TESTING flag, so the project can build without the flag and modl package.
#if MODL_AUTOMATIC_TESTING
            //Testing instrumentation, this call would normally be added by the developer, when some glitch/event happens.
            Modl.EventReporter.Report("KeyPickedUp", gameObject.name);
#endif
            
            keyPickedUp = true;
            playerCollider.gameObject.GetComponent<PlayerLogic>().HasKey = true;
            gameObject.SetActive(false);
        }
    }

}
