﻿using System.Text.Json.Serialization;
namespace client.Models;

public class TokenResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; }
}