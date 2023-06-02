using UnityEngine;
using StarterAssets;

namespace Modl.Demo
{
    public class OutOfBoundReset : MonoBehaviour
    {
        private void OnTriggerEnter(Collider playerCollider)
        {
            if (!playerCollider.CompareTag("Player")) return;
            
            //NOTE: check for MODL_AUTOMATIC_TESTING flag, so the project can build without the flag and modl package.
#if MODL_AUTOMATIC_TESTING
            //Testing instrumentation, this call would normally be added by the developer, when some glitch/event happens.
            Modl.EventReporter.Report("FellOffLevel");
#endif
            
            Debug.Log("Player out of bounds!");
            var charController = playerCollider.gameObject.GetComponent(typeof(CharacterController)) as CharacterController;

            if (charController == null) return;
            
            charController.enabled = false;
#if MODL_AUTOMATIC_TESTING
            //Let the bot know that it's out of bounds and no more exploration is available beyond this point.
            Modl.ModlPublicController.InvokeTerminalStateUpdate();
#else
            var playerController = playerCollider.gameObject.GetComponent(typeof(ThirdPersonController)) as ThirdPersonController;
            if (playerController != null)
            {
                var playerPos = playerController.playerStartPosition;
                playerCollider.gameObject.transform.position = playerPos;
            }
            else
            {
                Debug.LogError("PlayerController not found!");
            }

            //playerCollider.gameObject.transform.rotation = playerPos;
#endif
            charController.enabled = true;


        }
    }
}

