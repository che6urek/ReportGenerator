﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ReportGenerator.Resources {
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [DebuggerNonUserCode()]
    [CompilerGenerated()]
    internal class ReportTemplates {
        
        private static ResourceManager resourceMan;
        
        private static CultureInfo resourceCulture;
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ReportTemplates() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager {
            get {
                if (ReferenceEquals(resourceMan, null)) {
                    ResourceManager temp = new ResourceManager("ReportGenerator.Resources.ReportTemplates", typeof(ReportTemplates).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на {{
        /// &quot;date&quot;: &quot;{date}&quot;
        ///  &quot;students&quot;: [  {students}
        ///  ]
        ///}}
        ///.
        /// </summary>
        internal static string Json {
            get {
                return ResourceManager.GetString("Json", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Report generation time: {date}
        ///{students}.
        /// </summary>
        internal static string Text {
            get {
                return ResourceManager.GetString("Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на &lt;GenerationDate&gt;{date}&lt;/GenerationDate&gt;
        ///&lt;Students&gt;
        ///{students}&lt;/Students&gt;.
        /// </summary>
        internal static string Xml {
            get {
                return ResourceManager.GetString("Xml", resourceCulture);
            }
        }
    }
}
