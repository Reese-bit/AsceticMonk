using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : Singleton<InputManager>
{
	//PlayerCharacter character;
    [Header("控制是否使用自定义按键")]
    public bool keyIsSet;
    [Header("左移动")]
    public KeyCode LeftMoveKey;
    [Header("右移动")]
    public KeyCode RightMoveKey;
    [Header("持剑站立")]
    public KeyCode CombatIdleKey;
    [FormerlySerializedAs("Jump")] 
    [Header("跳跃按键")]
    public KeyCode JumpKey;
    [FormerlySerializedAs("Dash")] 
    [Header("冲刺按键")]
    public KeyCode DashKey;
    [FormerlySerializedAs("Climb")] 
    [Header("爬墙按键")]
    public KeyCode ClimbKey;
    [FormerlySerializedAs("IdleBlock")] 
    [Header("站立防御按键")]
    public KeyCode IdleBlockKey;
    [FormerlySerializedAs("Attack")] 
    [Header("攻击按键")] 
    public KeyCode AttackKey;
    [HideInInspector] public bool Attack => Input.GetKeyDown(AttackKey);

    [HideInInspector] public bool CombatIdle => Input.GetKey(CombatIdleKey);

    [HideInInspector] public bool IdleBlock => Input.GetKey(IdleBlockKey);

    [HideInInspector]
    public bool Climb { get {return Input.GetKey(ClimbKey); } }
    [HideInInspector]
    public bool ClimbDown { get { return Input.GetKeyDown(ClimbKey); } }
    [HideInInspector]
    public bool ClimbUp { get { return Input.GetKeyUp(ClimbKey); } }
    [HideInInspector]
    public bool Jump { get { return Input.GetKey(JumpKey); } }
    [HideInInspector]
    public bool JumpKeyDown {
        get
        {
            if(Input.GetKeyDown(JumpKey))
            {
	            //Debug.Log("JumpKey Pressed");
				return true;
            }
            else if(JumpFrame > 0)
            {
				return true;
            }
            return false;
        }
    }
    [HideInInspector]
    public bool JumpKeyUp { get { return Input.GetKeyUp(JumpKey); } }
    [HideInInspector]
    public bool Dash { get { return Input.GetKey(DashKey); } }
    [HideInInspector]
    public bool DashDown { get { return Input.GetKeyDown(DashKey); } }
    [HideInInspector]
    public bool DashUp { get { return Input.GetKeyUp(DashKey); } }
    [HideInInspector]
    public float v = 0;
    public float h = 0;
    public AnimationCurve MoveStartCurve;
    public AnimationCurve MoveEndCurve;
    [SerializeField]
    float MoveStartTime;
    [SerializeField]
    float MoveEndTime;
    [SerializeField]
    public int MoveDir;

    int JumpFrame;

    protected void OnEnable()
    {
	    //character = GetComponent<PlayerCharacter>();
        KeyInit();
    }
    public void KeyInit()
    {
        if (!keyIsSet)
        {
	        LeftMoveKey = KeyCode.A;
	        RightMoveKey = KeyCode.D;
	        CombatIdleKey = KeyCode.C;
            JumpKey = KeyCode.Space;
            AttackKey = KeyCode.J;
            DashKey = KeyCode.K;
            //DashKey = KeyCode.L;  // !!!!!!!!!!!!!!!!!!
            IdleBlockKey = KeyCode.E;
        }
    }

    private void FixedUpdate()
    {
        if(JumpFrame >= 0)
        {
            JumpFrame--;
        }
    }

    private void Update()
    {
        CheckHorzontalMove();
        v = Input.GetAxisRaw("Vertical");
		h = Input.GetAxisRaw("Horizontal");
		if (Input.GetKeyDown(JumpKey))
        {
            JumpFrame = 3;       //在落地前3帧按起跳仍然能跳
        }
    }

    void CheckHorzontalMove()
    {
		if (Input.GetKeyDown(RightMoveKey) && h<= 0)
		{
				MoveDir = 1;
		}
		else if (Input.GetKeyDown(LeftMoveKey) && h >= 0)
		{
		
				MoveDir = -1;
		}
		else if (Input.GetKeyUp(RightMoveKey))
		{
			if (Input.GetKey(LeftMoveKey))  //放开右键的时候仍按着左键
			{
				MoveDir = -1;
				MoveStartTime = Time.time;
			}
			else
			{
				MoveDir = 0;
			}
		}
		else if (Input.GetKeyUp(LeftMoveKey))
		{
			if (Input.GetKey(RightMoveKey))
			{
				MoveDir = 1;
			}
			else
			{
				MoveDir = 0;
			}
		}
	}

}
