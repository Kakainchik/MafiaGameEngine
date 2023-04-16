using System;
using System.ComponentModel;
using System.Resources;

namespace WPFApplication.Extensions
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public sealed class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private ResourceManager resourceManager;

        private readonly string resourceKey;

        public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
        {
            resourceManager = new ResourceManager(resourceType);
            this.resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                string? description = resourceManager.GetString(resourceKey);
                return string.IsNullOrWhiteSpace(description) ? $"[[{resourceKey}]]" : description;
            }
        }

        public RoleLocilize Locilize { get; set; }
    }

    public enum RoleLocilize : byte
    {
        NAME,
        DESCRIPTION,
        ABILITY
    }
}