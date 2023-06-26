using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mingo.Base.Runtime;
using Mingo.Base.Runtime.Extensions;
using Mingo.Base.Runtime.Utils;
using Mingo.Saves.Runtime.Savers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Mingo.Saves.Runtime
{
  public class SaveManager : PersistentSingleton<SaveManager>
  {
    public SaveSettings settings;

    [Disable]
    public List<SaveFile> saves;
    [Disable]
    public int index;

    public UnityEvent onLoad;
    public UnityEvent<Scene> onSceneLoad;
    public UnityEvent onBeforeSave;
    public UnityEvent onAfterSave;

    public SaveFile Current => saves[index];
    private ISaveFileSaver _saver;
    private Log LOG;

    protected override void Setup()
    {
      base.Setup();
      
      LOG = Log.Get(this);
      
      foreach (var savePlugin in GetComponents<SavePlugin>())
      {
        savePlugin.InitListeners();
      }

      
      if (settings.target == ESaveTarget.Memory)
      {
        _saver = new MemorySaver();
      }
      else
      {
        _saver = new FileSaver();
      }
      
      saves = new List<SaveFile>();
      
      for (var i = 0; i < settings.maxSaves; i++)
      {
        var fileName = settings.fileName.Replace("{index}", i.ToString());
        var folder = GetAbsoluteSaveFolder();
        var filePath = Path.Combine(folder, fileName);
        var saveFile = new SaveFile();
        saveFile.index = i;
        saveFile.time = TimeUtils.NowMillis();
        saveFile.FillDefaults();
        if (_saver.Exists(filePath))
        {
          var bytes = _saver.Load(filePath);
          LoadSaveFile(saveFile, bytes);
          saveFile.exists = true;
        }
        saves.Add(saveFile);
      }
      
      LOG.D($"load {saves.Count} saves");
    }

    public string GetAbsoluteSaveFolder()
    {
      switch (settings.target)
      {
        case ESaveTarget.Memory: return $"{settings.folder}";
        case ESaveTarget.Editor: return Path.Combine(Application.dataPath, $"MingoEditorSaves/{settings.folder}");
        case ESaveTarget.Production: return Path.Combine(Application.persistentDataPath, settings.folder);
        case ESaveTarget.Steam: return SteamUtils.GetAppInstallDir();
        default: throw new InvalidOperationException($"Invalid target: {settings.target}");
      }
    }

    public void Save()
    {
      SaveTo(index);
    }

    public void SaveTo(int targetIndex)
    {
      Current.time = TimeUtils.NowMillis();
      Current.exists = true;
      var fileName = settings.fileName.Replace("{index}", targetIndex.ToString());
      var folder = GetAbsoluteSaveFolder();

      if (!Directory.Exists(folder))
      {
        Directory.CreateDirectory(folder);
      }
      var filePath = Path.Combine(folder, fileName);
      
      onBeforeSave.Invoke();
      
      var json = JsonUtility.ToJson(Current, prettyPrint: true);
      var bytes = Encoding.UTF8.GetBytes(json);
      if (!settings.password.IsNullOrWhitespace())
      {
        bytes = CipherUtils.Encrypt(bytes, settings.password);
      }
      LOG.D($"save {index} to {filePath}, {bytes.Length} bytes");
      _saver.Save(filePath, bytes);
      
      onAfterSave.Invoke();
    }

    public void Delete(SaveFile saveFile)
    {
      saveFile.exists = false;
      ResetSaveItem(saveFile);
      throw new InvalidOperationException();
    }

    public void Load(int index)
    {
      LOG.D($"load save: {index}");
      this.index = index;
      onLoad.Invoke();
    }

    private void ResetSaveItem(SaveFile saveItem)
    {
      saveItem.time = 0;
      saveItem.title = "";
      saveItem.desc = "";
      saveItem.cover = "";
      saveItem.preferences = new SavePreferences();
    }
    
    private void LoadSaveFile(SaveFile saveFile, byte[] bytes)
    {
      string json;
      if (settings.password.IsNullOrWhitespace())
      {
        json = Encoding.UTF8.GetString(bytes);
      }
      else
      {
        json = CipherUtils.Decrypt(bytes, settings.password);
      }
      if (settings.target == ESaveTarget.Memory) return;
      JsonUtility.FromJsonOverwrite(json, saveFile);
    }
    
  }
}