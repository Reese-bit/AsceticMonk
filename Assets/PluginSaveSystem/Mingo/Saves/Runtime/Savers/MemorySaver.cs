namespace Mingo.Saves.Runtime.Savers
{
  public class MemorySaver : ISaveFileSaver
  {
    public bool Exists(string fileName)
    {
      return true;
    }

    public byte[] Load(string fileName)
    {
      return null;
    }

    public void Save(string fileName, byte[] bytes)
    {
      
    }
  }
}