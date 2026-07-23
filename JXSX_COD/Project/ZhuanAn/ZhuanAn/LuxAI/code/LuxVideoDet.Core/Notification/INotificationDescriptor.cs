namespace LuxVideoDet.Core.Notification;

/// <summary>
/// 通知服务描述符 — 每种通知渠道提供一个实现，供 <see cref="NotificationServiceFactory"/> 反射自动注册。
/// <para>
/// 新增通知类型时：在对应子目录添加 <c>*NotificationDescriptor.cs</c> 实现本接口，
/// 无需再修改工厂中的 switch。
/// </para>
/// </summary>
public interface INotificationDescriptor
{
    /// <summary>配置中 <c>type</c> 字段使用的标识（小写，如 <c>webhook</c>、<c>speaker</c>）</summary>
    string TypeKey { get; }

    /// <summary>UI 或文档中的显示名称</summary>
    string DisplayName { get; }

    /// <summary>
    /// 该通知类型需要的参数定义（表单/校验用）。<see cref="NotificationParameterDefinition.Required"/> 为 true 的项，
    /// 须在对应 <see cref="INotificationService.Initialize"/> 中校验；不满足时抛 <see cref="ArgumentException"/>，工厂将不加载该渠道。
    /// </summary>
    IReadOnlyList<NotificationParameterDefinition> ParameterDefinitions { get; }

    /// <summary>创建通知服务实例（尚未调用 <see cref="INotificationService.Initialize"/>）</summary>
    INotificationService Create(NotificationServiceFactoryContext context);
}
