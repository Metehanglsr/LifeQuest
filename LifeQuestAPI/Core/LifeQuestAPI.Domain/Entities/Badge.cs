using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Domain.Entities.Common;

namespace LifeQuestAPI.Domain.Entities;

public sealed class Badge : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconPath { get; set; } = string.Empty;

    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; } = default!;

    public int RequiredLevel { get; set; }
    public int CriteriaType { get; set; }
}