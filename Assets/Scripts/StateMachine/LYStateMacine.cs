using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 状态机类需要继承于状态类并实现状态机接口
    /// </summary>
    public class LYStateMacine : LYState, IStateMachine
    {
        private IState _currentState;//当前状态
        private IState _defaultState;//默认状态
        private List<IState> _states;//所有状态
        private bool _isTransition = false;//是否正在过渡
        private ITransition _t;

        /// <summary>
        /// 当前状态
        /// </summary>
        public IState CurrentState
        {
            get { return _currentState; }
        }

        /// <summary>
        /// 默认状态
        /// </summary>
        public IState DefaultState
        {
            get { return _defaultState;}
            set
            {
                AddState(value);
                _defaultState = value;
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public LYStateMacine(string name,IState defaultState):base( name)
        {
            _states =new List<IState>();
            _defaultState = defaultState;
        }

        /// <summary>
        /// 添加状态 
        /// </summary>
        /// <param name="state">要添加的状态</param>
        public void AddState(IState state)
        {
            if (state != null && !_states.Contains(state))
            {
                _states.Add(state);
                //将新加入的状态的Parent设置为当前状态机
                state.Parent = this;
                if (_defaultState == null)
                {
                    _defaultState = state;
                }
            }
        }

        /// <summary>
        /// 通过状态的tag查找状态
        /// </summary>
        /// <param name="tag">状态的Tag值</param>
        /// <returns></returns>
        public IState GetStateWithTag(string tag)
        {
            return null;;
        }

        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="state">要删除的状态</param>
        public void RemoveState(IState state)
        {
            //状态机运行过程中如果要删除的状态时当前状态return
            if(_currentState==state)
                return;
            if (state != null && !_states.Contains(state))
            {
                _states.Remove(state);
                //将已经移除的状态的Parent设置为null
                state.Parent = null;
                if (_defaultState == state)
                {
                    _defaultState = (_states.Count >= 1) ? _states[0] : null;
                }
            }
        }

        public override void UpdateCallback(float deltaTime)
        {
            //检测是否正在过渡
            if (_isTransition)
            {
                if (_t.TransitionCallback())
                {
                    DoTransition(_t);
                    _isTransition = false;
                }
                return;
            }
            base.UpdateCallback(deltaTime);
            if (_currentState == null)
            {
                _currentState = _defaultState;
            }
            List<ITransition> ts = _currentState.Transitions;
            int count = ts.Count;
            for (int i = 0; i < count; i++)
            {
                ITransition t = ts[i];
                if (t.ShouldBegin())
                {
                    _isTransition = true;
                    _t = t;
                    return;
                }
            }
            _currentState.UpdateCallback(deltaTime);
        }

        public override void LateUpdateCallback(float deltaTime)
        {
            base.LateUpdateCallback(deltaTime);
            _currentState.LateUpdateCallback(deltaTime);
        }

        public override void FixedUpdateCallback()
        {
            base.FixedUpdateCallback();
            _currentState.FixedUpdateCallback();
        }

        /// <summary>
        /// 开始进行过渡
        /// </summary>
        /// <param name="t"></param>
        private void DoTransition(ITransition t)
        {
            _currentState.ExxiteCallback(t.To);
            _currentState = t.To;
            _currentState.EnterCallback(t.From);
        }
    }
}

