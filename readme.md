# Test project for reproducing System.AggregateException 

## Summary

I want to pass a object from .cshtml view to .razor component like the link below.

[https://learn.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/component-tag-helper?view=aspnetcore-6.0](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/component-tag-helper?view=aspnetcore-6.0)<br />
Section : Pages/MyPage.cshtml

I could get the value from controller and set it to a view correctly, but get javascript error at blazor.webassembly.js:1.
I want to set new value with onclick event, but blazor doesn't work with this error.

## Reproduce Instruction

- Create Blazor Webassembly App with ASP.NET Core Hosted

- Enable ServerPrerendered following by this instruction

[https://learn.microsoft.com/en-us/aspnet/core/blazor/components/prerendering-and-integration?view=aspnetcore-6.0&pivots=webassembly](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/prerendering-and-integration?view=aspnetcore-6.0&pivots=webassembly)

#### Create model in Shared project like this
MyClass.cs
```
namespace BlazorAppTest.Shared.Models
{
    public class MyClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public MyClass() { }
    }
}
```


#### Pass the object with param-xxx by component tag like this
MyPage.cshtml
```
@page
@using BlazorAppTest.Client.Pages
@model BlazorAppTest.Server.Pages.MyPageModel
@{
    ViewData["Title"] = "Test";
    Layout = "~/Pages/_Layout.cshtml";
}
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers

<div>MyPage.cshtml string.</div>
<hr />

<component type="typeof(MyComponent)" render-mode="WebAssemblyPrerendered" param-MyObject="Model.MyObject" />
```

#### Razor is like this
MyComponent.razor
```
@using BlazorAppTest.Shared.Models

<div>MyComponent.razor string.</div>
<div>Id:@(MyObject?.Id)</div>
<div>Name:@(MyObject?.Name)</div>

<div><input type="button" @onclick="ChangeStr" value="Button" /></div>

@code {
    [Parameter]
    public MyClass? MyObject { get; set; }

    private void ChangeStr()
    {
        MyObject.Id = 2;
        MyObject.Name = "ChangeStr";
    }
}
```

#### Launch server and navigate to /MyPage, and output is this
```
MyPage.cshtml string.
[Border]
MyComponent.razor string.
Id:1
Name:OnGet
[Button]
```

This output is no problem, but the button cannot work.

## Detail exception

```
System.AggregateException: One or more errors occurred. (The parameter 'MyObject' with type 'BlazorAppTest.Shared.Models.MyClass' in assembly 'BlazorAppTest.Shared' could not be found.)
 ---> System.InvalidOperationException: The parameter 'MyObject' with type 'BlazorAppTest.Shared.Models.MyClass' in assembly 'BlazorAppTest.Shared' could not be found.
   at Microsoft.AspNetCore.Components.WebAssemblyComponentParameterDeserializer.DeserializeParameters(IList`1 parametersDefinitions, IList`1 parameterValues)
   at Microsoft.AspNetCore.Components.WebAssembly.Hosting.WebAssemblyHostBuilder.InitializeRegisteredRootComponents(IJSUnmarshalledRuntime jsRuntime)
   at Microsoft.AspNetCore.Components.WebAssembly.Hosting.WebAssemblyHostBuilder..ctor(IJSUnmarshalledRuntime jsRuntime, JsonSerializerOptions jsonOptions)
   at Microsoft.AspNetCore.Components.WebAssembly.Hosting.WebAssemblyHostBuilder.CreateDefault(String[] args)
   at Program.<Main>$(String[] args) in C:\Users\xtlab\source\repos\BlazorAppTest\BlazorAppTest\Client\Program.cs:line 5
   --- End of inner exception stack trace ---
callEntryPoint @ blazor.webassembly.js:1
await in callEntryPoint (async)
At @ blazor.webassembly.js:1
await in At (async)
(anonymous) @ blazor.webassembly.js:1
(anonymous) @ blazor.webassembly.js:1
```

## Environment

- Windows10

- Visual Studio 2022 17.3.4

- .NET 6.0


## GitHub

Reproducing project files are [here](https://github.com/XT-Lab/BlazorAppTest).

