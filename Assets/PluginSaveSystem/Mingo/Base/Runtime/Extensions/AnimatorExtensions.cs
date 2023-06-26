using UnityEngine;

namespace Mingo.Base.Runtime.Extensions
{
  public static class AnimatorExtensions
  {
    public static bool IsAnimationEnd(this Animator animator)
    {
      var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
      return stateInfo.normalizedTime >= 1;
    }

    public static bool IsAnimationEnd(this Animator animator, string state)
    {
      var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
      return stateInfo.normalizedTime >= 1 && stateInfo.IsName(state);
    }
  }
}