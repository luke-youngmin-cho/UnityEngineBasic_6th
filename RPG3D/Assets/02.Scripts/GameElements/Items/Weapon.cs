using RPG.GameElements.StatSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RPG.GameElements.Items
{
    public enum WeaponType
    {
        None,
        BareHand,
        Sword2Handed
    }

    [RequireComponent(typeof(BoxCollider))]
    public class Weapon : Equipment
    {
        public WeaponType weaponType;
        public bool doCast
        {
            get => _doCast;
            set
            {
                if (_doCast == value)
                    return;

                if (value)
                    targetsTriggered.Clear();

                _castingTrigger.enabled = value;
                _doCast = value;
            }
        }
        private bool _doCast;
        private BoxCollider _castingTrigger;
        private LayerMask _targetMask;
        public Dictionary<int, IDamageable> targetsTriggered = new Dictionary<int, IDamageable>();
        
        public StatID damageStatID;

        private void Awake()
        {
            _castingTrigger = GetComponent<BoxCollider>();
            _targetMask = (1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Enemy"));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (doCast == false)
                return;

            if ((1 << other.gameObject.layer & _targetMask) > 0 &&
                targetsTriggered.ContainsKey(other.gameObject.GetInstanceID()) == false)
            {
                if (other.TryGetComponent(out IDamageable damageable))
                {
                    targetsTriggered.Add(other.gameObject.GetInstanceID(), damageable);
                    damageable.Damage(owner.gameObject, owner.stats[damageStatID.value].valueModified);
                    if (owner.TryGetComponent(out PlayerController player))
                    {
                        player.behaviourTree.target = other.gameObject;
                    }
                    Debug.Log($"{other.name} is casted");
                }
            }
        }
    }
}
