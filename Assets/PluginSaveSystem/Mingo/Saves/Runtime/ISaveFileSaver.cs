namespace Mingo.Saves.Runtime
{
  public interface ISaveFileSaver
  {
    public bool Exists(string filePath);
    public byte[] Load(string filePath);
    public void Save(string filePath, byte[] bytes);
  }
}