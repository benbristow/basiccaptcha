# BasicCAPTCHA

```
Install-Package BasicCaptcha -Version 1.0.0
```

## Overview

I created this library as I found that other NuGet libraries seem to focus a lot on integrating with ASP's bespoke functionality rather than just verifying tokens. I originally created it to use on an Azure Functions API which uses dependency injection, but you can use it anywhere.

Basic library for verifying CAPTCHA tokens. Currently only supports [Google ReCAPTCHA (V3/V2)](https://developers.google.com/recaptcha/docs/verify) which use the 'siteverify' endpoint to verify tokens, but I'm open to accepting pull requests for other providers if required.

## TL;DR - Examples

### With dependency injection

1) Install Nuget Package

2) Register the service with your secret key in your startup file, here I'm using an environment variable. Don't hardcode this secret if possible!
```cs
using System;  
using BasicCaptcha.Enums;  
using BasicCaptcha.Extensions;  
using Microsoft.Azure.Functions.Extensions.DependencyInjection;  
  
namespace Example  
{  
    public class Startup  
    {  
        public override void Configure(IFunctionsHostBuilder builder)  
        {  
            builder.Services.AddCaptcha(ExternalCaptchaProvider.GoogleRecaptcha, Environment.GetEnvironmentVariable("GOOGLE_RECAPTCHA_SECRET"));  
        }  
    }  
}
```

3. Use the service wherever you require it...
```cs
using System.Threading.Tasks;  
using BasicCaptcha.Contracts;  
using Microsoft.AspNetCore.Mvc;  
  
namespace Example  
{  
    public class ExampleController  
    {  
        private readonly ICaptchaService _captchaService;  
  
        public ExampleController(ICaptchaService captchaService)  
        {  
            captchaService = captchaService;  
        }  
          
        public async Task<IActionResult> Example(string token)  
        {  
            var valid = await _captchaService.VerifyToken(token);
            if (!valid)  
            {  
                return new BadRequestResult();  
            }  
  
            return new OkResult();  
        }  
    }  
}
```

### Manual 

1. Install Nuget package

2. Use as follows. Here I'm using an environment variable. Don't hardcode this secret if possible...

```cs
using System;
using System.Threading.Tasks;  
using BasicCaptcha;  
using BasicCaptcha.Enums;  
using Microsoft.AspNetCore.Mvc;  
  
namespace Example  
{  
    public class ExampleController  
    {  
        public async Task<IActionResult> Example(string token)  
        {  
            var captchaService = new CaptchaService(ExternalCaptchaProvider.GoogleRecaptcha, Environment.GetEnvironmentVariable("GOOGLE_RECAPTCHA_SECRET"));  
            var valid = await captchaService.VerifyToken(token);  
  
            if (!valid)  
            {  
                return new BadRequestResult();  
            }  
  
            return new OkResult();  
        }  
    }  
}
```
