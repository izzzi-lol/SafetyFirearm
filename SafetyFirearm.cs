using Exiled.API.Features;
using Exiled.API.Interfaces;
using InventorySystem.Items.Firearms.ShotEvents;
using SafetyFirearm.Events;
using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using static RoundSummary;

namespace SafetyFirearm
{
    public class SafetyFirearm : Plugin<Cfg>
    {

        public static Dictionary<ushort, bool> SafetyModeList = new Dictionary<ushort, bool>();

        private static readonly SafetyFirearm Singleton = new();
        private static Lang _translation;
        public override string Name => "Safety Firearm";
        public override string Author => "izzzi_";
        public override string Prefix => "SafetyFirearm";
        public override Version Version => new Version(0, 0, 1, 0);
        public static SafetyFirearm Instance => Singleton;
        private Events.ShotEvent shotEvent;

        public ITranslation Lang => _translation;

        public override void OnEnabled()
        {
            shotEvent = new Events.ShotEvent();

            Exiled.Events.Handlers.Player.Shooting += shotEvent.Shooting;
            Exiled.Events.Handlers.Player.ChangingItem += shotEvent.ChangingItems;
            Exiled.Events.Handlers.Server.RestartingRound += shotEvent.RoundEnd;

            CreateOrLoadTranslationsFile();

            SSSettings.Register();
            Log.Info("Safety Firearm by izzzi_ ACTIVATED!");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            SSSettings.Unregister();
            Exiled.Events.Handlers.Player.Shooting -= shotEvent.Shooting;
            Exiled.Events.Handlers.Player.ChangingItem -= shotEvent.ChangingItems;
            Exiled.Events.Handlers.Server.RestartingRound -= shotEvent.RoundEnd;
            shotEvent = null;
            base.OnDisabled();
        }
        private void CreateOrLoadTranslationsFile()
        {
            try
            {
                var path = Path.Combine(Paths.Configs, "SafetyFirearm_lang.yml");
                if (!File.Exists(path))
                {
                    var defText = @"
#//SafetyFirarm by izzzi_lol LANG FILE
#//Tip: use \n for newline

# Server Specs
change_mode_keybind_text: 'Change mode keybind' # Text for keybind setting
fuse_by_default_text: 'Fuse by default' # Text for fuse by default switch
fuse_by_default_on: 'ON'
fuse_by_default_off: 'OFF'

# Display
fuse_is_active: '<color=#FF0000>the trigger wont pull...<color=#FF0000>' # Appears when firearm is on safety
fuse_switch_off: 'Fuse removed' # Appears when fuse removed
fuse_switch_on: 'Fuse installed' # Appears when fuse installed
fuse_on_pickup: 'The firearm is on safety' # Apperas when picking up firearm in hands when fuse is on
fuse_off_pickup: 'The firearm is off-fuse' # Apperas when picking up firearm in hands when fuse is off
not_have_fuse: 'This firearm does not have a safety' # Appears when trying to switch the fuse when firearm does not have (ex. Revolver)
";
                    File.WriteAllText(path, defText);
                }

                var yaml = File.ReadAllText(path);
                var ds = new DeserializerBuilder().Build();
                _translation = ds.Deserialize<Lang>(yaml) ?? new Lang();
            }
            catch (Exception ex)
            {
                _translation = new Lang();
            }
        }
    }
}
