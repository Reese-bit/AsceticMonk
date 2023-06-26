using System;
using System.Collections.Generic;
using Mingo.Base.Runtime.DataStruct;

namespace Mingo.Saves.Runtime
{
  [Serializable]
  public class SavePreferences
  {
    public SerializableDictionaryBoolValue bools = new SerializableDictionaryBoolValue();
    public SerializableDictionaryIntValue ints = new SerializableDictionaryIntValue();
    public SerializableDictionaryStringValue strings = new SerializableDictionaryStringValue();
    public SerializableDictionaryFloatValue floats = new SerializableDictionaryFloatValue();
  }
}