using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene_EvilWizard : LoadScene
{
    private void OnTriggerStay2D(Collider2D other)
    {
        // UI
        gameObject.SetActive(true);
        anim.Play("Arrow");
        
        if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.DownArrow))
        {
            SceneLoader.Instance.LoadEvilWard();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        gameObject.SetActive(false);
    }
}
