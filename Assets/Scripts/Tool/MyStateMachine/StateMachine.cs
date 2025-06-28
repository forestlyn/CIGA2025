using System.Diagnostics;
using UnityEngine.Events;

namespace MyStateMachine
{
    public class StateMachine
    {
        public StateMapper mapper;
        public UnityAction<int, int> StateChange;

        // 当前状态
        protected State curState;

        protected int stateIndex;

        /// <summary>
        /// 当前状态对应的index，外部应转换为枚举使用
        /// </summary>
        public int StateIndex
        {
            get => stateIndex;
            set
            {
                if (stateIndex != value)
                {
                    State newState = mapper.GetState(value);
                    curState?.OnExit(stateIndex);
                    int temp = stateIndex;
                    //MyLog.LogWithTime($"Before StateMachine: {temp} -> {value}");
                    stateIndex = value;
                    //MyLog.LogWithTime($"StateMachine: {temp} -> {value}");
                    curState = newState;
                    newState?.OnEnter(value);
                    StateChange?.Invoke(temp, value);
                }
            }
        }

        public StateMachine(int stateIndex)
        {
            this.stateIndex = stateIndex;
        }

        /// <param name="_mapper">通过StateMapper规定状态与index间的映射</param>
        public StateMachine(StateMapper _mapper, int stateIndex = -1)
        {
            mapper = _mapper;
            mapper.StateMachine = this;
            curState = mapper.GetState(this.stateIndex);
            this.stateIndex = stateIndex;
        }

        public void LogicUpdate()
        {
            curState?.LogicUpdate();
        }

        public void PhysicsUpdate()
        {
            curState?.PhysicsUpdate();
        }

        public void LateUpdate()
        {
            curState?.LateUpdate();
        }
    }
}