﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NextCallerApiTest.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NextCallerApiTest.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to {
        ///    &quot;error&quot;: {
        ///        &quot;message&quot;: &quot;There are validation errors:&quot;, 
        ///        &quot;code&quot;: &quot;1054&quot;, 
        ///        &quot;type&quot;: &quot;Validation&quot;, 
        ///        &quot;description&quot;: {
        ///            &quot;email&quot;: [
        ///                &quot;Invalid email address&quot;
        ///            ]
        ///        }
        ///    }
        ///}.
        /// </summary>
        internal static string JsonError {
            get {
                return ResourceManager.GetString("JsonError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;spoofed&quot;: &quot;false&quot;,
        ///  &quot;fraud_risk&quot;: &quot;low&quot;
        ///}.
        /// </summary>
        internal static string JsonFraudLevel {
            get {
                return ResourceManager.GetString("JsonFraudLevel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;data&quot;: [
        ///    {
        ///      &quot;id&quot;: &quot;test_platform_username1&quot;,
        ///      &quot;first_name&quot;: &quot;John&quot;,
        ///      &quot;last_name&quot;: &quot;Doe&quot;,
        ///      &quot;company_name&quot;: &quot;NC*&quot;,
        ///      &quot;email&quot;: &quot;test1@test.com&quot;,
        ///      &quot;number_of_operations&quot;: 5,
        ///      &quot;total_operations&quot;: {
        ///        &quot;2015-07&quot;: 5
        ///      },
        ///      &quot;billed_operations&quot;: {
        ///        &quot;2015-07&quot;: 5
        ///      },
        ///      &quot;object&quot;: &quot;account&quot;,
        ///      &quot;resource_uri&quot;: &quot;/api/v2.1/accounts/test_platform_username1/&quot;
        ///    },
        ///    {
        ///      &quot;id&quot;: &quot;me&quot;,
        ///      &quot;first_name&quot;: &quot;&quot;,
        ///      &quot;last_name [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string JsonPlatformStatistics {
            get {
                return ResourceManager.GetString("JsonPlatformStatistics", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///      &quot;id&quot;: &quot;test_platform_username1&quot;,
        ///      &quot;first_name&quot;: &quot;John&quot;,
        ///      &quot;last_name&quot;: &quot;Doe&quot;,
        ///      &quot;company_name&quot;: &quot;NC*&quot;,
        ///      &quot;email&quot;: &quot;test1@test.com&quot;,
        ///      &quot;number_of_operations&quot;: 5,
        ///      &quot;total_operations&quot;: {
        ///        &quot;2015-07&quot;: 5
        ///      },
        ///      &quot;billed_operations&quot;: {
        ///        &quot;2015-07&quot;: 5
        ///      },
        ///      &quot;object&quot;: &quot;account&quot;,
        ///      &quot;resource_uri&quot;: &quot;/api/v2.1/accounts/test_platform_username1/&quot;
        ///    }.
        /// </summary>
        internal static string JsonPlatformUser {
            get {
                return ResourceManager.GetString("JsonPlatformUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;id&quot;: &quot;97d949a413f4ea8b85e9586e1f2d9a&quot;,
        ///  &quot;first_name&quot;: &quot;Jerry&quot;,
        ///  &quot;first_pronounced&quot;: &quot;JER-ee&quot;,
        ///  &quot;middle_name&quot;: &quot;Allen&quot;,
        ///  &quot;last_name&quot;: &quot;Seinfeld&quot;,
        ///  &quot;name&quot;: &quot;Jerry Allen Seinfeld&quot;,
        ///  &quot;phone&quot;: [
        ///    {
        ///      &quot;number&quot;: &quot;2125558383&quot;,
        ///      &quot;line_type&quot;: &quot;Mobile&quot;,
        ///      &quot;carrier&quot;: &quot;Verizon Wireless&quot;,
        ///      &quot;resource_uri&quot;: &quot;/v2.1/records/2125558383/&quot;
        ///    }
        ///  ],
        ///  &quot;address&quot;: [
        ///    {
        ///      &quot;city&quot;: &quot;New York&quot;,
        ///      &quot;extended_zip&quot;: &quot;2344&quot;,
        ///      &quot;country&quot;: &quot;USA&quot;,
        ///      &quot;line1&quot;: &quot;129 West 81st Street&quot;,
        ///      &quot;l [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string JsonProfile {
            get {
                return ResourceManager.GetString("JsonProfile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;records&quot;: [
        ///    {
        ///      &quot;id&quot;: &quot;97d949a413f4ea8b85e9586e1f2d9a&quot;,
        ///      &quot;first_name&quot;: &quot;Jerry&quot;,
        ///      &quot;first_pronounced&quot;: &quot;JER-ee&quot;,
        ///      &quot;middle_name&quot;: &quot;Allen&quot;,
        ///      &quot;last_name&quot;: &quot;Seinfeld&quot;,
        ///      &quot;name&quot;: &quot;Jerry Allen Seinfeld&quot;,
        ///      &quot;phone&quot;: [
        ///        {
        ///          &quot;number&quot;: &quot;2125558383&quot;,
        ///          &quot;line_type&quot;: &quot;Mobile&quot;,
        ///          &quot;carrier&quot;: &quot;Verizon Wireless&quot;,
        ///          &quot;resource_uri&quot;: &quot;/v2.1/records/2125558383/&quot;
        ///        }
        ///      ],
        ///      &quot;address&quot;: [
        ///        {
        ///          &quot;city&quot;: &quot;New York&quot;,
        ///          &quot;e [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string JsonProfiles {
            get {
                return ResourceManager.GetString("JsonProfiles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;records&gt;
        ///    &lt;list-item&gt;
        ///        &lt;id&gt;97d949a413f4ea8b85e9586e1f2d9a&lt;/id&gt;
        ///        &lt;first_name&gt;Jerry&lt;/first_name&gt;
        ///        &lt;first_pronounced&gt;JER-ee&lt;/first_pronounced&gt;
        ///        &lt;middle_name&gt;Allen&lt;/middle_name&gt;
        ///        &lt;last_name&gt;Seinfeld&lt;/last_name&gt;
        ///        &lt;name&gt;Jerry Allen Seinfeld&lt;/name&gt;
        ///        &lt;phone&gt;
        ///            &lt;list-item&gt;
        ///                &lt;number&gt;2125558383&lt;/number&gt;
        ///                &lt;line_type&gt;Mobile&lt;/line_type&gt;
        ///                &lt;carrier&gt;Verizon Wireless&lt;/carrier&gt;
        ///                &lt;resource_uri [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string XmlResponse {
            get {
                return ResourceManager.GetString("XmlResponse", resourceCulture);
            }
        }
    }
}
