# Blogz
Simple blog platform with user interactivity.

# Configuration
You need to create appsetting.json file, provide connection string to your PostgreSQL server and Cloudinary credentials:

```sh
{
  "ConnectionStrings": {
    "DefaultConnection": "User ID=postgres; Password=<PASSWORD>; Host=localhost; Port=5432; Database=BlogZ;"
  },

  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },

  "CloudinaryAccount": {
    "ApiKey": "your-api-key",
    "ApiSecret": "your-api-secret",
    "Cloud": "your-cloud-directory"
  },
}
```
