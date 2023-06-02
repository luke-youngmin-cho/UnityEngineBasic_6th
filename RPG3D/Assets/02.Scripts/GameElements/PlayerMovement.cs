using RPG.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameElements {
    public class PlayerMovement : MovementBase
    {
        public override float v
        {
            get
            {
                if (ControllerManager.instance.TryGet(out PlayerController playerController) &&
                    playerController.controllable)
                {
                    return Input.GetAxis("Vertical");
                }
                else
                {
                    return 0.0f;
                }
            }
        }

        public override float h
        {
            get
            {
                if (ControllerManager.instance.TryGet(out PlayerController playerController) &&
                    playerController.controllable)
                {
                    return Input.GetAxis("Horizontal");
                }
                else
                {
                    return 0.0f;
                }
            }
        }

        public override float gain => Input.GetKey(KeyCode.LeftShift) ? 1.0f : 0.5f;
    }
}