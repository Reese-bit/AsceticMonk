using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class HK_IsGround : BT_Conditionals
{
    public float groundDistance = 0.2f;  // 射线的长度
    public float footOffset = 0.4f;  // 两条射线的距离
    public float rayPositionY = -0.5f;  // 射线的Y轴
    public LayerMask groundLayer;  // 地面图层
    
    public override TaskStatus OnUpdate()
    {
        isGround = PhysicsCheck();
        return isGround ? TaskStatus.Success : TaskStatus.Running;
    }
    
    public bool PhysicsCheck()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, rayPositionY), Vector2.down, 
            groundDistance, groundLayer);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, rayPositionY), Vector2.down, 
            groundDistance, groundLayer);

        if (leftCheck || rightCheck)
        {
            isGround = true;
            return true;
        }
        else
        {
            isGround = false;
            return false;
        }
    }
    
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length,LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, layer);

        Color color = hit ? Color.red : Color.green;
        
        Debug.DrawRay(pos + offset,rayDirection * length,color);
        return hit;
    }
}
