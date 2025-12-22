using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Domain.Entities.Common;

namespace LifeQuestAPI.Domain.Entities;
public sealed class Category : BaseEntity
{
    public string Name { get; set; } =string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconPath { get; set; }  = string.Empty;

    public ICollection<AppTask> Tasks { get; set; } = default!;
    public ICollection<UserCategoryProgress> UserProgresses { get; set; } = default!;
    public ICollection<Badge> Badges { get; set; } = default!;
}