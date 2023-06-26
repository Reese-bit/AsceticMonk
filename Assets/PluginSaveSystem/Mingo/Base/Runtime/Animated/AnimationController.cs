using Mingo.Base.Runtime.FSM;
using UnityEngine;
using UnityEngine.Events;

namespace Mingo.Base.Runtime.Animated
{
  public class AnimationController : MonoBehaviour
  {
    public Animator animator;

    public void Play(string state)
    {
      animator.Play(state, 0, 0f);
    }
    
    public bool IsAnimationEnd()
    {
      var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
      return stateInfo.normalizedTime >= 1;
    }

    public bool IsAnimationEnd(string state)
    {
      var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
      return stateInfo.normalizedTime >= 1 && stateInfo.IsName(state);
    }
  }
}