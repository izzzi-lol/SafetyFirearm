using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using YamlDotNet.Serialization;

namespace SafetyFirearm
{
    public class Lang : ITranslation
    {
        [Description("Keybind Settings")]
        [YamlMember(Alias = "change_mode_keybind_text")]
        public string ChangeModeKeybind { get; set; } = "Change mode keybind";
        [YamlMember(Alias = "fuse_by_default_text")]
        public string FuseByDefault { get; set; } = "Safe mode by default";
        [YamlMember(Alias = "fuse_by_default_on")]
        public string FuseByDefaultOn { get; set; } = "On";
        [YamlMember(Alias = "fuse_by_default_off")]
        public string FuseByDefaultOff { get; set; } = "Off";
        [YamlMember(Alias = "fuse_is_active")]
        public string FuseIsActive { get; set; } = "<color=#FF0000>the trigger wont pull...<color=#FF0000>";
        [YamlMember(Alias = "fuse_switch_off")]
        public string FuseSwitchOff { get; set; } = "Fuse removed";
        [YamlMember(Alias = "fuse_switch_on")]
        public string FuseSwitchOn { get; set; } = "Fuse installed";
        [YamlMember(Alias = "fuse_on_pickup")]
        public string FuseOnPickup { get; set; } = "The firearm is on safety";
        [YamlMember(Alias = "fuse_off_pickup")]
        public string FuseOffPickup { get; set; } = "The firearm is off-fuse";
        [YamlMember(Alias = "not_have_fuse")]
        public string NotHaveFuse { get; set; } = "This firearm does not have a safety";
    }
}
