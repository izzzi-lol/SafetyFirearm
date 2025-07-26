using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Features;
using Exiled.API.Interfaces;

namespace SafetyFirearm
{
    public class Cfg : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; }
        [Description("Change keybind code if it conflicts with other plugins")]
        public int KeybindCode { get; set; } = 200;
        [Description("Change two buttons code if it conflicts with other plugins")]
        public int FuseByDefaultCode { get; set; } = 201;
    }
}
