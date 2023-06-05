using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.InputSystems 
{
    public class CustomStandaloneInputModule : StandaloneInputModule
    {
        public bool IsPointerOverGameObject<T>(out GameObject selectedObject, int mouseID = kMouseLeftId)
            where T : BaseRaycaster
        {
            selectedObject = null;
            if (IsPointerOverGameObject(mouseID))
            {
                if (m_PointerData.TryGetValue(mouseID, out PointerEventData pointerEventData))
                {
                    return pointerEventData.pointerCurrentRaycast.module.GetType() == typeof(T);
                }
            }
            return false;
        }

        public bool TryGetHovered<T>(out List<GameObject> hovered, int mouseID = kMouseLeftId)
            where T : BaseRaycaster
        {
            hovered = null;
            if (IsPointerOverGameObject(mouseID))
            {
                if (m_PointerData.TryGetValue(mouseID, out PointerEventData pointerEventData))
                {
                    hovered = pointerEventData.hovered;
                    if (pointerEventData.pointerCurrentRaycast.module != null)
                        return pointerEventData.pointerCurrentRaycast.module.GetType() == typeof(T);
                }
            }
            return false;
        }
    }
}