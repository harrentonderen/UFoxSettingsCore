using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DM;
using Landfall.TABS;
using Landfall.TABS.UnitEditor;
using UnityEngine;
using BepInEx;
using BepInEx.Configuration;

namespace UFoxSettingsCore
{
    public static class UFoxSettingsUtility
    {
        public static ConfigEntry<bool> BindBepinexConfig(BaseUnityPlugin modPluginClass, CATEGORY category, string configEntryName, bool defaultBoolValue, string description)
        {
            string categoryType = "Gameplay";
            switch (category)
            {
                case CATEGORY.BUG:
                    categoryType = "Bug";
                    break;
                case CATEGORY.VIDEO:
                    categoryType = "Video";
                    break;
                case CATEGORY.AUDIO:
                    categoryType = "Audio";
                    break;
                case CATEGORY.CONTROLS:
                    categoryType = "Controls";
                    break;
                case CATEGORY.TWITCH:
                    categoryType = "Twitch";
                    break;
                default: break;
            }

            var newEntry = modPluginClass.Config.Bind<bool>(categoryType, configEntryName, defaultBoolValue, description);
            return newEntry;
        }

        public static SettingsInstance CreateSetting(UFoxSettingInstance _settingInstance, ConfigEntry<bool> _config)
        {
            return UFoxSettingsUtility.CreateSettingInternal
                (_settingInstance.m_doLocalizeName, 
                _settingInstance.m_buttonType, 
                _settingInstance.m_category, 
                _settingInstance.m_key,
                _settingInstance.m_tooltip, 
                _settingInstance.m_defaultValue, 
                (float)(_config.Value ? 0 : 1), 
                _settingInstance.m_options, _settingInstance.m_min,
                _settingInstance.m_max);
        }

        private static SettingsInstance CreateSettingInternal(LOCALIZENAME doLocalizeName, SettingsInstance.SettingsType type, CATEGORY category, string key, string tooltip, float defaultValue, float currentValue, string[] options = null, float min = 0f, float max = 1f)
        {
            SettingsInstance settingsInstance = new SettingsInstance
            {
                localizeOptions = (doLocalizeName == LOCALIZENAME.DO ? true : false),
                settingsType = type,
                m_settingsKey = key,
                toolTip = tooltip,
                options = options,
                defaultValue = (int)defaultValue,
                currentValue = (int)currentValue,
                defaultSliderValue = defaultValue,
                currentSliderValue = currentValue,
                min = min,
                max = max
            };

            GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
            SettingsInstance[] array = new SettingsInstance[0];

            switch (category)
            {
                case CATEGORY.BUG:
                    array = service.BugsSettings;
                    break;
                case CATEGORY.VIDEO:
                    array = service.VideoSettings;
                    break;
                case CATEGORY.AUDIO:
                    array = service.AudioSettings;
                    break;
                case CATEGORY.CONTROLS:
                    array = service.ControlSettings;
                    break;
                case CATEGORY.GAMEPLAY:
                    array = service.GameplaySettings;
                    break;
                case CATEGORY.TWITCH:
                    array = service.GameplaySettings;
                    break;
                default: break;
            }

            List<SettingsInstance> list = array.ToList<SettingsInstance>();
            list.Add(settingsInstance);

            switch (category)
            {
                case CATEGORY.BUG:
                    typeof(GlobalSettingsHandler).GetField("m_bugsSettings", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(service, list.ToArray());
                    break;
                case CATEGORY.VIDEO:
                    typeof(GlobalSettingsHandler).GetField("m_videoSettings", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(service, list.ToArray());
                    break;
                case CATEGORY.AUDIO:
                    typeof(GlobalSettingsHandler).GetField("m_audioSettings", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(service, list.ToArray());
                    break;
                case CATEGORY.CONTROLS:
                    typeof(GlobalSettingsHandler).GetField("m_controlSettings", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(service, list.ToArray());
                    break;
                case CATEGORY.GAMEPLAY:
                    typeof(GlobalSettingsHandler).GetField("m_gameplaySettings", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(service, list.ToArray());
                    break;
                case CATEGORY.TWITCH:
                    typeof(GlobalSettingsHandler).GetField("m_gameplaySettings", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(service, list.ToArray());
                    break;
                default: break;
            }

            return settingsInstance;
        }
    }
}
