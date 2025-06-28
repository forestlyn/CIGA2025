using MyStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Round
{
    internal class PlayState : State
    {
        public PlayState(int enumIndex) : base(enumIndex)
        {
        }
        internal override void CheckConditions()
        {
            
        }
        protected internal override void OnEnter(int enumIndex)
        {
            base.OnEnter(enumIndex);
            GameManger.Instance.SetCardGroupActive(true);
        }

        protected internal override void OnExit(int enumIndex)
        {
            base.OnExit(enumIndex);
            GameManger.Instance.SetCardGroupActive(false);
        }
    }
}
