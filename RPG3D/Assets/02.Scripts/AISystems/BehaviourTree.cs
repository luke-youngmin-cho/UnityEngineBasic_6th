using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.AISystems
{
    public class Dummy : MonoBehaviour
    {
        BehaviourTree _behaviourTree;

        private void Awake()
        {
            _behaviourTree = new BehaviourTree();
            //_behaviourTree.root.child = new Selector();
            //((Selector)_behaviourTree.root.child).children.Add(new Condition(() => true));
            //((Selector)_behaviourTree.root.child).children.Add(new Execution(() => Result.Success));
            //((Selector)_behaviourTree.root.child).children.Add(new RandomSelector());
            //((Condition)((Selector)_behaviourTree.root.child).children[0]).child = new Sequence();

            _behaviourTree.StartBuild()
                .Selector()
                    .Condition(() => true)
                        .Sequence()
                            .Execution(() => Result.Success)
                            .Execution(() => Result.Success)
                            .Condition(() => false)
                                .Execution(() => Result.Failure)
                        .ExitCurrentComposite()
                    .Execution(() => Result.Success)
                    .RandomSelector()
                        .Execution(() => Result.Failure)
                        .Execution(() => Result.Success)
                    .ExitCurrentComposite()
                .ExitCurrentComposite();
        }

        private void Update()
        {
            _behaviourTree.Run();
        }
    }




    public class BehaviourTree
    {
        protected Root root;
        

        public virtual Result Run()
        {
            return root.Invoke();
        }

        #region Builder

        protected Behaviour current;
        protected Stack<Composite> compositeStack;

        public BehaviourTree StartBuild()
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

        public BehaviourTree ExitCurrentComposite()
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

        public BehaviourTree Selector()
        {
            Composite selector = new Selector();
            AttachAsChild(current, selector);
            current = selector;
            compositeStack.Push(selector);
            return this;
        }

        public BehaviourTree RandomSelector()
        {
            Composite selector = new RandomSelector();
            AttachAsChild(current, selector);
            current = selector;
            compositeStack.Push(selector);
            return this;
        }

        public BehaviourTree Sequence()
        {
            Composite sequence = new Sequence();
            AttachAsChild(current, sequence);
            current = sequence;
            compositeStack.Push(sequence);
            return this;
        }

        public BehaviourTree RandomSequence()
        {
            Composite sequence = new RandomSequence();
            AttachAsChild(current, sequence);
            current = sequence;
            compositeStack.Push(sequence);
            return this;
        }

        public BehaviourTree Condition(Func<bool> func)
        {
            Behaviour condition = new Condition(func);
            AttachAsChild(current, condition);
            current = condition;
            return this;
        }

        public BehaviourTree Repeat(int times)
        {
            Behaviour repeat = new Repeat(times);
            AttachAsChild(current, repeat);
            current = repeat;
            return this;
        }

        public BehaviourTree Execution(Func<Result> execute)
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
    }
}
