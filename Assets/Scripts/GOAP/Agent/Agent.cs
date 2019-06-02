namespace LYGOAP
{
    /// <summary>
    /// 代理接口
    /// </summary>
    public interface IAgent
    {
        IState AgentState { get; }

        /// <summary>
        /// 更新
        /// </summary>
        void UpdateData();


        void FrameFun();
    }


    public abstract class AgentBase : IAgent
    {
        public IState AgentState { get; private set; }

        public AgentBase()
        {
            DebugBase.Instance = InitDebugBase();
            AgentState = new State();
            AgentState.AddStateChangeListener(UpdateData);
        }


        protected abstract DebugBase InitDebugBase();


        public void UpdateData()
        {
            throw new System.NotImplementedException();
        }


        public void FrameFun()
        {
            throw new System.NotImplementedException();
        }
    }
}
