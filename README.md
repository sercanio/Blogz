# Blogz
Simple blog platform with user interactivity.

# Screens
![screen1](https://github.com/user-attachments/assets/27ebef3a-9be0-4677-812b-cf58d105fe64)

![screen2](https://github.com/user-attachments/assets/2159ad16-6348-4832-9c4d-2943ebcd0fc2)

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
