using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;


public class LightContorller : MonoBehaviour
{
    public  float         maxIntensity;
    public  float         fadeSpeed;
    public  float         colorSpeed;
    private bool          _isOpen        = true;
    private bool          _ischangeColor = false;
    private LYStateMacine _fsm;       //1.声明一个状态机
    private LYStateMacine _open;      //2.声明状态 打开
    private LYState       _close;     //关闭状态
    private LYTransition  _openClose; //3.声明状态过渡 打开到关闭
    private LYTransition  _closeOpen; //关闭到打开
    private LYTransition  _colorIntensity;
    private LYTransition  _intensityColor;
    private LYState       _changeIntensity;
    private LYState       _changeColor;

    private Light _light;
    private bool  _isAnimation;
    private bool  _isRest;
    private float _target;

    private Color _targetColor;
    private Color _startColor;
    private float _colorTimer;


    // Use this for initialization
    void Start()
    {
        _light = GetComponent<Light>();
        InitFSM();
    }


    // Update is called once per frame
    void Update()
    {
        _fsm.UpdateCallback(Time.deltaTime);
    }


    void OnGUI()
    {
        if (GUI.Button(new Rect(150f, 30f, 50f, 28f), "Open"))
        {
            _isOpen = true;
        }

        if (GUI.Button(new Rect(150f, 65f, 50f, 28f), "Close"))
        {
            _isOpen = false;
        }
    }


    private void InitFSM()
    {
        _changeIntensity     =  new LYState("ChangeInitensity");
        _changeColor         =  new LYState("ChangeColor");
        _changeColor.OnEnter += (IState state) => { _isAnimation = false; };
        _changeColor.OnUpdate += (float f) =>
        {
            if (_isAnimation)
            {
                if (_colorTimer >= 1f)
                {
                    _isAnimation = false;
                }
                else
                {
                    _colorTimer  += Time.deltaTime * colorSpeed;
                    _light.color =  Color.Lerp(_startColor, _targetColor, _colorTimer);
                }
            }
            else
            {
                float r = Random.Range(0f, 1f);
                float g = Random.Range(0f, 1f);
                float b = Random.Range(0f, 1f);
                _targetColor = new Color(r, g, b);
                _startColor  = _light.color;
                _colorTimer  = 0f;
                _isAnimation = true;
            }
        };
        _changeIntensity.OnUpdate += (float f) =>
        {
            if (_isAnimation)
            {
                if (_isRest)
                {
                    if (FadeTo(maxIntensity))
                    {
                        _isRest      = false;
                        _isAnimation = false;
                    }
                }
                else
                {
                    if (FadeTo(_target))
                    {
                        _isRest = true;
                    }
                }
            }
            else
            {
                _target      = Random.Range(0.3f, 0.7f);
                _isAnimation = true;
            }
        };
        _colorIntensity         =  new LYTransition("ColorIntensity", _changeColor, _changeIntensity);
        _colorIntensity.OnCheck += () => { return _ischangeColor; };
        _changeColor.AddTransition(_colorIntensity);
        _intensityColor         =  new LYTransition("IntensityColor", _changeIntensity, _changeColor);
        _intensityColor.OnCheck += () => { return !_ischangeColor; };
        _changeIntensity.AddTransition(_intensityColor);
        _open                   =  new LYStateMacine("Open", _changeIntensity);
        _open.OnEnter           += (IState state) => { _light.intensity = maxIntensity; };
        _close                  =  new LYState("Close");
        _close.OnEnter          += (IState state) => { _light.intensity = 0f; };
        _openClose              =  new LYTransition("OpenClose", _open, _close);
        _openClose.OnCheck      += () => !_isOpen;
        _openClose.OnTransition += () => FadeTo(0f);
        _open.AddTransition(_openClose);
        _closeOpen              =  new LYTransition("CloseOpen", _close, _open);
        _closeOpen.OnCheck      += () => _isOpen;
        _closeOpen.OnTransition += () => FadeTo(maxIntensity);
        _close.AddTransition(_closeOpen);
        _fsm = new LYStateMacine("Root", _open);
        _fsm.AddState(_close);
    }


    /// <summary>
    /// 将灯光光强渐变到指定的值
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    private bool FadeTo(float f)
    {
        if (Mathf.Abs(_light.intensity - f) <= 0.05f)
        {
            _light.intensity = f;
            return true;
        }
        else
        {
            int flag = _light.intensity > f ? -1 : 1;
            _light.intensity += Time.deltaTime * fadeSpeed * flag;
            return false;
        }
    }
}