using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EW_Jump : BT_EvilWizard
{
    [Tooltip("跳跃最高高度")]
    public float jumpHigh;
    [Tooltip("跳跃力")]
    public float jumpForce;
    [Tooltip("跳跃自然掉落临界点比例")]
    public float Ratio;
    [Tooltip("跳跃无法掉落临界点比例")]
    public float fallRatio;

    private bool isJump;
    private Transform shortPlayer;
    private Transform shortEnemy;
    private float fixedDistance;

    public override void OnStart()
    {
        shortPlayer.position = player.transform.position;
        shortEnemy.position = transform.position;
        fixedDistance = Mathf.Abs(shortEnemy.position.x - shortPlayer.position.x);
    }

    void StartJump()
    {
        isJump = true;
        Vector2 target = new Vector2(shortPlayer.transform.position.x + 8.0f, shortPlayer.transform.position.y);
        Vector2 newPos = Vector2.MoveTowards(transform.position, target, jumpForce * Time.deltaTime);
        rb.MovePosition(newPos);
    }

    bool CheckPlayer()
    {
        if (Mathf.Abs(shortPlayer.transform.position.x - transform.position.x) < Mathf.Epsilon)
        {
            // TODO Straight Fall
            // TODO 两边都释放出三波导弹
            return true;
        }

        // 判断是否已到达无法下砸临界点
        var tmp = shortPlayer.position.x - Mathf.Abs(Ratio * fixedDistance);
        if (Mathf.Abs(transform.position.x - tmp) < Mathf.Epsilon)
        {
            // TODO Natural Fall
            return false;
        }
        
        // 判断是否无法进行下砸临界点 (离原来位置很近)
        if ((Mathf.Abs(transform.position.x) - fixedDistance * fallRatio) < Mathf.Epsilon)
        {
            // TODO Straight Fall But No Fire
            return false;
        }

        return true;
    }
}
