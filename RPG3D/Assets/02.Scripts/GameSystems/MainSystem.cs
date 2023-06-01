using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Tools;
using RPG.UI;

namespace RPG.GameSystems
{
    public class MainSystem : SingletonMonoBase<MainSystem>
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                UIManager.instance.HideLast();
            }
        }
    }
}