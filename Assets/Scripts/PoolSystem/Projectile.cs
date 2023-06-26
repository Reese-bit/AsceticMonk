using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public UnityEvent destroyEvent = new UnityEvent();
    public bool isDestroied;
    public float destroyTime;
    public float checkTime;

    // Start is called before the first frame update
    void Start()
    {
        isDestroied = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDestroy();
    }

    void CheckDestroy()
    {
        checkTime += Time.deltaTime;
        
        if (checkTime >= destroyTime &&!isDestroied)
        {
            isDestroied = true;
            destroyEvent?.Invoke();
        }
    }
}
