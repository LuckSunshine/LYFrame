using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public interface IStateMachine 
    {
        /// <summary>
        /// 当前状态
        /// </summary>
        IState CurrentState { get; }

        /// <summary>
        /// 默认状态
        /// </summary>
        IState DefaultState { set; get; }

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state">要添加的状态</param>
        void AddState(IState state);

        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="state">要删除的状态</param>
        void RemoveState(IState state);

        /// <summary>
        /// 通过制定的Tag 查找状态
        /// </summary>
        /// <param name="tag">状态的tag</param>
        /// <returns></returns>
        IState GetStateWithTag(string tag);

    }

}

