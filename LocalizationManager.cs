using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Windows.Forms;

namespace AudioConverter
{
    public static class LocalizationManager
    {
        private static Dictionary<string, string> _localizedStrings = new Dictionary<string, string>();
        private static string _currentLanguage = "es";

        public static string CurrentLanguage
        {
            get => _currentLanguage;
            set
            {
                if (_currentLanguage != value)
                {
                    _currentLanguage = value;
                    LoadLanguage(_currentLanguage);
                }
            }
        }

        static LocalizationManager()
        {
            LoadLanguage(_currentLanguage);
        }

        private static void LoadLanguage(string languageCode)
        {
            _localizedStrings = new Dictionary<string, string>();
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = $"AudioConverter.lang.{languageCode}.json";

            using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    Helpers.Logger.Log($"Error: Embedded language resource '{resourceName}' not found. Using default texts.", true);
                    if (languageCode != "en")
                    {
                        LoadLanguage("en");
                    }
                    return;
                }

                try
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string jsonString = reader.ReadToEnd();
                        var jsonDocument = JsonDocument.Parse(jsonString);
                        foreach (JsonProperty property in jsonDocument.RootElement.EnumerateObject())
                        {
                            _localizedStrings[property.Name] = property.Value.GetString() ?? property.Name;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helpers.Logger.Log($"Error al cargar el recurso de idioma incrustado '{resourceName}': {ex.Message}", true);
                }
            }
        }

        public static string GetString(string key)
        {
            if (_localizedStrings.TryGetValue(key, out string? value))
            {
                return value ?? key;
            }
            return key;
        }

        public static void ApplyLocalization(Control control)
        {
            if (control == null) return;

            string? tag = control.Tag as string;
            if (!string.IsNullOrEmpty(tag))
            {
                string localizedText = GetString(tag);
                if (!string.IsNullOrEmpty(localizedText))
                {
                    control.Text = localizedText;
                }
            }

            foreach (Control childControl in control.Controls)
            {
                ApplyLocalization(childControl);
            }
        }
    }
}
