using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.GameSystems
{
    public class SceneInitializer : MonoBehaviour
    {
        public static bool IsInitialized;
        [SerializeField] private List<GameObject> _objectsNeedToAwake;

        private void Awake()
        {
            foreach (GameObject go in _objectsNeedToAwake)
            {
                if (go.activeSelf == false)
                {
                    go.SetActive(true);
                    go.SetActive(false);
                }
            }

            IsInitialized = true;
            SceneManager.sceneUnloaded -= ResetInitialziedFlag;
            SceneManager.sceneUnloaded += ResetInitialziedFlag;
        }

        private void ResetInitialziedFlag(Scene scene)
        {
            IsInitialized = false;
        }
    }
}