using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public interface ITransition 
    {
        /// <summary>
        /// 从哪个状态过渡
        /// </summary>
        IState From { get; set; }

        /// <summary>
        /// 过渡到哪个状态
        /// </summary>
        IState To { get; set; }

        /// <summary>
        /// 过渡名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 过渡时的回调
        /// </summary>
        /// <returns>true 过渡结束 false 继续过渡</returns>
        bool TransitionCallback();

        /// <summary>
        /// 能否开始过渡的回调
        /// </summary>
        /// <returns></returns>
        bool ShouldBegin();
    }

}

