using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AISystems
{
    public class BehaviourTreeForCharacter
    {
        public bool enabled = true;
        public GameObject owner;
        public AnimatorWrapper animator;
        public IEnumerator<Result> runningFSM;
        public Result status
        {
            get => _status;
            set => _status = value;
        }
        private Result _status;
        public int currentAnimatorParameterID;
        public Root root;
        private bool _interrupted;
        public GameObject target
        {
            get => _target;
            set
            {
                if (_target == value)
                    return;

                GameObject prev = _target;
                _target = value;
                onTargetChanged?.Invoke(prev, value);
            }
        }
        private GameObject _target;
        public event Action<GameObject, GameObject> onTargetChanged; // prev, next

        public BehaviourTreeForCharacter(GameObject owner)
        {
            this.owner = owner;
            animator = owner.GetComponent<AnimatorWrapper>();
        }

        public Result Run()
        {
            if (enabled == false)
                return Result.Failure;

            if (animator.GetBool(Animator.StringToHash("hasNotInitialized")))
                return Result.Failure;

            _interrupted = false;
            Result tmp = Result.Failure;

            if (runningFSM != null &&
                runningFSM.MoveNext())
            {
                if (_interrupted)
                    return status;

                tmp = runningFSM.Current;
            }
            else
            {
                tmp = root.Invoke();
                if (_interrupted)
                    return status;
            }

            //if (status == Result.Running)
            //{
            //    if (runningFSM.MoveNext())
            //    {
            //        if (_interrupted)
            //            return status;
            //
            //        tmp = runningFSM.Current;
            //    }
            //}
            //else
            //{
            //    tmp = root.Invoke();
            //    if (_interrupted)
            //        return status;
            //}

            status = tmp;
            return tmp;
        }

        public void Interrupt(Behaviour behaviour)
        {
            status = behaviour.Invoke();

            if (status == Result.Running && 
                runningFSM.MoveNext())
            {
                status = runningFSM.Current;
            }

            _interrupted = true;
        }
        #region Builder

        protected Behaviour current;
        protected Stack<Composite> compositeStack;

        public BehaviourTreeForCharacter StartBuild()
        {
            root = new Root();
            current = root;
            compositeStack = new Stack<Composite>();
            return this;
        }

        protected void AttachAsChild(Behaviour parent, Behaviour child)
        {
            if (parent is IChild)
            {
                ((IChild)parent).child = child;
            }
            else if (parent is IChildren)
            {
                ((IChildren)parent).children.Add(child);
            }
            else
            {
                throw new Exception($"[BehaviourTree] : {parent.GetType()} cannot attach child.");
            }
        }

        public BehaviourTreeForCharacter ExitCurrentComposite()
        {
            if (compositeStack.Count > 1)
            {
                compositeStack.Pop();
                current = compositeStack.Peek();
            }
            else if (compositeStack.Count == 1)
            {
                compositeStack.Pop();
                current = null;
            }
            else
            {
                throw new Exception($"[BehaviourTree] : Cannot exit composite. Composite stack is empty.");
            }
            return this;
        }

        public BehaviourTreeForCharacter Selector()
        {
            Composite selector = new Selector();
            AttachAsChild(current, selector);
            current = selector;
            compositeStack.Push(selector);
            return this;
        }

        public BehaviourTreeForCharacter RandomSelector()
        {
            Composite selector = new RandomSelector();
            AttachAsChild(current, selector);
            current = selector;
            compositeStack.Push(selector);
            return this;
        }

        public BehaviourTreeForCharacter Sequence()
        {
            Composite sequence = new Sequence();
            AttachAsChild(current, sequence);
            current = sequence;
            compositeStack.Push(sequence);
            return this;
        }

        public BehaviourTreeForCharacter RandomSequence()
        {
            Composite sequence = new RandomSequence();
            AttachAsChild(current, sequence);
            current = sequence;
            compositeStack.Push(sequence);
            return this;
        }

        public BehaviourTreeForCharacter Condition(Func<bool> func)
        {
            Behaviour condition = new Condition(func);
            AttachAsChild(current, condition);
            current = condition;
            return this;
        }

        public BehaviourTreeForCharacter Repeat(int times)
        {
            Behaviour repeat = new Repeat(times);
            AttachAsChild(current, repeat);
            current = repeat;
            return this;
        }

        public BehaviourTreeForCharacter Execution(Func<Result> execute)
        {
            Behaviour execution = new Execution(execute);
            AttachAsChild(current, execution);

            if (compositeStack.Count > 0)
                current = compositeStack.Peek();
            else
                current = null;

            return this;
        }


        #endregion
        public BehaviourTreeForCharacter Move()
        {
            Behaviour move = new Move(this, animator, "doMove");
            AttachAsChild(current, move);

            if (compositeStack.Count > 0)
                current = compositeStack.Peek();
            else
                current = null;

            return this;
        }

        public BehaviourTreeForCharacter Jump()
        {
            Behaviour jump = new Jump(this, animator, "doJump");
            AttachAsChild(current, jump);

            if (compositeStack.Count > 0)
                current = compositeStack.Peek();
            else
                current = null;

            return this;
        }

        public BehaviourTreeForCharacter Fall()
        {
            Behaviour fall = new Fall(this, animator, "doFall");
            AttachAsChild(current, fall);

            if (compositeStack.Count > 0)
                current = compositeStack.Peek();
            else
                current = null;

            return this;
        }

        public BehaviourTreeForCharacter Land()
        {
            Behaviour land = new Land(this, animator, "doLand");
            AttachAsChild(current, land);

            if (compositeStack.Count > 0)
                current = compositeStack.Peek();
            else
                current = null;

            return this;
        }

        public BehaviourTreeForCharacter Attack()
        {
            Behaviour attack = new Attack(this, animator, "doAttack");
            AttachAsChild(current, attack);

            if (compositeStack.Count > 0)
                current = compositeStack.Peek();
            else
                current = null;

            return this;
        }

        public BehaviourTreeForCharacter Seek(float radius, float angle, float angleDelta, LayerMask targetMask, Vector3 offset)
        {
            Behaviour seek = new Seek(this, radius, angle, angleDelta, targetMask, offset);
            AttachAsChild(current, seek);

            if (compositeStack.Count > 0)
                current = compositeStack.Peek();
            else
                current = null;

            return this;
        }

        public BehaviourTreeForCharacter Follow(float endDistance)
        {
            Behaviour follow = new Follow(this, animator, "doMove", endDistance);
            AttachAsChild(current, follow);

            if (compositeStack.Count > 0)
                current = compositeStack.Peek();
            else
                current = null;

            return this;
        }

        public BehaviourTreeForCharacter Sleep(float sleepTime)
        {
            Behaviour sleep = new Sleep(this, sleepTime);
            AttachAsChild(current, sleep);
            current = sleep;
            return this;
        }
        public BehaviourTreeForCharacter RandomSleep(float sleepTimeMin, float sleepTimeMax)
        {
            Behaviour sleep = new RandomSleep(this, sleepTimeMin, sleepTimeMax);
            AttachAsChild(current, sleep);
            current = sleep;
            return this;
        }

    }
}