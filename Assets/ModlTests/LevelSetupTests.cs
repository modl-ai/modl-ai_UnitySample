using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if MODL_AUTOMATIC_TESTING
using Modl;
#endif
using StarterAssets;

namespace Modl.Demo
{
    [Serializable]
    public class LevelSetupTests : MonoBehaviour
    {
        // These are for one-off checks that happen when the level is loaded to ensure that the level has been set up correctly

        public ThirdPersonController player;

        [Header("Ground Layer Checks")]

        [Tooltip("The parent object for the level geometry in the scene")]
        public GameObject LevelGeometry;

        [Tooltip("The number of objects that should be in the Ground Layer of the scene")]
        public int warningThreshold = 1;

        void Start()
        {
            GroundLayerCheck();
        }


        // Test to check what GroundLayers is set to, and then check if more than just the ground has that layer
        public void GroundLayerCheck()
        {
            int groundLayers = GetLayerNumber(player.GroundLayers); //doesn't work if there is more than one layer in the layermask
            int objectCount = 0;

            foreach (Transform t in LevelGeometry.GetComponentsInChildren<Transform>())
            {
                GameObject go = t.gameObject;
                Collider coll = go.GetComponent<Collider>();
                if (coll != null )
                {
                    if (!coll.isTrigger)
                    {
                        if (go.layer == groundLayers)
                        {
                            objectCount++;
                        }
                    }
                }
            }

            if (objectCount > warningThreshold)
            {
                // Give a warning that the level may have the geometry layers set up incorrectly and this could lead to issues with the Grounded check and jumping
                Debug.Log($"{objectCount} objects in the layer {LayerMask.LayerToName((int)groundLayers)}. Threshold for this level is set to {warningThreshold}");
#if MODL_AUTOMATIC_TESTING
                EventReporter.Report("GroundLayers");
#endif
            }   
        }

        public static int GetLayerNumber(LayerMask mask)
        {
            var bitmask = mask.value;
            for (int i = 0; i < 32; i++) {
                if (((1 << i) & bitmask) != 0) {
                    return i;
                }
            }
            return -1;
        }
    }
}