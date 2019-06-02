using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;



namespace LYGOAP
{
    /// <summary>
    /// 接口类
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// 设置当前状态
        /// </summary>
        /// <param name="key">状态名</param>
        /// <param name="value">是否为当前状态</param>
        void Set(string key, bool value);


        void Set(IState otherState);

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="key">状态名</param>
        /// <returns></returns>
        bool Get(string key);

        /// <summary>
        /// 判断是否包含Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ContainKey(string key);

        /// <summary>
        /// 是否包含当前状态
        /// </summary>
        /// <param name="otherState"></param>
        /// <returns></returns>
        bool ContainState(IState otherState);

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="onChange">委托</param>
        void AddStateChangeListener(Action onChange);

        /// <summary>
        /// 清理
        /// </summary>
        void Clear();

        /// <summary>
        /// key的集合
        /// </summary>
        /// <returns></returns>
        ICollection<string> GetKeys();
    }


    public class State : IState
    {
        private Dictionary<string, bool> dateTable;//状态的字典
        private Action onChange;//改变状态的委托

        /// <summary>
        /// 构造函数
        /// </summary>
        public State()
        {
            dateTable=new Dictionary<string, bool>();
        }

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="key">状态名</param>
        /// <param name="value">是否为当前状态</param>
        private void ChangeValue(string key,bool value)
        {
            dateTable[key] = value;
            onChange?.Invoke();
        }

        /// <summary>
        /// 添加状态改变的监听
        /// </summary>
        /// <param name="onChange"></param>
        public void AddStateChangeListener(Action onChange)
        {
            this.onChange = onChange;
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Get(string key)
        {
            if (!dateTable.ContainsKey(key))
            {
                //报错 
                Debuger.Log("当前状态不包含此键值："+key);
                return false;
            }

            return dateTable[key];
        }

        /// <summary>
        /// 获取当前状态的键
        /// </summary>
        public ICollection<string> GetKeys()
        {
            return dateTable.Keys;
        }

        /// <summary>
        /// 设置当前状态
        /// </summary>
        /// <param name="key">状态名称</param>
        /// <param name="value">是否为当前状态</param>
        public void Set(string key, bool value)
        {
            if (dateTable.ContainsKey(key) && dateTable[key] != value)
            {
                ChangeValue(key,value);
            }else if (!dateTable.ContainsKey(key))
            {
                ChangeValue(key,value);
            }
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="otherState"></param>
        public void Set(IState otherState)
        {
            foreach (string key in otherState.GetKeys())
            {
                Set(key,otherState.Get(key));
            }
        }

        public bool ContainKey(string key)
        {
            return dateTable.ContainsKey(key);
        }

        /// <summary>
        /// 是否包含当前状态
        /// </summary>
        /// <param name="otherState"></param>
        /// <returns></returns>
        public bool ContainState(IState otherState)
        {
            foreach (string key in otherState.GetKeys())
            {
                if (!ContainKey(key) || dateTable[key] != otherState.Get(key))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 清理
        /// </summary>
        public void Clear()
        {
            dateTable.Clear();
        }


        public override string ToString()
        {
            StringBuilder temp = new StringBuilder();
            foreach (KeyValuePair<string,bool> pair in dateTable)
            {
                temp.Append("key:");
                temp.Append(pair.Key);
                temp.Append("    Value:");
                temp.Append(pair.Value);
                temp.Append("\r\n");
            }

            return temp.ToString();
        }
    }


    /// <summary>
    /// 泛型类
    /// </summary>
    /// <typeparam name="TKey">泛型</typeparam>
    public class State<TKey> : State
    {
        public State():base()
        {
            
        }


        public void Set(TKey key, bool value)
        {
            base.Set(key.ToString(),value);
        }


        public bool Get(TKey key)
        {
            return base.Get(key.ToString());
        }


        public bool ContainKey(TKey key)
        {
            return base.ContainKey(key.ToString());
        }
    }
}
