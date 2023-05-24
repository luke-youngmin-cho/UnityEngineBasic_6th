using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AISystems
{
    public enum Result
    {
        None,
        Running,
        Success,
        Failure,
    }

    public abstract class Behaviour
    {
        public abstract Result Invoke();
    }
}