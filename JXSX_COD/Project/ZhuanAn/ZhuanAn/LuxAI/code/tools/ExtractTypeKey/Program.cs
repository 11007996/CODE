// Extract TypeKey from algorithm DLLs using MetadataReader
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

class Program
{
    static void Main(string[] args)
    {
        var pluginDir = args.Length > 0 ? args[0] : "plugins";
        var dlls = Directory.GetFiles(pluginDir, "LuxVideoDet.Algorithm.*.dll");
        
        Console.WriteLine("PluginName                                  | TypeKey            | DescriptorClass");
        Console.WriteLine(new string('-', 95));
        
        foreach (var dll in dlls)
        {
            try
            {
                using var stream = File.OpenRead(dll);
                var provider = MetadataReaderProvider.FromStream(stream);
                var reader = provider.GetMetadataReader();
                
                string typeKey = "NOT_FOUND";
                string descriptorClass = "Unknown";
                
                foreach (var typeHandle in reader.TypeDefinitions)
                {
                    var typeDef = reader.GetTypeDefinition(typeHandle);
                    var namespaceName = reader.GetString(typeDef.Namespace);
                    var typeName = reader.GetString(typeDef.Name);
                    
                    // Check if class implements IAlgorithmDescriptor
                    foreach (var iface in typeDef.GetInterfaceImplementations())
                    {
                        var ifaceDef = reader.GetInterfaceImplementation(iface);
                        var ifaceType = ifaceDef.Interface;
                        
                        if (ifaceType.IsTypeReference)
                        {
                            var ifaceRef = reader.GetTypeReference((TypeReferenceHandle)ifaceType);
                            var ifaceNamespace = reader.GetString(ifaceRef.Namespace);
                            var ifaceName = reader.GetString(ifaceRef.Name);
                            
                            if (ifaceName == "IAlgorithmDescriptor")
                            {
                                descriptorClass = typeName;
                                
                                // Get property TypeKey
                                foreach (var propHandle in typeDef.GetProperties())
                                {
                                    var prop = reader.GetProperty(propHandle);
                                    var propName = reader.GetString(prop.Name);
                                    
                                    if (propName == "TypeKey")
                                    {
                                        // TypeKey is a string property - try to find the default value
                                        typeKey = $"[Property found in {typeName}]";
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                
                var baseName = Path.GetFileNameWithoutExtension(dll);
                Console.WriteLine($"{baseName,-46} | {typeKey,-18} | {descriptorClass}");
            }
            catch (Exception ex)
            {
                var baseName = Path.GetFileNameWithoutExtension(dll);
                Console.WriteLine($"{baseName,-46} | ERROR           | {ex.Message.Substring(0, Math.Min(30, ex.Message.Length))}");
            }
        }
    }
}