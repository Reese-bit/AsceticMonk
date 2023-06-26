using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene_HeroKnight : LoadScene
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // UI
            gameObject.SetActive(true);
            anim.Play("Arrow");
            Debug.Log("TriggerStay is triggered");

            
            if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.DownArrow))
            {
                SceneLoader.Instance.LoadGamePlay();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        gameObject.SetActive(false);
    }
}
