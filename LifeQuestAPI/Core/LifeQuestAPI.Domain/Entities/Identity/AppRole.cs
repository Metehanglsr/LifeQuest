using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeQuestAPI.Domain.Entities.Common;

namespace LifeQuestAPI.Domain.Entities.Identity;

public sealed class AppRole : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}