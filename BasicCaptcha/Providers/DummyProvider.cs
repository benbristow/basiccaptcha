﻿using System.Net.Http;
using System.Threading.Tasks;

namespace BasicCaptcha.Providers;

public sealed class DummyProvider : BaseProvider
{
    public override Task<bool> VerifyTokenAsync(string? token, HttpClient? httpClient = null) =>
        Task.FromResult(token?.ToLowerInvariant().Trim() != "fail");
}
