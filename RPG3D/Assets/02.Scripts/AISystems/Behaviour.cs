using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AISystems
{
    public enum Result
    {
        Success,
        Failure,
        Running
    }

    public abstract class Behaviour
    {
        public abstract Result Invoke();
    }
}