namespace LYGOAP
{
    public interface IAction<TAction>
    {
        TAction Label { get; }

        /// <summary>
        /// 当前动作的花费
        /// </summary>
        int Cost { get; }

        /// <summary>
        /// 优先级
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// 是否可以被打断
        /// </summary>
        bool CanInterruptiblePlan { get; }

        /// <summary>
        /// 先决条件
        /// </summary>
        IState Precconditions { get;}

        /// <summary>
        /// 造成的结果
        /// </summary>
        IState Effects { get; }


        /// <summary>
        /// 是否满足条件
        /// </summary>
        bool VerifyPreconditions();
    }

    public abstract class ActionBase<TAction> : IAction<TAction>
    {
        public abstract TAction Label { get; }
        public abstract int Cost { get; }
        public abstract int Priority { get; }
        public abstract bool CanInterruptiblePlan { get; }
        public IState Precconditions { get; private set; }
        public IState Effects { get; private set; }

        private IAgent agent;

        public ActionBase(IAgent agent)
        {
            Precconditions = InitPreconditions();
            Effects = InitEffects();
            this.agent = agent;
        }


        protected abstract IState InitPreconditions();
        protected abstract IState InitEffects();


        public  bool VerifyPreconditions()
        {
            return agent.AgentState.ContainState(Precconditions);
        }
    }
}
