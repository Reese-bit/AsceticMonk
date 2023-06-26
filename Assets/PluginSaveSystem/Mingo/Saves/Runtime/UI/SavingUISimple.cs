using System;
using System.Collections;
using UnityEngine;

namespace Mingo.Saves.Runtime.UI
{
  public class SavingUISimple : MonoBehaviour
  {
    public GameObject root;
    public float minDisplayTime;

    private float _lastShowTime;
    private bool _saving;
    
    private void Awake()
    {
      root.SetActive(false);
      SaveManager.Instance.onBeforeSave.AddListener(OnBeforeSave);
      SaveManager.Instance.onAfterSave.AddListener(OnAfterSave);
    }

    private void OnAfterSave()
    {
      StartCoroutine(DelayHide());
    }

    private void OnBeforeSave()
    {
      if (_saving) return;
      _saving = true;
      StopAllCoroutines();
      root.SetActive(true);
      _lastShowTime = Time.time;
    }

    private IEnumerator DelayHide()
    {
      var time = minDisplayTime - (Time.time - _lastShowTime);
      if (time > 0)
      {
        yield return new WaitForSeconds(time);
      }
      root.SetActive(false);
      _saving = false;
    }

    private void OnDestroy()
    {
      if (Application.isEditor) return;
      SaveManager.Instance.onBeforeSave.RemoveListener(OnBeforeSave);
      SaveManager.Instance.onAfterSave.RemoveListener(OnAfterSave);
    }
  }
}