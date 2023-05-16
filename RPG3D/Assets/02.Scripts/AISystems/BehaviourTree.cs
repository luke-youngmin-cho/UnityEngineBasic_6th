using System;
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

            //_behaviourTree.StartBuild()
            //    .Selector()
            //        .Sequence()
            //            .Execution()
            //            .Execution()
            //            .Condition()
            //                .Execution()
            //        .ExitComposite()
            //        .Execution()
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

        

        public void Run()
        {
            _root.Invoke();
        }

        #region Builder

        private Behaviour _current;
        private Stack<Composite> _compositeStack;

        public BehaviourTree StartBuild()
        {
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

        public BehaviourTree Selector()
        {
            Selector selector = new Selector();
            AttachAsChild(_current, selector);
            _current = selector;
            _compositeStack.Push(selector);
            return this;
        }

        #endregion
    }
}
