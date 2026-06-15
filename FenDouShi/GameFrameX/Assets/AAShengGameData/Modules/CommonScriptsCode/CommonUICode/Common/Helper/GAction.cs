using System;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

// 基础 Action 类
public abstract class GAction
{
    public bool IsDone { get; set; }
    public GObject Target { get; set; }
    
    public abstract void Start();
    public abstract void Update(float dt);
    public virtual void Stop() {}
    
    // 安全检查方法
    public bool CheckTargetValid()
    {
        return Target != null && !Target.isDisposed;
    }
    
    // 安全的设置目标方法
    public void SetTargetSafe(GObject target)
    {
        if (target != null && !target.isDisposed)
        {
            Target = target;
        }
        else
        {
            Target = null;
            IsDone = true;
        }
    }
}

// Delay 动作
public class GDelayTime : GAction
{
    private float _duration;
    private float _elapsedTime;

    public GDelayTime(float duration)
    {
        _duration = duration;
    }

    public override void Start()
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }
        
        _elapsedTime = 0f;
        IsDone = false;
    }

    public override void Update(float dt)
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }
        
        _elapsedTime += dt;
        if (_elapsedTime >= _duration)
        {
            IsDone = true;
        }
    }
}

// 回调动作
public class GCallFunc : GAction
{
    private System.Action _callback;

    public GCallFunc(System.Action callback)
    {
        _callback = callback;
    }

    public override void Start()
    {
        IsDone = false;
    }

    public override void Update(float dt)
    {
        _callback?.Invoke();
        IsDone = true;
    }
}

// 移动动作
public class GMoveTo : GAction
{
    private Vector2 _targetPosition;
    private Vector2 _startPosition;
    private float _duration;
    private float _elapsedTime;

    public GMoveTo(Vector2 targetPosition, float duration)
    {
        _targetPosition = targetPosition;
        _duration = duration;
    }

    public override void Start()
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }
        
        _startPosition = new Vector2(Target.x, Target.y);
        _elapsedTime = 0f;
        IsDone = false;
    }

    public override void Update(float dt)
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }

        _elapsedTime += dt;
        float t = Mathf.Clamp01(_elapsedTime / _duration);
        
        Target.SetXY(
            Mathf.Lerp(_startPosition.x, _targetPosition.x, t),
            Mathf.Lerp(_startPosition.y, _targetPosition.y, t)
        );
        
        if (_elapsedTime >= _duration)
        {
            IsDone = true;
        }
    }
}

// 缩放动作
public class GScaleTo : GAction
{
    private Vector2 _targetScale;
    private Vector2 _startScale;
    private float _duration;
    private float _elapsedTime;

    public GScaleTo(Vector2 targetScale, float duration)
    {
        _targetScale = targetScale;
        _duration = duration;
    }

    public override void Start()
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }
        
        _startScale = new Vector2(Target.scaleX, Target.scaleY);
        _elapsedTime = 0f;
        IsDone = false;
    }

    public override void Update(float dt)
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }

        _elapsedTime += dt;
        float t = Mathf.Clamp01(_elapsedTime / _duration);
        
        Target.SetScale(
            Mathf.Lerp(_startScale.x, _targetScale.x, t),
            Mathf.Lerp(_startScale.y, _targetScale.y, t)
        );
        
        if (_elapsedTime >= _duration)
        {
            IsDone = true;
        }
    }
}

// 透明度动作
public class GFadeTo : GAction
{
    private float _targetAlpha;
    private float _startAlpha;
    private float _duration;
    private float _elapsedTime;

    public GFadeTo(float targetAlpha, float duration)
    {
        _targetAlpha = targetAlpha;
        _duration = duration;
    }

    public override void Start()
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }
        
        _startAlpha = Target.alpha;
        _elapsedTime = 0f;
        IsDone = false;
    }

    public override void Update(float dt)
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }

        _elapsedTime += dt;
        float t = Mathf.Clamp01(_elapsedTime / _duration);
        
        Target.alpha = Mathf.Lerp(_startAlpha, _targetAlpha, t);
        
        if (_elapsedTime >= _duration)
        {
            IsDone = true;
        }
    }
}

// 旋转动作
public class GRotateTo : GAction
{
    private float _targetRotation;
    private float _startRotation;
    private float _duration;
    private float _elapsedTime;

    public GRotateTo(float targetRotation, float duration)
    {
        _targetRotation = targetRotation;
        _duration = duration;
    }

    public override void Start()
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }
        
        _startRotation = Target.rotation;
        _elapsedTime = 0f;
        IsDone = false;
    }

    public override void Update(float dt)
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }

        _elapsedTime += dt;
        float t = Mathf.Clamp01(_elapsedTime / _duration);
        
        Target.rotation = Mathf.Lerp(_startRotation, _targetRotation, t);
        
        if (_elapsedTime >= _duration)
        {
            IsDone = true;
        }
    }
}

// 序列动作（增强版）
public class GSequence : GAction
{
    private List<GAction> _actions = new List<GAction>();
    private int _currentIndex;

    public GSequence(params GAction[] actions)
    {
        _actions.AddRange(actions);
    }

