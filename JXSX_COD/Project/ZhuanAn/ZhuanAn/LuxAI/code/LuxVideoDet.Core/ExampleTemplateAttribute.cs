namespace LuxVideoDet.Core;

/// <summary>
/// 标在「仅作学习用的示例描述符」上：反射注册时会跳过，不会进入算法 / 通知 / AOI 工厂。
/// 复制到自己的模块并实现业务逻辑后，请移除此特性。
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class ExampleTemplateAttribute : Attribute
{
}
