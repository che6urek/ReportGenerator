using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace ReportGenerator
{
    public static class TemplateService
    {
        /// <summary>
        /// Gets all templates from the resource file
        /// </summary>
        /// <returns></returns>
        public static Template[] GetTemplates(Type resource)
        {
            var templates = new List<Template>();
            var resourceManager = new ResourceManager(resource);
            var resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            foreach (DictionaryEntry entry in resourceSet)
            {
                templates.Add(new Template { Name = entry.Key.ToString(), Format = entry.Value.ToString() });
            }

            return templates.ToArray();
        }
    }
}
