using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modl.Demo
{
    public class LevelLoader : MonoBehaviour
    {
        private void Awake()
        {
            //Automatically load level based on comment line argument
            var args = Environment.GetCommandLineArgs();
            var levelToLoad = string.Empty;

            for (var index = 0; index < args.Length; index++)
            {
                //NOTE: if "--modl_load_level" is specified multiple times, we will only be loading the last level specified.
                if (!args[index].Contains("--modl_load_level") || (index + 1) >= args.Length) continue;

                if (args[index + 1].Contains("Playground"))
                {
                    levelToLoad = "Playground";
                }
                else if (args[index + 1].Contains("Labyrinth"))
                {
                    levelToLoad = "Labyrinth";
                }
                else if (args[index + 1].Contains("BigWorld"))
                {
                    levelToLoad = "BigWorld";
                }
            }

            if (!string.IsNullOrWhiteSpace(levelToLoad))
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }

        public void Playground()
        {
            SceneManager.LoadScene("Playground");
        }

        public void Labyrinth()
        {
            SceneManager.LoadScene("Labyrinth");
        }

        public void BigWorld()
        {
            SceneManager.LoadScene("BigWorld");
        }
    }
}

