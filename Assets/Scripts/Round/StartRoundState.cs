using MyStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Round
{
    internal class StartRoundState : State
    {
        public StartRoundState(int enumIndex) : base(enumIndex)
        {

        }
        internal override void CheckConditions()
        {
            
        }

        float timer = 0f;

        internal override void LogicUpdate()
        {
            base.LogicUpdate();
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                stateMachine.StateIndex = (int)StateType.OnPlay;
                timer = 0f;
            }
        }

        protected internal override void OnEnter(int enumIndex)
        {
            base.OnEnter(enumIndex);
            RoundManager.Instance.CurrentRound++;
            RoundManager.Instance.StartNewRoundEvent.Invoke(this, new EventArgs());
            //Debug.Log($"Round {RoundManager.Instance.CurrentRound} started.");
        }

        protected internal override void OnExit(int enumIndex)
        {
            base.OnExit(enumIndex);
            
        }
    }
}
