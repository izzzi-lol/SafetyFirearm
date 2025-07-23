using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using Exiled.API.Features.Items;
using Exiled.API.Interfaces;
using LabApi.Events.Arguments.PlayerEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSettings.ServerSpecific;

namespace SafetyFirearm
{
    public class SSSettings
    {

        internal static IEnumerable<SettingBase> _settings;

        public static void Register()
        {

            var lang = SafetyFirearm.Instance?.Lang as Lang;

            ServerSpecificSettingsSync.ServerOnSettingValueReceived += KeybindRecieved;

            _settings = [
                new HeaderSetting("Safety Firearm"),
                new KeybindSetting(200, lang.ChangeModeKeybind, UnityEngine.KeyCode.B),
                new TwoButtonsSetting(201, lang.FuseByDefault, lang.FuseByDefaultOn,
                lang.FuseByDefaultOff, defaultIsSecond: true)
                ];

            SettingBase.Register(_settings);
        }

        public static void Unregister()
        {
            ServerSpecificSettingsSync.ServerOnSettingValueReceived -= KeybindRecieved;
        }


        public static void KeybindRecieved(ReferenceHub referenceHub, ServerSpecificSettingBase settingBase)
        {
            if (settingBase is not SSKeybindSetting keybindSetting || keybindSetting.SettingId != 200 || !keybindSetting.SyncIsPressed) return;
            if (!Player.TryGet(referenceHub, out Player player)) return;
            if (player.CurrentItem != null && player.IsAlive && player.CurrentItem is Firearm firearm)
            {
                var lang = SafetyFirearm.Instance?.Lang as Lang;
                if (firearm.FirearmType == Exiled.API.Enums.FirearmType.Revolver)
                {
                    player.ShowHint(lang.NotHaveFuse, 2);
                    return;
                }
                if (SafetyFirearm.SafetyModeList[firearm.Serial])
                {
                    SafetyFirearm.SafetyModeList[firearm.Serial] = false;
                    player.ShowHint(lang.FuseSwitchOff, 2);
                }
                else
                {
                    SafetyFirearm.SafetyModeList[firearm.Serial] = true;
                    player.ShowHint(lang.FuseSwitchOn, 2);
                }
            }
        }

    }
}
