using System;
using System.Collections.Generic;
using Mingo.Base.Runtime.Extensions;
using UnityEngine;

namespace Mingo.Base.Runtime.FSM
{
  public abstract class StateManager : MonoBehaviour
  {
    [Disable]
    public string currentState;
    [Disable]
    public string prevState;

    public StateComponent CurrentStateComponent { get; private set; }
    private readonly Dictionary<string, StateComponent> _stateDict = new Dictionary<string, StateComponent>();
    private readonly Dictionary<StateComponent, string> _stateNameDict = new Dictionary<StateComponent, string>();

    [Header("Debug")] public bool debug;
    private Log LOG;

    protected virtual void Awake()
    {
      LOG = Log.Get(this);
      var states = GetComponentsInChildren<StateComponent>();
      foreach (var stateComponent in states)
      {
        AddState(stateComponent.Name, stateComponent);
      }
    }

    protected void AddState(string state, StateComponent component)
    {
      _stateDict.Add(state, component);
      _stateNameDict.Add(component, state);
      component.InitState();
    }

    public virtual void OnEnter(string state)
    {
      if (debug)
      {
        LOG.D($"enter {state}");
      }
      currentState = state;
      CurrentStateComponent = _stateDict[state];
      foreach (var action in CurrentStateComponent.BeforeEnter)
      {
        action.Invoke();
      }
      CurrentStateComponent.BeforeEnter.Clear();
      CurrentStateComponent.OnEnter();
    }

    public virtual void OnExit(string state)
    {
      if (debug)
      {
        LOG.D($"exit {state}");
      }
      _stateDict[state].OnExit();
      prevState = state;
    }

    public StateComponent GetStateComponent(string state)
    {
      return _stateDict[state];
    }

    public T GetCurrentStateComponent<T>() where T : StateComponent
    {
      return GetStateComponent(currentState) as T;
    }

    public virtual bool TriggerTransitionTo(string state)
    {
      if (currentState == state && !_stateDict[state].allowReenter)
      {
        return false;
      } 
      if (!currentState.IsNullOrWhitespace())
      {
        OnExit(currentState);
      }
      OnEnter(state);
      return true;
    }
    
  }
}