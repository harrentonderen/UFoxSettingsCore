using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UFoxSettingsCore
{
    [Serializable]
    public class UFoxSettingInstance
    {
        public CATEGORY m_category;
        public LOCALIZENAME m_doLocalizeName;
        public SettingsInstance.SettingsType m_buttonType;
        public string m_key;
        public string m_tooltip;
        public float m_defaultValue;
        public string[] m_options = null;
        public float m_min = 0f;
        public float m_max = 1f;
    }
}
