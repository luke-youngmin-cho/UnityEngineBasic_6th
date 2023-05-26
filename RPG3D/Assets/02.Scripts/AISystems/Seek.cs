using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.AISystems
{
    public class Seek : Behaviour
    {
        protected BehaviourTreeForCharacter behaviourTree;
        protected Transform owner;
        private float _radius;
        private float _angle;
        private float _angleDelta;
        private LayerMask _targetMask;
        private Vector3 _offset;

        public Seek(BehaviourTreeForCharacter behaviourTree,
                    float radius,
                    float angle,
                    float angleDelta,
                    LayerMask targetMask,
                    Vector3 offset) 
        {
            this.behaviourTree = behaviourTree;
            this.owner = behaviourTree.owner.transform;
            this._radius = radius;
            this._angle = angle;
            this._angleDelta = angleDelta;
            this._targetMask = targetMask;
            this._offset = offset;
        }

        public override Result Invoke()
        {
            bool result = false;
            Ray ray;
            RaycastHit hit;
            behaviourTree.target = null;
            for (float theta = 0; theta < _angle / 2.0f; theta += _angleDelta)
            {
                ray = new Ray(owner.position + _offset,
                              Quaternion.Euler(Vector3.up * theta) * (owner.forward) * _radius);

                if (Physics.Raycast(ray, out hit, _radius, _targetMask))
                {
                    Debug.DrawRay(owner.position + _offset,
                              Quaternion.Euler(Vector3.up * theta) * (owner.forward) * _radius,
                              Color.red);
                    behaviourTree.target = hit.transform.gameObject;
                    result = true;
                }
                else
                {
                    Debug.DrawRay(owner.position + _offset,
                              Quaternion.Euler(Vector3.up * theta) * (owner.forward) * _radius,
                              Color.yellow);
                }

                ray = new Ray(owner.position + _offset,
                              Quaternion.Euler(Vector3.up * -theta) * (owner.forward) * _radius);

                if (Physics.Raycast(ray, out hit, _radius, _targetMask))
                {
                    Debug.DrawRay(owner.position + _offset,
                              Quaternion.Euler(Vector3.up * -theta) * (owner.forward) * _radius,
                              Color.red);
                    behaviourTree.target = hit.transform.gameObject;
                    result = true;
                }
                else
                {
                    Debug.DrawRay(owner.position + _offset,
                              Quaternion.Euler(Vector3.up * -theta) * (owner.forward) * _radius,
                              Color.yellow);
                }
            }

            return result ? Result.Success : Result.Failure;
        }
    }
}
