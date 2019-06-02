using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// 状态名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 状态标签
        /// </summary>
        string Tag { set; get; }

        /// <summary>
        /// 当前状态的状态机
        /// </summary>
        IStateMachine Parent { get; set; }

        /// <summary>
        /// 从进入状态开始计算的时长
        /// </summary>
        float Timer { get; }

        /// <summary>
        /// 状态过渡
        /// </summary>
        List<ITransition> Transitions { get; }

        /// <summary>
        /// 进入状态的回调
        /// </summary>
        /// <param name="prev"></param>
        void EnterCallback(IState prev);
        /// <summary>
        /// 退出状态的回调
        /// </summary>
        /// <param name="next"></param>
        void ExxiteCallback(IState next);

        /// <summary>
        /// Update的回调
        /// </summary>
        /// <param name="detaTime"></param>
        void UpdateCallback(float detaTime);

        /// <summary>
        /// LateUpdate的回调
        /// </summary>
        /// <param name="detaTime"></param>
        void LateUpdateCallback(float detaTime);

        /// <summary>
        /// FixedUpdate的回调
        /// </summary>
        void FixedUpdateCallback();

        /// <summary>
        /// 添加过渡
        /// </summary>
        /// <param name="t"></param>
        void AddTransition(ITransition t);
    }
} 
