using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tools.EventBus {
    // Search though assemblies and find all instances of a type
    /// <summary>
    /// A utility class, PredefinedAssemblyUtil, provides methods to interact with predefined assemblies.
    /// It allows to get all types in the current AppDomain that implement from a specific Interface type.
    /// For more details, <see href="https://docs.unity3d.com/2023.3/Documentation/Manual/ScriptCompileOrderFolders.html">visit Unity Documentation</see>
    /// </summary>
    public static class PredefinedAssemblyUtil {
         enum AssemblyType {
            AssemblyCSharp,
            AssemblyCSharpEditor,
            AssemblyCSharpEditorFirstPass,
            AssemblyCSharpFirstPass,
        }

         // Null bcs if I don't have eg any editor scripts the corresponding assemblies won't even exist
         /// <summary>
         /// Maps the assembly name to the corresponding AssemblyType.
         /// </summary>
         /// <param name="assemblyName">Name of the assembly.</param>
         /// <returns>AssemblyType corresponding to the assembly name, null if no match.</returns>
         static AssemblyType? GetAssemblyType(string assemblyName) {
             return assemblyName switch {
                 "Assembly-CSharp" => AssemblyType.AssemblyCSharp,
                 "Assembly-CSharp-Editor" => AssemblyType.AssemblyCSharpEditor,
                 "Assembly-CSharp-Editor-firstpass" => AssemblyType.AssemblyCSharpEditorFirstPass,
                 "Assembly-CSharp-firstpass" => AssemblyType.AssemblyCSharpFirstPass,
                 _ => null
             };
         }
         
         
         /// <summary>
         /// Method looks through a given assembly and adds types that fulfill a certain interface to the provided collection.
         /// </summary>
         /// <param name="assemblyTypes">Array of Type objects representing all the types in the assembly.</param>
         /// <param name="interfaceType">Type representing the interface to be checked against.</param>
         /// <param name="results">Collection of types where result should be added.</param>
         static void AddTypesFromAssembly(Type[] assemblyTypes, Type interfaceType, ICollection<Type> results) {
             if (assemblyTypes == null) return;
             for (int i = 0; i < assemblyTypes.Length; i++) {
                 Type type = assemblyTypes[i]; // The bellow means we are actually implementing an interface
                 if (type != interfaceType && interfaceType.IsAssignableFrom(type)) {
                     results.Add(type);
                 }
             }
         }
         
         // In our case, we are looking for all the IEvent (PlayerEvent, TestEvent)
         /// <summary>
         /// Gets all Types from all assemblies in the current AppDomain that implement the provided interface type.
         /// </summary>
         /// <param name="interfaceType">Interface type to get all the Types for.</param>
         /// <returns>List of Types implementing the provided interface type.</returns>    
         public static List<Type> GetTypes(Type interfaceType) {
             // And using Assembly type is a feature from the Reflection package
             Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
             Dictionary<AssemblyType, Type[]> assemblyTypes = new Dictionary<AssemblyType, Type[]>();
             List<Type> types = new List<Type>();
             for (int i = 0; i < assemblies.Length; i++) {
                 AssemblyType? assemblyType = GetAssemblyType(assemblies[i].GetName().Name);
                 if (assemblyType != null) {
                     assemblyTypes.Add((AssemblyType) assemblyType, assemblies[i].GetTypes());
                 }
             }
        
             // Why does he have the editor ones if he doesn't use them?
             assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharp, out var assemblyCSharpTypes);
             AddTypesFromAssembly(assemblyCSharpTypes, interfaceType, types);

             assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharpFirstPass, out var assemblyCSharpFirstPassTypes);
             AddTypesFromAssembly(assemblyCSharpFirstPassTypes, interfaceType, types);
        
             return types;
         }
    }
}