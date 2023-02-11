﻿namespace GenApi.WebApi.Helpers;

public class SolutionGenHelper
{
    public static string GenerateSolutionFileContent(string appNamespace)
    {
        string slnContent = @"
Microsoft Visual Studio Solution File, Format Version 12.00
Project(""{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"") = ""{APP-NAMESPACE}"", ""{APP-NAMESPACE}\{APP-NAMESPACE}.csproj"", ""{YOUR-PROJECT-GUID}""
EndProject
Global
    GlobalSection(SolutionConfigurationPlatforms) = preSolution
        Debug|Any CPU = Debug|Any CPU
        Release|Any CPU = Release|Any CPU
    EndGlobalSection
    GlobalSection(ProjectConfigurationPlatforms) = postSolution
        {YOUR-PROJECT-GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
        {YOUR-PROJECT-GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
        {YOUR-PROJECT-GUID}.Release|Any CPU.ActiveCfg = Release|Any CPU
        {YOUR-PROJECT-GUID}.Release|Any CPU.Build.0 = Release|Any CPU
    EndGlobalSection
    GlobalSection(SolutionProperties) = preSolution
        HideSolutionNode = FALSE
    EndGlobalSection
EndGlobal
";
        slnContent = slnContent.Replace("{YOUR-PROJECT-GUID}", Guid.NewGuid().ToString().ToUpper());
        slnContent = slnContent.Replace("{APP-NAMESPACE}", appNamespace);
        
        return slnContent;
    }
    
    public static string GenerateProjectFileContent(int sdkVersion)
    {
        string projectContent = @"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
";
        return projectContent;
    }
    
    public static string GenerateEditorconfigContent()
    {
        string content = @"
# NOTE: Requires **VS2019 16.3** or later

# New Rule Set
# Description:  

# Code files
[*.{cs,vb}]


dotnet_diagnostic.ASP0001.severity = none

dotnet_diagnostic.CA1028.severity = none

dotnet_diagnostic.CA1031.severity = none

dotnet_diagnostic.CA1056.severity = none

dotnet_diagnostic.CA1304.severity = none

dotnet_diagnostic.CA1712.severity = none

dotnet_diagnostic.CA1715.severity = none

dotnet_diagnostic.CA1716.severity = none

dotnet_diagnostic.CA1717.severity = none

dotnet_diagnostic.CA1801.severity = none

dotnet_diagnostic.CA1819.severity = none

dotnet_diagnostic.CA2007.severity = none

dotnet_diagnostic.CS1573.severity = none

dotnet_diagnostic.CS1591.severity = none

dotnet_diagnostic.CS1712.severity = none

dotnet_diagnostic.CS1717.severity = none

dotnet_diagnostic.CS8600.severity = none

dotnet_diagnostic.CS8602.severity = none

dotnet_diagnostic.CS8632.severity = none

dotnet_diagnostic.CS8603.severity = none

dotnet_diagnostic.CS8618.severity = none

dotnet_diagnostic.CS8625.severity = none

dotnet_diagnostic.IDE0161.severity = none

dotnet_diagnostic.SA0001.severity = none

dotnet_diagnostic.SA1101.severity = none

dotnet_diagnostic.SA1200.severity = none

dotnet_diagnostic.SA1204.severity = none

dotnet_diagnostic.SA1309.severity = none

dotnet_diagnostic.SA1506.severity = none

dotnet_diagnostic.SA1600.severity = none

dotnet_diagnostic.SA1601.severity = none

dotnet_diagnostic.SA1602.severity = none

dotnet_diagnostic.SA1611.severity = none

dotnet_diagnostic.SA1615.severity = none

dotnet_diagnostic.SA1617.severity = none

dotnet_diagnostic.SA1618.severity = none

dotnet_diagnostic.SA1619.severity = none

dotnet_diagnostic.SA1633.severity = none

dotnet_diagnostic.SX1101.severity = none

dotnet_diagnostic.SX1309.severity = none

dotnet_diagnostic.IDE0065.severity = none

dotnet_diagnostic.IDE0002.severity = none

dotnet_diagnostic.IDE0005.severity = none

dotnet_diagnostic.IDE0059.severity = none

dotnet_diagnostic.IDE0052.severity = none

dotnet_diagnostic.IDE0058.severity = none

dotnet_diagnostic.CA1305.severity = none

dotnet_diagnostic.CA1848.severity = none

dotnet_diagnostic.CA2254.severity = none

dotnet_diagnostic.CA1727.severity = none

dotnet_diagnostic.CA2201.severity = none

dotnet_diagnostic.CA1309.severity = none

dotnet_diagnostic.CA2208.severity = none

dotnet_diagnostic.IDE0007.severity = none

dotnet_diagnostic.IDE0008.severity = none

dotnet_diagnostic.IDE0061.severity = none

dotnet_diagnostic.IDE0022.severity = none

dotnet_diagnostic.IDE0039.severity = none

dotnet_diagnostic.IDE0051.severity = none

dotnet_diagnostic.IDE0270.severity = none

dotnet_diagnostic.IDE0037.severity = none

dotnet_diagnostic.CS0542.severity = none

[*.{cs,vb}]
#### Naming styles ####

# Naming rules

dotnet_naming_rule.interface_should_be_begins_with_i.severity = suggestion
dotnet_naming_rule.interface_should_be_begins_with_i.symbols = interface
dotnet_naming_rule.interface_should_be_begins_with_i.style = begins_with_i

dotnet_naming_rule.types_should_be_pascal_case.severity = suggestion
dotnet_naming_rule.types_should_be_pascal_case.symbols = types
dotnet_naming_rule.types_should_be_pascal_case.style = pascal_case

dotnet_naming_rule.non_field_members_should_be_pascal_case.severity = suggestion
dotnet_naming_rule.non_field_members_should_be_pascal_case.symbols = non_field_members
dotnet_naming_rule.non_field_members_should_be_pascal_case.style = pascal_case

# Symbol specifications

dotnet_naming_symbols.interface.applicable_kinds = interface
dotnet_naming_symbols.interface.applicable_accessibilities = public, internal, private, protected, protected_internal, private_protected
dotnet_naming_symbols.interface.required_modifiers = 

dotnet_naming_symbols.types.applicable_kinds = class, struct, interface, enum
dotnet_naming_symbols.types.applicable_accessibilities = public, internal, private, protected, protected_internal, private_protected
dotnet_naming_symbols.types.required_modifiers = 

dotnet_naming_symbols.non_field_members.applicable_kinds = property, event, method
dotnet_naming_symbols.non_field_members.applicable_accessibilities = public, internal, private, protected, protected_internal, private_protected
dotnet_naming_symbols.non_field_members.required_modifiers = 

# Naming styles

dotnet_naming_style.begins_with_i.required_prefix = I
dotnet_naming_style.begins_with_i.required_suffix = 
dotnet_naming_style.begins_with_i.word_separator = 
dotnet_naming_style.begins_with_i.capitalization = pascal_case

dotnet_naming_style.pascal_case.required_prefix = 
dotnet_naming_style.pascal_case.required_suffix = 
dotnet_naming_style.pascal_case.word_separator = 
dotnet_naming_style.pascal_case.capitalization = pascal_case

dotnet_naming_style.pascal_case.required_prefix = 
dotnet_naming_style.pascal_case.required_suffix = 
dotnet_naming_style.pascal_case.word_separator = 
dotnet_naming_style.pascal_case.capitalization = pascal_case
dotnet_style_operator_placement_when_wrapping = beginning_of_line
tab_width = 4
indent_size = 4
end_of_line = crlf
dotnet_style_coalesce_expression = true:suggestion
dotnet_style_null_propagation = true:suggestion
dotnet_style_prefer_is_null_check_over_reference_equality_method = true:suggestion
dotnet_style_prefer_auto_properties = true:silent
dotnet_style_object_initializer = true:suggestion
dotnet_style_namespace_match_folder = true:suggestion
dotnet_diagnostic.CA1050.severity = error

[*.cs]
csharp_indent_labels = one_less_than_current
csharp_using_directive_placement = outside_namespace:error
csharp_prefer_simple_using_statement = true:error
csharp_prefer_braces = true:silent
csharp_style_namespace_declarations = file_scoped:warning
csharp_style_prefer_method_group_conversion = true:silent
csharp_style_prefer_top_level_statements = true:silent
csharp_style_expression_bodied_methods = false:silent
csharp_style_expression_bodied_constructors = false:silent
csharp_style_expression_bodied_operators = false:silent
csharp_style_expression_bodied_properties = true:silent
csharp_style_expression_bodied_indexers = true:silent
csharp_style_expression_bodied_accessors = true:silent
csharp_style_expression_bodied_lambdas = true:silent
csharp_style_expression_bodied_local_functions = false:silent
dotnet_diagnostic.SA1210.severity = error
dotnet_style_namespace_match_folder = true
dotnet_sort_system_directives_first = true
dotnet_analyzer_diagnostic.severity = error
dotnet_diagnostic.SA1118.severity = silent

# Ignore paths
[/*.Tests/**]
dotnet_diagnostic.CA1707.severity = none

[*/Migrations/*]
generated_code = true
";
        return content;
    }
}