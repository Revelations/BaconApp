﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaconBuilder.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BaconBuilder.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE HTML&gt;
        ///&lt;html&gt;
        ///&lt;head&gt;
        ///&lt;link href=&quot;style.css&quot; rel=&quot;stylesheet&quot; /&gt;
        ///&lt;title&gt;&lt;/title&gt;&lt;!-- x = 0 --&gt;&lt;!-- y = 0 --&gt;
        ///&lt;/head&gt;
        ///&lt;body&gt;
        ///&lt;/body&gt;
        ///&lt;/html&gt;.
        /// </summary>
        public static string Blank {
            get {
                return ResourceManager.GetString("Blank", resourceCulture);
            }
        }
        
        public static System.Drawing.Bitmap map {
            get {
                object obj = ResourceManager.GetObject("map", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ftp://Revelations.webhop.org/.
        /// </summary>
        public static string ServerLocation {
            get {
                return ResourceManager.GetString("ServerLocation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE HTML&gt;&lt;html&gt;&lt;head&gt;&lt;link href=&quot;style.css&quot; rel=&quot;stylesheet&quot; /&gt;&lt;title&gt;&lt;/title&gt;&lt;!-- x = 42 --&gt;&lt;!-- y = 13 --&gt;&lt;/head&gt;&lt;body&gt;&lt;p&gt;hello world&lt;/p&gt;&lt;p&gt;hello you&lt;/p&gt;&lt;/body&gt;&lt;/html&gt;.
        /// </summary>
        public static string TestFile {
            get {
                return ResourceManager.GetString("TestFile", resourceCulture);
            }
        }
    }
}
