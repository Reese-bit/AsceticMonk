using UnityEngine.Windows;

namespace Mingo.Saves.Runtime.Savers
{
  public class FileSaver : ISaveFileSaver
  {
    public bool Exists(string filePath)
    {
      return File.Exists(filePath);
    }

    public byte[] Load(string filePath)
    {
      return File.ReadAllBytes(filePath);
    }

    public void Save(string filePath, byte[] bytes)
    {
      File.WriteAllBytes(filePath, bytes);
    }
  }
}