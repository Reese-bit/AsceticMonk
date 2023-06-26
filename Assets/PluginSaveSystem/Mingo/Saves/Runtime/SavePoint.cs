using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mingo.Saves.Runtime
{
  public class SavePoint : MonoBehaviour
  {
    public static readonly Dictionary<string, SavePoint> Cache = new Dictionary<string, SavePoint>();

    public string id;
    public string title;
    public string desc;
    public string cover;

    private void Awake()
    {
      Debug.Log($"Awake {this}");
      Cache[id] = this;
    }

    public void Save()
    {
      var saveFile = SaveManager.Instance.Current;
      saveFile.scene = gameObject.scene.name;
      saveFile.savePoint = id;
      saveFile.title = title;
      saveFile.desc = desc;
      saveFile.cover = cover;
      SaveManager.Instance.Save();
    }
    
    public virtual void OnLoad() {}
  }
}