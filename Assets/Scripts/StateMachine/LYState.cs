using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public delegate void LYStateDelegate();
    public delegate void LYStateDelegateState(IState state);
    public delegate void LYStateDelegateFloat(float f);


    public class LYState : IState
    {
        private string _name;
        private string _tag;
        private IStateMachine _parent;
        private float _timer;
        private List<ITransition> _transitions;//状态过渡
        public event LYStateDelegateState OnEnter;//进入状态时调用
        public event LYStateDelegateState OnExit;//离开状态时调用
        public event LYStateDelegateFloat OnUpdate;//Update时调用
        public event LYStateDelegateFloat OnLateUpdate;//LateUpdate时调用
        public event LYStateDelegate OnFixedUpdate;//FixedUpdate时调用
        /// <summary>
        /// 状态名
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// 状态标签
        /// </summary>
        public string Tag
        {
            get { return _tag; }
            set { }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name"></param>
        public LYState(string name)
        {
            _name = name;
            _transitions =new List<ITransition>();
        }

        public IStateMachine Parent
        {
            set { _parent = value; }
            get { return _parent; }
        }

        public float Timer
        {
            get { return _timer; }
        }
        /// <summary>
        /// 保存所有过渡
        /// </summary>
        public List<ITransition> Transitions
        {
            get { return _transitions; }
        }

        /// <summary>
        /// 添加过渡状态
        /// </summary>
        /// <param name="t">过渡状态</param>
        public virtual void AddTransition(ITransition t)
        {
            if (t != null && !_transitions.Contains(t))
            {
                _transitions.Add(t);
            }
        }
        /// <summary>
        /// 进入状态的回调
        /// </summary>
        /// <param name="prev">上一个状态</param>
        public virtual void EnterCallback(IState prev)
        {
            _timer = 0f;
            //进入状态时会调用
            if (OnEnter != null)
            {
                OnEnter(prev);
            }
        }

        /// <summary>
        /// 退出状态的回调
        /// </summary>
        /// <param name="next">下一个状态</param>
        public virtual void ExxiteCallback(IState next)
        {
            if (OnExit != null)
            {
                OnExit(next);
            }
            //重置计时器
            _timer = 0f;
        }

        /// <summary>
        /// FixedUpdate的回调
        /// </summary>
        public virtual void FixedUpdateCallback()
        {
            //FixedUpdate时系统自动调用
            if (OnFixedUpdate != null)
            {
                OnFixedUpdate();
            }
        }
        /// <summary>
        /// LateUpdate的回调
        /// </summary>
        /// <param name="detaTime">增量时间</param>
        public virtual void LateUpdateCallback(float detaTime)
        {
            if (OnLateUpdate != null)
            {
                OnLateUpdate(detaTime);
            }
        }
        /// <summary>
        /// Update的回调
        /// </summary>
        /// <param name="detaTime">增量时间</param>
        public virtual void UpdateCallback(float detaTime)
        {
            _timer += detaTime;
            if (OnUpdate != null)
            {
                OnUpdate(detaTime);
            }
        }
    }

}

