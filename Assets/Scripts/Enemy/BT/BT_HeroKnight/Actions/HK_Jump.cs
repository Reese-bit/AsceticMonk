using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;

public class HK_Jump : BT_HeroKnight
{
    public float horizontalForce = 5.0f;
    public float jumpForce = 10.0f;
    public float buildupTime;
    public float jumpTime;
    public string jumpTriggerName;
    public bool shakeCameraOnLanding;

    private bool isLanded;

    private Tween buildupTween;
    private Tween jumpTween;

    public override void OnStart()
    {
        buildupTween = DOVirtual.DelayedCall(buildupTime, StartJump, false);
        anim.SetTrigger(jumpTriggerName);
    }

    public override TaskStatus OnUpdate()
    {
        return isLanded ? TaskStatus.Success : TaskStatus.Running;
    }

    public void StartJump()
    {
        var direction = player.transform.position.x < transform.position.x ? -1 : 1;
        rb.AddForce(new Vector2(horizontalForce * direction ,jumpForce),ForceMode2D.Impulse);

        jumpTween = DOVirtual.DelayedCall(jumpTime, () =>
        {
            isLanded = true;
            if (shakeCameraOnLanding)
            {

            }
        }, false);
    }

    public override void OnEnd()
    {
        buildupTween?.Kill();
        jumpTween?.Kill();
        isLanded = false;
    }
}
