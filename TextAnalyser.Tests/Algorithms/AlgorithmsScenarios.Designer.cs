﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TextAnalyser.Tests.Algorithms {
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
    internal class AlgorithmsScenarios {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AlgorithmsScenarios() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TextAnalyser.Tests.Algorithms.AlgorithmsScenarios", typeof(AlgorithmsScenarios).Assembly);
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
        ///
        ///given: &apos;Deberíamos haber hecho más. No lo defiendo, sino que trato de explicarlo.&apos;,
        ///
        ///expected : {
        ///	&quot;Sentences&quot;: [{
        ///			&quot;SentenceNumber&quot;: 1,
        ///			&quot;SentenceText&quot;: &quot;Deberíamos haber hecho más&quot;,
        ///			&quot;WordsCount&quot;: 4,
        ///			&quot;Words&quot;: [{
        ///					&quot;WordNumber&quot;: 1,
        ///					&quot;WordText&quot;: &quot;Deberíamos&quot;,
        ///					&quot;LettersCount&quot;: 10
        ///				}, {
        ///					&quot;WordNumber&quot;: 2,
        ///					&quot;WordText&quot;: &quot;haber&quot;,
        ///					&quot;LettersCount&quot;: 5
        ///				}, {
        ///					&quot;WordNumber&quot;: 3,
        ///					&quot;WordText&quot;: &quot;hecho&quot;,
        ///					&quot;LettersCount&quot;: 5
        ///				}, {
        ///					&quot;WordNumber&quot;: [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Algorithm_Scenario_001 {
            get {
                return ResourceManager.GetString("Algorithm_Scenario_001", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///	given: &apos;Málaga is a municipality, capital of the Province of Málaga, in the Autonomous Community of Andalusia, Spain. With a population of 569,130 in 2015,[1] it is the second-most populous city of Andalusia and the sixth-largest in Spain. The southernmost large city in Europe, it lies on the Costa del Sol (Coast of the Sun) of the Mediterranean, about 100 kilometres (62.14 miles) east of the Strait of Gibraltar and about 130 km (80.78 mi) north of Africa.Málagas history spans about 2,800 years, making  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Algorithm_Scenario_002 {
            get {
                return ResourceManager.GetString("Algorithm_Scenario_002", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///    given: &apos;Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam ac felis ac sapien imperdiet tempus. Praesent nisl sem, gravida sit amet pretium eu, commodo vitae eros. Curabitur vitae volutpat diam. Vestibulum id laoreet justo. Vivamus in tellus vel metus sodales varius. Nunc maximus nibh in orci dictum, nec molestie tortor posuere. Donec at ligula ac lacus euismod volutpat. Etiam ullamcorper in ligula non auctor. Sed eros risus, molestie eu ligula non, dapibus scelerisque purus. Sed quis mas [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Algorithm_Scenario_003 {
            get {
                return ResourceManager.GetString("Algorithm_Scenario_003", resourceCulture);
            }
        }
    }
}