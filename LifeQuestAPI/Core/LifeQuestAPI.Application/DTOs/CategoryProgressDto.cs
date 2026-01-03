using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuestAPI.Application.DTOs;

public sealed record CategoryProgressDto
{
    public string CategoryName { get; set; } = string.Empty;
    public string IconPath { get; set; } = string.Empty;
    public int Level { get; set; }
    public int CurrentXp { get; set; }
    public int XpToNextLevel { get; set; }
}