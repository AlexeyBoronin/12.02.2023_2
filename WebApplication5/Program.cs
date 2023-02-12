using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*app.Run(async (context) =>
{
    if (context.Request.Path == "/old")
    {
        //await context.Response.WriteAsync("Old Page");
        context.Response.Redirect("https://github.com/session");
    }
    else if(context.Request.Path=="/new")
    {
        await context.Response.WriteAsync("New Page");
    }
    else
    {
        await context.Response.WriteAsync("Main Page");
    }
});*/
/*app.Run(async (context) =>
{
    Person ted = new("Ted", 22);
    await context.Response.WriteAsJsonAsync(ted);
});*/

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    if (request.Path == "/api/user")
    {
        var message = "Некорректные данные";
        try
        {
            var person = await request.ReadFromJsonAsync<Person>();
            if (person != null)
                message = $"Name: {person.Name} Age:{person.Age}";
        }
        catch { }
        await response.WriteAsJsonAsync(new { text = message });
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }
});
app.Run();

public record Person(string Name,int Age);