#if MODL_AUTOMATIC_TESTING
using Modl;
#endif
using System;
using UnityEngine;

public class ModlBootstrapper : MonoBehaviour
{
#if MODL_AUTOMATIC_TESTING
    private void Start()
    {
        Debug.Log("test game.Start()");
        if (!ModlPublicController.IsTransmitting)
        {
            ModlPublicController.Start();
        }
    }
#endif
}