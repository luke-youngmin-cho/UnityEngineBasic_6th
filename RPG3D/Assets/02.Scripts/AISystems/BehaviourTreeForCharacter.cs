using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AISystems
{
    public class BehaviourTreeForCharacter : BehaviourTree
    {
        public GameObject owner;
        public AnimatorWrapper animator;
        public IEnumerator<Result> runningFSM;
        public Result status;

        public override Result Run()
        {
            Result tmp = Result.Failure;

            if (status == Result.Running)
            {
                if (runningFSM.MoveNext())
                {
                    tmp = runningFSM.Current;
                }
            }
            else
            {
                tmp = base.Run();
            }

            status = tmp;
            return tmp;
        }
    }
}