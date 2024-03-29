# BasicCAPTCHA

[![Run unit tests](https://github.com/benbristow/basiccaptcha/actions/workflows/unit-tests.yml/badge.svg)](https://github.com/benbristow/basiccaptcha/actions/workflows/unit-tests.yml)

## Overview

I created this library as I found that other NuGet libraries seem to focus a lot on integrating with ASP's bespoke
functionality rather than just verifying tokens. I originally created it to use on an Azure Functions API which uses
dependency injection, but you can use it anywhere.

Basic library for verifying CAPTCHA tokens. Currently only
supports [Google ReCAPTCHA (V3/V2)](https://developers.google.com/recaptcha/docs/verify) which use the 'siteverify'
endpoint to verify tokens, but I'm open to accepting pull requests for other providers if required.

You can also extend the library to support other providers by extending the `BaseProvider` class and implementing the `VerifyTokenAsync` method.

## Getting Started

1. Install Nuget Package

```
Install-Package BasicCaptcha -Version 1.2.0
```

2. Register the service with your secret key in your startup file, here I'm using an environment variable. Don't
   hardcode this secret if possible!

```csharp
builder.Services.AddHttpClient();  
builder.Services.AddCaptcha(new GoogleRecaptchaProvider(Environment.GetEnvironmentVariable("GOOGLE_RECAPTCHA_SECRET")));  
```

3. Use the service wherever you require it...

```cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BasicCaptcha.Contracts;

namespace Example.Controllers;

public sealed class ExampleController : Controller
{  
  private readonly ICaptchaService _captchaService;  

  public ExampleController(ICaptchaService captchaService)  
  {  
      _captchaService = captchaService;  
  }  
    
  public async Task<IActionResult> Example(string token)  
  {  
      var valid = await _captchaService.VerifyTokenAsync(token);
      return valid ? Ok() : BadRequest();  
  }  
}  
```

## Dummy Provider

If you're doing development work, especially API development work, you probably don't want to
have to fill out a CAPTCHA challenge every time you hit your endpoint. So you can use the dummy provider
to bypass the CAPTCHA challenge.

To use this you can use the dummy provider

```csharp
builder.Services.AddHttpClient();  
builder.Services.AddCaptcha(new DummyRecaptchaProvider());  
```

If you wish to force a failure, you can pass `fail` as the token (case-insensitive) and the `VerifyToken` method will
always
return false instead.
