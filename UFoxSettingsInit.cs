using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
//using static RootMotion.FinalIK.GenericPoser;

namespace UFoxSettingsCore
{
    [BepInPlugin("terren.UFoxSettingsCore", "UFoxSettingsCore", "1.0.0")]
    public class UFoxSettingsInit : BaseUnityPlugin
    {
        private void Awake()
        {
            base.StartCoroutine(WaitForService());
        }

        private IEnumerator WaitForService()
        {
            yield return new WaitUntil(() => global::UnityEngine.Object.FindObjectOfType<ServiceLocator>() != null);
            yield return new WaitUntil(() => ServiceLocator.GetService<ISaveLoaderService>() != null);
            if (UFoxSettingsInit._addExampleSetting) ExampleSetting();
            yield break;
        }

        void ExampleSetting()
        {
            UFoxSettingsInit.exampleConfigEntry = UFoxSettingsUtility.BindBepinexConfig(this, CATEGORY.GAMEPLAY, "Test Entry", false, "This modded setting does absolutely nothing!");
            UFoxSettingInstance settingsInstance = new UFoxSettingInstance
            {
                m_category = CATEGORY.GAMEPLAY,
                m_doLocalizeName = LOCALIZENAME.DO,
                m_buttonType = SettingsInstance.SettingsType.Options,
                m_key = "UFoxSetting Example Setting",
                m_tooltip = "This modded setting does absolutely nothing!",
                m_defaultValue = 0f,
                m_options = new string[] { "LABEL_DISABLED", "LABEL_ENABLED" },
                m_min = 0f,
                m_max = 1f
            };
            UFoxSettingsUtility.CreateSetting(settingsInstance, UFoxSettingsInit.exampleConfigEntry).OnValueChanged += UFoxSettingsInit.Dummy_OnValueChanged;
        }

        public static void Dummy_OnValueChanged(int value)
        {
            Debug.Log("--- UFOXSETTINGS EXAMPLE: VALUE CHANGED TO " + value.ToString());
        }

        public static ConfigEntry<bool> exampleConfigEntry;

        public static bool _addExampleSetting = false;
    }
}
