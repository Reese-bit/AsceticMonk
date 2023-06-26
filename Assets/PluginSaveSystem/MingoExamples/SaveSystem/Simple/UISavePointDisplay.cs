using System;
using Mingo.Saves.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace MingoExamples.SaveSystem.Simple
{
  public class UISavePointDisplay : MonoBehaviour
  {
    public Color active;
    public Color normal;

    private Image _image;
    private SavePoint _savePoint;

    private void Awake()
    {
      _image = GetComponent<Image>();
      _savePoint = GetComponent<SavePoint>();
    }

    private void Update()
    {
      var color = SaveManager.Instance.Current.savePoint == _savePoint.id ? active : normal;
      _image.color = color;
    }
  }
}