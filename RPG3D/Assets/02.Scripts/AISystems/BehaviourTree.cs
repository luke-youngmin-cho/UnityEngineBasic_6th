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
        public Root root => _root;
        private Root _root;
        

        public virtual Result Run()
        {
            return _root.Invoke();
        }

        #region Builder

        private Behaviour _current;
        private Stack<Composite> _compositeStack;

        public BehaviourTree StartBuild()
        {
            _root = new Root();
            _current = _root;
            _compositeStack = new Stack<Composite>();
            return this;
        }

        private void AttachAsChild(Behaviour parent, Behaviour child)
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
            if (_compositeStack.Count > 1)
            {
                _compositeStack.Pop();
                _current = _compositeStack.Peek();
            }
            else if (_compositeStack.Count == 1)
            {
                _compositeStack.Pop();
                _current = null;
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
            AttachAsChild(_current, selector);
            _current = selector;
            _compositeStack.Push(selector);
            return this;
        }

        public BehaviourTree RandomSelector()
        {
            Composite selector = new RandomSelector();
            AttachAsChild(_current, selector);
            _current = selector;
            _compositeStack.Push(selector);
            return this;
        }

        public BehaviourTree Sequence()
        {
            Composite sequence = new Sequence();
            AttachAsChild(_current, sequence);
            _current = sequence;
            _compositeStack.Push(sequence);
            return this;
        }

        public BehaviourTree RandomSequence()
        {
            Composite sequence = new RandomSequence();
            AttachAsChild(_current, sequence);
            _current = sequence;
            _compositeStack.Push(sequence);
            return this;
        }

        public BehaviourTree Condition(Func<bool> func)
        {
            Behaviour condition = new Condition(func);
            AttachAsChild(_current, condition);
            _current = condition;
            return this;
        }

        public BehaviourTree Repeat(int times)
        {
            Behaviour repeat = new Repeat(times);
            AttachAsChild(_current, repeat);
            _current = repeat;
            return this;
        }

        public BehaviourTree Execution(Func<Result> execute)
        { 
            Behaviour execution = new Execution(execute);
            AttachAsChild(_current, execution);

            if (_compositeStack.Count > 0)
                _current = _compositeStack.Peek();
            else
                _current = null;

            return this;
        }


        #endregion
    }
}
