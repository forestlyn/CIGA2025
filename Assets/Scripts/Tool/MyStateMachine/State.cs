namespace MyStateMachine
{
    public abstract class State
    {
        protected internal StateMachine stateMachine;
        protected internal int enumIndex;

        public State()
        {
            enumIndex = 0;
        }

        public State(int index)
        {
            enumIndex = index;
        }

        /// <summary>
        /// 进入状态时
        /// </summary>
        /// <param name="enumIndex">上一个状态</param>
        protected internal virtual void OnEnter(int enumIndex)
        {

        }

        /// <summary>
        /// 离开状态时
        /// </summary>
        /// <param name="enumIndex">下一个状态</param>
        protected internal virtual void OnExit(int enumIndex)
        {

        }

        /// <summary>
        /// 处于此状态时，每帧自动调用
        /// </summary>
        internal virtual void LogicUpdate()
        {
            CheckConditions();
        }
        internal virtual void PhysicsUpdate() { }
        internal virtual void LateUpdate() { }
        internal abstract void CheckConditions();
    }
}