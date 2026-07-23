using System.Collections.ObjectModel;
using LuxVideoDet.Core.Aoi;
using LuxVideoDet.Core.Algorithm;
using LuxVideoDet.Core.Notification;

namespace LuxVideoDet.Desktop.ViewModels;

/// <summary>
/// 配置编辑器中单个算法参数表单区块（标题 + 与 <c>args</c> 映射的参数行）。
/// </summary>
public sealed class AlgorithmArgsFormSectionViewModel : ViewModelBase
{
    public AlgorithmArgsFormSectionViewModel(AlgorithmConfigViewModel owner, AlgorithmArgsFormSection section)
    {
        Title = ResolveTitle(section);
        Description = section.Description;

        foreach (var def in section.ArgFields)
        {
            var raw = AlgorithmConfigViewModel.ResolveArgDisplayRaw(owner, def);
            ParameterRows.Add(new NotifierParameterRowViewModel(def, raw));
        }
    }

    public string Title { get; }

    public string? Description { get; }

    public ObservableCollection<NotifierParameterRowViewModel> ParameterRows { get; } = new();

    private static string ResolveTitle(AlgorithmArgsFormSection section)
    {
        if (!string.IsNullOrWhiteSpace(section.SectionTitle))
            return section.SectionTitle.Trim();
        if (!string.IsNullOrWhiteSpace(section.AoiDetectorTypeKey))
            return AoiDetectorFactory.GetDisplayName(section.AoiDetectorTypeKey);
        return string.Empty;
    }
}
