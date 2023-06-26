using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform top, buttom, platForm;
    public float topY, buttomY, platFormY;
    public float speed = 1.0f;
    public float waitForSecond = 1.0f;
    public bool isUp = false;
    public bool isDown = false;
    public bool isTouched = false;

    private Rigidbody2D rb;
    private Collider2D col;
    private WaitForSeconds waitForSeconds;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // 修改会影响性能,此时不支持AddForce
        col = GetComponent<Collider2D>();
        topY = top.position.y;
        buttomY = buttom.position.y;
        Destroy(top.gameObject);
        Destroy(buttom.gameObject);

        waitForSeconds = new WaitForSeconds(waitForSecond);
    }

    private void Update()
    {
        platFormY = platForm.position.y;
        
        CheckIsUp();
        CheckIsDown();
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        isTouched = coll.gameObject.CompareTag("Player");
        //Debug.Log("OnCollisionStay2D");
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isTouched = false;
        //Debug.Log("OnCollisionExit");
        Debug.Log(isTouched);
    }

    void CheckIsUp()
    {
        // 平台到达顶点
        // platform arrived at top
        if (Math.Abs(platFormY - topY) < 0.1f)
        {
            isUp = true;
            isDown = false;
            //Debug.Log("isUP is true");
            StartCoroutine(nameof(DownCoroutine));
            StopCoroutine(nameof(TopCoroutine));
        }
    }

    void CheckIsDown()
    {
        // 平台到达底部
        // platform arrived at buttom
        if (Math.Abs(platFormY - buttomY) < 0.1f)
        {
            isDown = true;
            isUp = false;
            //Debug.Log("isDown is true");
            //StopCoroutine(nameof(DownCoroutine));


            //TODO 检测玩家与平台接触
            if (isTouched)
            {
                //rb.velocity = Vector2.up * speed;
                //Debug.Log("isTouched is true");
                StartCoroutine(nameof(TopCoroutine));
            }
            else
            {
                StopCoroutine(nameof(DownCoroutine));
                StopCoroutine(nameof(TopCoroutine));
                rb.velocity = Vector2.zero;
            }
        }
    }

    IEnumerator TopCoroutine()
    {
        yield return waitForSeconds;
        //rb.velocity = new Vector2(rb.velocity.x, speed);
        rb.velocity = Vector2.up * speed;
        // Vector2 vDistance = top.transform.position - platForm.transform.position;
        // rb.velocity = vDistance.normalized;
        //Debug.Log("TopCoroutine is Started");
    }

    IEnumerator DownCoroutine()
    {
        yield return waitForSeconds;
        //rb.velocity = new Vector2(rb.velocity.x, -speed);
        rb.velocity = Vector2.down * speed;
        //Debug.Log("DownCoroutine is started");
    }
}
