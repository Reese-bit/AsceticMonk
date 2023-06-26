using System;
#if STEAMWORKS_NET
using Steamworks;
#endif

namespace Mingo.Base.Runtime.Utils
{
  public class SteamUtils
  {
    public static string GetAppInstallDir() {
#if STEAMWORKS_NET
      string appInstallDir = null;
      SteamApps.GetAppInstallDir(new AppId_t(480), out appInstallDir, 1024);
      return appInstallDir;
#else
      throw new InvalidOperationException("Please install steamworks.net: http://steamworks.github.io/");
#endif
    }
  }
}