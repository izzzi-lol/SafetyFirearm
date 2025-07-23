using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using LabApi.Events.Arguments.ServerEvents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSettings.ServerSpecific;
using YamlDotNet.Serialization;

namespace SafetyFirearm.Events
{
    internal sealed class ShotEvent
    {
        public void Shooting(ShootingEventArgs e)
        {
            if (e.Item is Firearm firearm)
            {
                var lang = SafetyFirearm.Instance?.Lang as Lang;
                if (SafetyFirearm.SafetyModeList[firearm.Serial])
                {
                    e.Player.ShowHint(lang.FuseIsActive, 3);
                    e.IsAllowed = false;
                }
            }
        }

        public void ChangingItems(ChangingItemEventArgs e)
        {
            if (e.Item is Firearm firearm)
            {
                var lang = SafetyFirearm.Instance?.Lang as Lang;
                Exiled.API.Features.Player user = e.Player;
                if (firearm.FirearmType.Equals(FirearmType.Revolver)) return;
                if (!SafetyFirearm.SafetyModeList.ContainsKey(firearm.Serial))
                {
                    SSTwoButtonsSetting safemodeselect = ServerSpecificSettingsSync.GetSettingOfUser<SSTwoButtonsSetting>(user.ReferenceHub, 201);
                    SafetyFirearm.SafetyModeList.Add(firearm.Serial, safemodeselect.SyncIsA);
                }
                if (SafetyFirearm.SafetyModeList[firearm.Serial])
                    user.ShowHint(lang.FuseSwitchOn, 2);
                else user.ShowHint(lang.FuseSwitchOff, 2);
            }
        }

        public void RoundEnd()
        {
            Log.Info($"Clearing {SafetyFirearm.SafetyModeList.Count} entries...");
            SafetyFirearm.SafetyModeList.Clear();
            return;
        }
    }
}
