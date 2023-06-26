using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mingo.Base.Runtime.FSM
{
  public abstract class StateComponent : MonoBehaviour
  {
    public abstract string Name { get; }
    public bool allowReenter;
    public List<Action> BeforeEnter = new List<Action>();

    public abstract void InitState();
    
    public abstract void OnEnter();
    public abstract void OnExit();
  }
}