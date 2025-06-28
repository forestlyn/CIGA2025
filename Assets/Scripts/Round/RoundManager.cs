using Assets.Scripts.Round;
using MyStateMachine;
using MyTools.MyEventSystem;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions.Must;

namespace Assets.Scripts
{
    public enum StateType
    {
        None,
        StartRound,
        OnPlay,
        EndRound,
    }
    public class RoundManager
    {
        private StateMachine stateMachine;
        private StateMapper stateMapper;


        public StateType StateType
        {
            get { return (StateType)stateMachine.StateIndex; }
        }

        private static RoundManager _instance;
        public static RoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new System.Exception("RoundManager instance is not initialized. Please call Init() before accessing the instance.");
                }
                return _instance;
            }
        }
        private int currentRound;

        public int CurrentRound
        {
            get { return currentRound; }
            set { currentRound = value; }
        }
        public RoundManager()
        {
            currentRound = 0;
            _instance = this;
        }

        public void ResetRound()
        {
            currentRound = 0;
        }

        public MyEvent StartNewRoundEvent = MyEvent.CreateEvent((int)EventTypeEnum.RoundStart);
        public MyEvent EndRoundEvent = MyEvent.CreateEvent((int)EventTypeEnum.RoundEnd);

        public void StartNewRound()
        {
            stateMachine.StateIndex = (int)StateType.StartRound;
        }

        public void EndRound()
        {
            stateMachine.StateIndex = (int)StateType.EndRound;
        }
        public int GetCurrentRound()
        {
            return currentRound;
        }

        public void Init()
        {
            stateMapper = new StateMapper();
            stateMapper.AddState((int)StateType.None, new InitState((int)StateType.None));
            stateMapper.AddState((int)StateType.StartRound, new StartRoundState((int)StateType.StartRound));
            stateMapper.AddState((int)StateType.OnPlay, new PlayState((int)StateType.OnPlay));
            stateMapper.AddState((int)StateType.EndRound, new EndRoundState((int)(StateType.EndRound)));
            stateMachine = new StateMachine(stateMapper, 0);
        }

        public void LogicUpdate()
        {
            stateMachine?.LogicUpdate();
        }

        public void PhysicsUpdate()
        {
            stateMachine?.PhysicsUpdate();
        }

        public void LateUpdate()
        {
            stateMachine?.LateUpdate();
        }

    }
}
