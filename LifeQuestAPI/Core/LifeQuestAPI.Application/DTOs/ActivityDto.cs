using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuestAPI.Application.DTOs;

public sealed record ActivityDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Xp { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public DateTime RawDate { get; set; }
}