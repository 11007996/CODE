# PowerShell script to extract TypeKey from algorithm DLLs
$ErrorActionPreference = "SilentlyContinue"

$pluginDir = "plugins"
$dlls = Get-ChildItem -Path $pluginDir -Filter "LuxVideoDet.Algorithm.*.dll"
$results = @()

# Use reflection-only context
$domain = [System.AppDomain]::CurrentDomain
$asmName = New-Object System.Reflection.AssemblyName("DynamicAssembly")
$asmBuilder = $domain.DefineDynamicAssembly($asmName, [System.Reflection.Emit.AssemblyBuilderAccess]::Run)

foreach ($dll in $dlls) {
    try {
        # Try loading with metadata only
        $rawBytes = [System.IO.File]::ReadAllBytes($dll.FullName)
        $asm = [System.Reflection.Assembly]::Load($rawBytes)
        
        $descriptorType = $null
        foreach ($type in $asm.GetTypes()) {
            if ($type.IsClass -and -not $type.IsAbstract) {
                if ($type.GetInterface("IAlgorithmDescriptor") -ne $null) {
                    $descriptorType = $type
                    break
                }
            }
        }
        
        if ($descriptorType) {
            # Get TypeKey property without instantiating
            $typeKeyProp = $descriptorType.GetProperty("TypeKey")
            if ($typeKeyProp -and $typeKeyProp.CanRead) {
                # Create instance using GetMethod
                $getter = $typeKeyProp.GetGetMethod()
                
                # Try to create instance via Activator
                $instance = [System.Activator]::CreateInstance($descriptorType)
                $typeKey = $instance.TypeKey
                
                $results += [PSCustomObject]@{
                    PluginName = $dll.BaseName
                    TypeKey = $typeKey
                    DescriptorClass = $descriptorType.Name
                }
            }
        } else {
            $results += [PSCustomObject]@{
                PluginName = $dll.BaseName
                TypeKey = "NOT_FOUND"
                DescriptorClass = "No descriptor"
            }
        }
    }
    catch {
        $results += [PSCustomObject]@{
            PluginName = $dll.BaseName
            TypeKey = "ERROR"
            DescriptorClass = $_.Exception.Message.Substring(0, [Math]::Min(60, $_.Exception.Message.Length))
        }
    }
}

$results | Format-Table -AutoSize