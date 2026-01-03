using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuestAPI.Application.DTOs;

public sealed record UserTaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Xp { get; set; }
    public string Difficulty { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}