﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bagge.Seti.BusinessLogic.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Bagge.Seti.BusinessLogic.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El CUIT ingresado ya existe..
        /// </summary>
        internal static string CUITNotUniqueErrorMessage {
            get {
                return ResourceManager.GetString("CUITNotUniqueErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El empleado cuyo nombre de usuario es {0} no pudo ser encontrado.
        /// </summary>
        internal static string EmployeeByUsernameNotFoundErrorMessage {
            get {
                return ResourceManager.GetString("EmployeeByUsernameNotFoundErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El id no puede ser {0}.
        /// </summary>
        internal static string IdCannotBeDefault {
            get {
                return ResourceManager.GetString("IdCannotBeDefault", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El objeto de tipo {0} no puede ser nulo.
        /// </summary>
        internal static string InstanceCannotBeNull {
            get {
                return ResourceManager.GetString("InstanceCannotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El objeto de tipo {1} no puede tener un id igual a {0}.
        /// </summary>
        internal static string InstanceIdCannotBeDefault {
            get {
                return ResourceManager.GetString("InstanceIdCannotBeDefault", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El objeto de tipo {1} cuyo id es {0} no pudo ser encontrado.
        /// </summary>
        internal static string InstanceNotFound {
            get {
                return ResourceManager.GetString("InstanceNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hay multiples registros con el mismo nombre..
        /// </summary>
        internal static string MultipleNamesErrorMessage {
            get {
                return ResourceManager.GetString("MultipleNamesErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hay multiples empleados con el mismo nombre de usuario.
        /// </summary>
        internal static string MultipleUsernamesErrorMessage {
            get {
                return ResourceManager.GetString("MultipleUsernamesErrorMessage", resourceCulture);
            }
        }
    }
}
