using MyStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Round
{
    internal class EndRoundState : State
    {
        float timer = 0f;
        public EndRoundState(int enumIndex) : base(enumIndex)
        {

        }
        internal override void CheckConditions()
        {
            if (timer >= 1f)
            {
                stateMachine.StateIndex = (int)StateType.None;
                timer = 0f;
            }
        }

        protected internal override void OnEnter(int enumIndex)
        {
            base.OnEnter(enumIndex);
            RoundManager.Instance.EndRoundEvent.Invoke(this, new EventArgs());
            Debug.Log($"Round {RoundManager.Instance.CurrentRound} ended.");
        }
        internal override void LogicUpdate()
        {
            base.LogicUpdate();
            timer += Time.deltaTime;
        }
    }
}
