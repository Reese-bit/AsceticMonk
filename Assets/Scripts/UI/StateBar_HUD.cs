using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBar_HUD : StateBar
{
    public override void Initialize(float currentHealth, float maxHealth)
    {
        base.Initialize(currentHealth, maxHealth);
    }

    protected override IEnumerator BufferFillCoroutine(Image image)
    {
        return base.BufferFillCoroutine(image);
    }
}
