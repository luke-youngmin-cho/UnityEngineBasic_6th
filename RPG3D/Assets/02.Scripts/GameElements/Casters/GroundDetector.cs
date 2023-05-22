using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.GameElements.Casters
{
    public class GroundDetector : MonoBehaviour
    {
        public bool isDetected;
        [SerializeField] private float _range;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private LayerMask _groundMask;

        public bool TryCastGround(out RaycastHit hit)
        {
            return Physics.SphereCast(transform.position + Vector3.up,
                                      _range,
                                      Vector3.down,
                                      out hit,
                                      float.PositiveInfinity,
                                      _groundMask);
        }

        private void FixedUpdate()
        {
            Collider[] cols = Physics.OverlapSphere(transform.position + _offset,
                                                    _range,
                                                    _groundMask);
            isDetected = cols.Length > 0;
        }
    }
}