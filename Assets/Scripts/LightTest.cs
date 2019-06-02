using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;


public class LightTest : MonoBehaviour
{
    private LYState _openState;  //灯光的打开状态
    private LYState _closeState; //灯光的关闭状态

    private Light _light; //灯光组件


    void Awake()
    {
        _openState          =  new LYState("OpenState");
        _openState.OnUpdate += (float f) => { Debug.Log("Hello,woeld"); };
        _openState.OnEnter  += new LYStateDelegateState(WillEnterOpenState);
        _closeState         =  new LYState("CloseState");
    }


    private void WillEnterOpenState(IState prev)
    {
        Debug.Log("即将打开");
        _light.color = Color.red;
    }
}