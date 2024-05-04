using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coding_Tracker.Model;

[Table("Tracker")]
public class CodingSessionModel
{
    public string? ID { get; init; }
    public string? Duration { get; init; }
    public DateTime Date { get; init; }
}