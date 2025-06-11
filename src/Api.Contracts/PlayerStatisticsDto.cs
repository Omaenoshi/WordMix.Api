namespace WordMix.Api.Contracts;

using System;

public class PlayerStatisticsDto
{
    public DateTimeOffset? Date { get; set; }
    
    public int Score { get; set; }
}