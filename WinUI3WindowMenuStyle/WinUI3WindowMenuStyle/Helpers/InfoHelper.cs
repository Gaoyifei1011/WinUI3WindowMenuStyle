using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store.Preview;
using Windows.Storage;
using Windows.System.Profile;

namespace GetStoreApp.Helpers.Root
{
    /// <summary>
    /// 系统版本和应用版本信息辅助类
    /// </summary>
    public static class InfoHelper
    {
        // 系统版本信息
        public static Version SystemVersion { get; }

        static InfoHelper()
        {
            string systemVersion = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            ulong version = ulong.Parse(systemVersion);
            SystemVersion = new Version((int)((version & 0xFFFF000000000000L) >> 48), (int)((version & 0x0000FFFF00000000L) >> 32), (int)((version & 0x00000000FFFF0000L) >> 16), (int)(version & 0x000000000000FFFFL));
        }
    }
}