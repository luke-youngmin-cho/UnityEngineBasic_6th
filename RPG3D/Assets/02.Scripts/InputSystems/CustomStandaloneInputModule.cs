using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.InputSystems 
{
    public class CustomStandaloneInputModule : StandaloneInputModule
    {
        public bool IsPointerOverGameObject<T>(int mouseID)
            where T : BaseRaycaster
        {
            if (IsPointerOverGameObject(mouseID))
            {
                if (m_PointerData.TryGetValue(mouseID, out PointerEventData pointerEventData))
                {
                    return pointerEventData.pointerCurrentRaycast.module.GetType() == typeof(T);
                }
            }
            return false;
        }
    }
}