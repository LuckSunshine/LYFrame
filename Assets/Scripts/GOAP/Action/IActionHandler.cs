using System;



namespace LYGOAP
{
    public interface IActionHandler<TAction>
    {
        IAction<TAction> Action { get; }
        TAction          LabelP { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsComplete { get; }

        /// <summary>
        /// 能否执行
        /// </summary>
        bool CanPerformAction { get; }

        void AddFinishCallBack(System.Action onFinishAction);
    }


    public abstract class ActionHandlerBase<TAction> : IActionHandler<TAction>
    {
        public IAction<TAction> Action { get; private set; }

        public TAction LabelP{get;private set;}

        public bool IsComplete { get; private set; }

        public bool CanPerformAction { get; private set; }

        private Action onFinishAction;

        private IAgent agent;


        public ActionHandlerBase(IAgent agent, IAction<TAction> action)
        {
            if (action != null)
            {
                Debuger.LogError("动作不能为空");
            }
            Action = action;
            IsComplete = false;
            CanPerformAction = false;
            this.agent = agent;
        }

        public void AddFinishCallBack(Action onFinishAction)
        {
            this.onFinishAction = onFinishAction;
        }


        protected void OnComplete()
        {
            IsComplete = true;
            onFinishAction?.Invoke();
            SetAgentState(Action.Effects);
        }


        private void SetAgentState(IState state)
        {
            agent.AgentState.Set(state);
        }
    }
}