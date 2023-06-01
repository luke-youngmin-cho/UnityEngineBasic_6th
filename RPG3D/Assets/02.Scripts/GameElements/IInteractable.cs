using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameElements
{
    public interface IInteractable
    {
        bool canInteract { get; }
        void StartInteraction(GameObject interactor);
        void FinishInteraction();
    }
}