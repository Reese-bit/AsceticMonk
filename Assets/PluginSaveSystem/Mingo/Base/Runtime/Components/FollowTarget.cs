using System;
using UnityEngine;

namespace Mingo.Base.Runtime.Components
{
  public class FollowTarget : MonoBehaviour
  {
    [TagSelector]
    public string targetTag;
    public Transform target;

    private void Awake()
    {
      if (target == null)
      {
        target = GameObject.FindWithTag(targetTag).transform;
      }
    }

    private void Update()
    {
      var pos = target.transform.position;
      var trans = transform;
      trans.position = new Vector3(pos.x, pos.y, trans.position.z);
    }
  }
}