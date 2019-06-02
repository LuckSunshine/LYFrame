using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;


public class Test : MonoBehaviour
{
    private LYStateMacine _fsm;      //状态机
    private LYState       _idle;     //闲置状态
    private LYState       _move;     //移动状态
    private LYTransition  _idleMove; //从idle到Move
    private LYTransition  _moveIdle; //从Move到Idle
    public  float         speed   = 10f;
    private bool          _isMove = false; //能否开始移动


    // Use this for initialization
    void Start()
    {
        //初始化状态
        _idle             =  new LYState("Idle");
        _idle.OnEnter     += (IState state) => { Debug.Log("进入Idle状态"); };
        _move             =  new LYState("Move");
        _move.OnUpdate    += (float f) => { transform.position += transform.forward * f * speed; };
        _idleMove         =  new LYTransition("IdleMove", _idle, _move);
        _idleMove.OnCheck += () => _isMove;
        _idle.AddTransition(_idleMove);
        //_idleMove.OnTransition += () => { }
        _moveIdle         =  new LYTransition("MoveIdle", _move, _idle);
        _moveIdle.OnCheck += () => !_isMove;
        _move.AddTransition(_moveIdle);
        _fsm = new LYStateMacine("Root", _idle);
        _fsm.AddState(_move);
    }


    // Update is called once per frame
    void Update()
    {
        _fsm.UpdateCallback(Time.deltaTime);
    }


    public void OnClickChangeState(int a)
    {
        if (a == 1)
        {
            _isMove = true;
        }
        else if (a == 2)
        {
            _isMove = false;
        }
    }
}