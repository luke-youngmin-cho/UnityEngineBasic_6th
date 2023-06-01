using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameElements
{
    public abstract class NPC : MonoBehaviour, IInteractable
    {
        public bool canInteract => isInteracting == false;
        public bool isInteracting;

        public abstract void StartInteraction(GameObject interactor);

        public abstract void FinishInteraction();
    }
}