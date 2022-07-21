using System;
namespace Panda.Tools.Attributes.Setting
{
    public class SettingAttribute
    {
        public SettingAttribute()
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class SettingPrefixAttribute: Attribute
    {
        public string Prefix { get; set; }
    }
}

