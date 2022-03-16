namespace DKZKV.Swagger;

public class SwaggerTitle
{
    public SwaggerTitle(string title, string description)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentException("Swagger title should not be empty");

        if (string.IsNullOrEmpty(description))
            throw new ArgumentException("Swagger description should not be empty");

        Title = title;
        Description = description;
    }

    public string Title { get; }
    public string Description { get; }
}