    // 添加新的动作到序列
    public GSequence Add(GAction action)
    {
        if (action != null)
        {
            _actions.Add(action);
        }
        return this;
    }

    // 添加多个动作到序列
    public GSequence AddRange(params GAction[] actions)
    {
        if (actions != null)
        {
            _actions.AddRange(actions);
        }
        return this;
    }

    // 获取动作数量
    public int Count => _actions.Count;

    // 清空所有动作
    public void Clear()
    {
        _actions.Clear();
        _currentIndex = 0;
        IsDone = true;
    }

    public override void Start()
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }
        
        _currentIndex = 0;
        IsDone = false;
        
        if (_actions.Count > 0)
        {
            _actions[0].SetTargetSafe(Target);
            _actions[0].Start();
        }
    }

    public override void Update(float dt)
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }

        if (_currentIndex >= _actions.Count)
        {
            IsDone = true;
            return;
        }

        GAction currentAction = _actions[_currentIndex];
        
        if (!currentAction.CheckTargetValid())
        {
            _currentIndex++;
            if (_currentIndex < _actions.Count)
            {
                _actions[_currentIndex].SetTargetSafe(Target);
                _actions[_currentIndex].Start();
            }
            return;
        }

        currentAction.Update(dt);

        if (currentAction.IsDone)
        {
            _currentIndex++;
            if (_currentIndex < _actions.Count)
            {
                _actions[_currentIndex].SetTargetSafe(Target);
                _actions[_currentIndex].Start();
            }
        }
    }
}

// 并行动作
public class GSpawn : GAction
{
    private List<GAction> _actions = new List<GAction>();

    public GSpawn(params GAction[] actions)
    {
        _actions.AddRange(actions);
    }
    
    // 添加新的动作到并行
    public GSpawn Add(GAction action)
    {
        if (action != null)
        {
            _actions.Add(action);
        }
        return this;
    }

    public override void Start()
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }

        IsDone = false;
        foreach (var action in _actions)
        {
            action.SetTargetSafe(Target);
            action.Start();
        }
    }

    public override void Update(float dt)
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }

        bool allDone = true;
        foreach (var action in _actions)
        {
            if (!action.IsDone)
            {
                if (action.CheckTargetValid())
                {
                    action.Update(dt);
                    allDone = false;
                }
                else
                {
                    action.IsDone = true;
                }
            }
        }
        IsDone = allDone;
    }
}

// 重复动作
public class GRepeat : GAction
{
    private GAction _innerAction;
    private int _repeatCount;
    private int _currentCount;

    public GRepeat(GAction action, int count = -1)
    {
        _innerAction = action;
        _repeatCount = count;
    }

    public override void Start()
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }
        
        _currentCount = 0;
        IsDone = false;
        StartInnerAction();
    }

    public override void Update(float dt)
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }

        if (_innerAction.IsDone)
        {
            _currentCount++;
            
            if (_repeatCount > 0 && _currentCount >= _repeatCount)
            {
                IsDone = true;
                return;
            }
            
            StartInnerAction();
        }
        
        _innerAction.Update(dt);
    }

    private void StartInnerAction()
    {
        _innerAction.SetTargetSafe(Target);
        _innerAction.Start();
    }
}

// 闪烁动作
public class GBlink : GAction
{
    private float _duration;
    private float _interval;
    private float _elapsedTime;
    private float _blinkTimer;
    private bool _visibleState;

    public GBlink(float duration, float interval = 0.2f)
    {
        _duration = duration;
        _interval = interval;
    }

    public override void Start()
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }
        
        _elapsedTime = 0f;
        _blinkTimer = 0f;
        _visibleState = Target.visible;
        IsDone = false;
    }

    public override void Update(float dt)
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }

        _elapsedTime += dt;
        _blinkTimer += dt;

        if (_blinkTimer >= _interval)
        {
            _blinkTimer = 0f;
            Target.visible = !Target.visible;
        }

        if (_elapsedTime >= _duration)
        {
            Target.visible = _visibleState;
            IsDone = true;
        }
    }
}

// 跳动的动作
public class GBounce : GAction
{
    private Vector2 _originalPosition;
    private float _height;
    private float _duration;
    private float _elapsedTime;

    public GBounce(float height, float duration)
    {
        _height = height;
        _duration = duration;
    }

    public override void Start()
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }
        
        _originalPosition = new Vector2(Target.x, Target.y);
        _elapsedTime = 0f;
        IsDone = false;
    }

    public override void Update(float dt)
    {
        if (!CheckTargetValid())
        {
            IsDone = true;
            return;
        }

        _elapsedTime += dt;
        float t = Mathf.Clamp01(_elapsedTime / _duration);
        
        // 使用正弦函数创建弹跳效果
        float bounce = Mathf.Sin(t * Mathf.PI * 2) * _height * (1 - t);
        Target.SetXY(_originalPosition.x, _originalPosition.y - bounce);
        
        if (_elapsedTime >= _duration)
        {
            Target.SetXY(_originalPosition.x, _originalPosition.y);
            IsDone = true;
        }
    }
}