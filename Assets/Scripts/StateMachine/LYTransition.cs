using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public delegate bool LYTransitionDelegate();
    public class LYTransition : ITransition
    {
        private IState _from;
        private IState _to;
        private string _name;
        public event LYTransitionDelegate OnTransition;
        public event LYTransitionDelegate OnCheck;
        /// <summary>
        /// 从哪个状态
        /// </summary>
        public IState From
        {
            get { return _from; }

            set { _from = value; }
        }
        /// <summary>
        /// 过渡到哪个状态
        /// </summary>
        public IState To
        {
            get { return _to;}
            set { _to = value; }
        }

        /// <summary>
        /// 过渡名
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        public LYTransition(string name, IState fromStte, IState toState)
        {
            _name = name;
            _from = fromStte;
            _to = toState;
        }
        /// <summary>
        /// 过渡的回调
        /// </summary>
        /// <returns></returns>
        public bool TransitionCallback()
        {
            if (OnTransition != null)
            {
                return OnTransition();
            }
            return true;
        }
        /// <summary>
        /// 能否开始的回调
        /// </summary>
        /// <returns></returns>
        public bool ShouldBegin()
        {
            if (OnCheck != null)
            {
                return OnCheck();
            }
            return false;
        }
    }

}

