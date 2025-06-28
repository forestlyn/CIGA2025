using MyStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Round
{
    internal class InitState : State
    {
        public InitState(int enumIndex) : base(enumIndex)
        {

        }
        internal override void CheckConditions()
        {
            if (true)
            {
                stateMachine.StateIndex = (int)StateType.StartRound;
            }
        }
    }
}
