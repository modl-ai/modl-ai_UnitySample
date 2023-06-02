using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modl.Demo
{
    public class DoorOpener : MonoBehaviour
    {
        public bool doorOpen;
        public bool singleUseKey = false;
        private bool playerHasKey;

        private void OnTriggerEnter(Collider playerCollider)
        {
            if (playerCollider.CompareTag("Player"))
            {
                if (doorOpen == false && playerCollider.gameObject.GetComponent<PlayerLogic>().HasKey)
                {
                    //NOTE: check for MODL_AUTOMATIC_TESTING flag, so the project can build without the flag and modl package.
    #if MODL_AUTOMATIC_TESTING
                    //Testing instrumentation, this call would normally be added by the developer, when some glitch/event happens.
                    Modl.EventReporter.Report("DoorOpened", gameObject.name);
    #endif
                    if (singleUseKey)
                    {
                        playerCollider.gameObject.GetComponent<PlayerLogic>().HasKey = false;
                    }
                    doorOpen = true;
                    gameObject.SetActive(false);
                }
            }

            
        }

    }
}