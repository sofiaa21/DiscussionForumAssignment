﻿namespace Domain.DTOs;

public class SearchPostParametersDto
{
    public string? Username { get;}
    public bool? CompletedStatus { get;}
    public string? TitleContains { get;}

    public SearchPostParametersDto(string? username, string? titleContains)
    {
        Username = username;
        TitleContains = titleContains;
    }
}