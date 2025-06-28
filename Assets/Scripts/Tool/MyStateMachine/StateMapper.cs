using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyStateMachine
{
    /// <summary>
    /// 储存index与State的映射
    /// </summary>
    public class StateMapper
    {
        protected Dictionary<int, State> stateDict = new Dictionary<int, State>();
        internal StateMachine stateMachine;
        internal StateMachine StateMachine
        {
            get => stateMachine;
            set
            {
                stateMachine = value;
                foreach (var state in stateDict.Values)
                {
                    state.stateMachine = value;
                }
            }
        }

        public State GetState(int enumIndex)
        {
            if (stateDict.TryGetValue(enumIndex, out var state))
                return state;
            Debug.LogWarning($"未包含{enumIndex}状态");
            return null;
        }

        public List<State> GetAllStates()
        {
            return stateDict.Values.ToList();
        }

        /// <summary>
        /// 添加一个状态，并为其分配新的index
        /// </summary>
        public void AddState(int stateIndex, State state)
        {
            if (!stateDict.ContainsKey(stateIndex))
            {
                stateDict.Add(stateIndex, state);
                state.enumIndex = stateIndex;
                state.stateMachine = StateMachine;
            }
        }

        /// <summary>
        /// 批量添加状态，要求事先分配好index
        /// </summary>
        public void AddStates(params State[] states)
        {
            foreach (State state in states)
            {
                AddState(state.enumIndex, state);
            }
        }

        /// <summary>
        /// 通过State类型和枚举类型快速创建状态，创建出的状态行为相同，用途有限
        /// </summary>
        public void AddSameStates<T, E>() where T : State where E : Enum
        {
            foreach (E value in Enum.GetValues(typeof(E)))
            {
                T state = Activator.CreateInstance<T>();
                AddState(Convert.ToInt32(value), state);
            }
        }
    }
}