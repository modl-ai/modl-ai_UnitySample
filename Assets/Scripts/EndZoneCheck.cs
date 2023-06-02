using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modl.Demo
{
    public class EndZoneCheck : MonoBehaviour
    {
        private void OnTriggerEnter(Collider playerCollider)
        {
            if (!playerCollider.CompareTag("Player")) return;

            if (!playerCollider.gameObject.GetComponent<PlayerLogic>().HasKey)
            {
            
                //NOTE: check for MODL_AUTOMATIC_TESTING flag, so the project can build without the flag and modl package.
#if MODL_AUTOMATIC_TESTING
                //Testing instrumentation, this call would normally be added by the developer, when some glitch/event happens.
                Modl.EventReporter.Report("EndZoneNoKey");
#endif
                
                Debug.Log("Entered the end zone without a key!");
            }


        }
    }
}
