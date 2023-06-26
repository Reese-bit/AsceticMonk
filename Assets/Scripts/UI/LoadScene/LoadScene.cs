using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public new GameObject gameObject;
    
    protected Animator anim;
    protected Collider2D col;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
    }
}
