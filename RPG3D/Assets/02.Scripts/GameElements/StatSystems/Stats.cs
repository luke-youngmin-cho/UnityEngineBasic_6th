using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace RPG.GameElements.StatSystems
{
    public class Stats
    {
        public Stat this[int id] => _dictionary[id];
        private Dictionary<int, Stat> _dictionary;

        public Stats(Dictionary<int, Stat> dictionary)
        {
            this._dictionary = dictionary;
        }
    }
}