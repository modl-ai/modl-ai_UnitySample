using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modl.Demo
{
    public class NextLevelLoader : MonoBehaviour
    {

        public string nameOfNextLevel;

        private void OnTriggerEnter(Collider playerCollider)
        {
            if (playerCollider.CompareTag("Player"))
            {         
                Debug.Log("Loading next level..");

                //NOTE: check for MODL_AUTOMATIC_TESTING flag, so the project can build without the flag and modl package.
                var sceneToLoad = SceneManager.GetSceneByName(nameOfNextLevel);

#if MODL_AUTOMATIC_TESTING
                Modl.EventReporter.Report("LevelComplete");
                if (sceneToLoad.name == null)
                {
                    // We want the nullref to fire during the demo
                    StartCoroutine(LoadNextLevel());
                }
                //Testing instrumentation, this call would normally be added by the developer, when some glitch/event happens.
                
                
                //For testing purposes, we can set the state to be terminal, and avoid the actual load level,
                //instead letting the sidecar issue a Load command. 
                Modl.ModlPublicController.InvokeTerminalStateUpdate();
                return;
                
#else
                StartCoroutine(LoadNextLevel());
#endif
            }
        }

        IEnumerator LoadNextLevel()
        {
            yield return new WaitForSeconds(2);
            //SceneManager.LoadScene(currentLevel);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nameOfNextLevel);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

        }
    }
}