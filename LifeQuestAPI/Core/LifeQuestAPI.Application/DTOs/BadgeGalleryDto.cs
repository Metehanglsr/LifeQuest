using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuestAPI.Application.DTOs;

public sealed record BadgeGalleryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconPath { get; set; } = string.Empty;
    public int RequiredLevel { get; set; }
    public bool IsEarned { get; set; }
    public DateTime? EarnedAt { get; set; }
}