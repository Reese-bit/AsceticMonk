using System;
using Mingo.Base.Runtime.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mingo.Saves.Runtime.Misc
{
  public class SavePlayerPositionPlugin : SavePlugin
  {
    public override string PluginId => "Mingo.SavePlayerPosition";

    [TagSelector]
    public string playerTag = "Player";
    public Vector2 defaultPlayerPosition;

    protected override void OnBeforeSave(SaveFile file)
    {
      var player = GameObject.FindWithTag(playerTag.IsNullOrWhitespace() ? "Player" : playerTag);
      var position = player.transform.position;
      file.playerPosition = new SaveFile.PlayerPosition((Vector2) position);
    }

    protected override void OnAfterSave(SaveFile file)
    {
    }

    protected override void OnSaveLoad(SaveFile file)
    {
      if (file.playerPosition == null)
      {
        file.playerPosition = new SaveFile.PlayerPosition(defaultPlayerPosition);
      }
    }

    protected override void OnSceneLoad(SaveFile file, Scene scene)
    {
      var player = GameObject.FindWithTag(playerTag.IsNullOrWhitespace() ? "Player" : playerTag);
      var pos = SaveManager.Instance.Current.playerPosition;
      player.transform.position = new Vector3(pos.x, pos.y);
    }
  }
}