using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class Character : MonoBehaviour
{
    [Header("----DEATH----")]
    [SerializeField]protected float maxHealth;
    
    [Header("----MICS----")]
    [SerializeField] protected float invincibleTime = 1.0f;
    [SerializeField] protected float timeControl = 1.0f;
    public bool isPaused = false;
    //[SerializeField] protected StateBar_HUD stateBarHUD;

    public float health;
    protected  WaitForSeconds waitForInvincibleTime;

    protected virtual void Awake()
    {
        waitForInvincibleTime = new WaitForSeconds(invincibleTime);
    }

    protected virtual void OnEnable()
    {
        health = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        if (health == 0f) return;

        health -= damage;

        if (health <= 0f)
        {
            Die();
        }
    }
    
    public virtual void Die()
    {
        health = 0f;
        //gameObject.SetActive(false);
        //stateBarHUD.gameObject.SetActive(false);
        //stateBarHUD.UpdateState(health,maxHealth);
    }

    public virtual void Restore(float value)
    {
        if (health == maxHealth)
        {
            return;
        }

        health = Mathf.Clamp(health + value, 0f, maxHealth);
    }
    
    public virtual void BackUp(float distance)
    {
        //rb.velocity = new Vector2(distance * 5 * lookAtDir / Mathf.Abs(lookAtDir), 0);
    }

    protected IEnumerator RestoreHealthCoroutine(WaitForSeconds waitTime, float percent)
    {
        while (health < maxHealth)
        {
            yield return waitTime;
            
            Restore(maxHealth * percent);
        }
    }

    public static void PauseGame()
    {
        Object[] objs = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject objects in objs)
        {
            objects.SendMessage("OnPauseGame",SendMessageOptions.DontRequireReceiver);
        }
    }

    public static void ResumeGame()
    {
        Object[] objs = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject objects in objs)
        {
            objects.SendMessage("OnResumeGame",SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnPauseGame()
    {
        isPaused = true;
        GameManager.GameState = GameState.Menu;
    }

    void OnResumeGame()
    {
        isPaused = false;
    }
}
