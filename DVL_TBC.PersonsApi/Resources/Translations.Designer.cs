﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DVL_TBC.PersonsApi.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Translations {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Translations() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DVL_TBC.PersonsApi.Resources.Translations", typeof(Translations).Assembly);
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
        ///   Looks up a localized string similar to All letters should be latin or georgian characters..
        /// </summary>
        public static string ErrorAllLettersGeoOrLatin {
            get {
                return ResourceManager.GetString("ErrorAllLettersGeoOrLatin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BirthDate should be typeof DateTime.
        /// </summary>
        public static string ErrorBirthDateDateTime {
            get {
                return ResourceManager.GetString("ErrorBirthDateDateTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Directory not found. Path: {0}.
        /// </summary>
        public static string ErrorDirectoryNotFound {
            get {
                return ResourceManager.GetString("ErrorDirectoryNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File content type is not valid.
        /// </summary>
        public static string ErrorFileContentTypeNotValid {
            get {
                return ResourceManager.GetString("ErrorFileContentTypeNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Minimum age should be {0}.
        /// </summary>
        public static string ErrorMinAgeShouldBe {
            get {
                return ResourceManager.GetString("ErrorMinAgeShouldBe", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter &apos;{0}&apos; length should be in range [{1},{2]].
        /// </summary>
        public static string ErrorParameterLengthRange {
            get {
                return ResourceManager.GetString("ErrorParameterLengthRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Phone number length should be between 4 and 50.
        /// </summary>
        public static string ErrorPhoneNumberRange {
            get {
                return ResourceManager.GetString("ErrorPhoneNumberRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Private number length should be equal to {0}.
        /// </summary>
        public static string ErrorPrivateNumberLength {
            get {
                return ResourceManager.GetString("ErrorPrivateNumberLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Private number should contain only digits.
        /// </summary>
        public static string ErrorPrivateNumberOnlyDigits {
            get {
                return ResourceManager.GetString("ErrorPrivateNumberOnlyDigits", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Private number should be a string.
        /// </summary>
        public static string ErrorPrivateNumberString {
            get {
                return ResourceManager.GetString("ErrorPrivateNumberString", resourceCulture);
            }
        }
    }
}
