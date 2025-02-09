# WebForum

Assessment project for a Backend Developer position.

Project represents a web forum backend. Users can subscribe to the forum and make posts. Other functions include adding comments and also liking posts.

Used clean architecture, ASPNET Core Api 8.0, AspNetCore Identity, Sql Server

# Install

You need the following on your local pc installed to run this project.
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) or other IDE
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) 
- [Postman](https://www.postman.com/downloads/)

# Run API

Clone the project locally and open the solution using the solution file in Visual Studio.

In Visual Studio navigate to the docker-compose project in the Solution Explorer tab. 

Right click on the project and select 'Set as Startup Project' and build the project.

Run the project in docker using the play button. Make sure Docker Compose is next to the green play button.

Please be aware there will be a delay as the docker images are first downloaded.

A new browser tab should open with the [Swagger](http://localhost:8966/swagger/index.html) page displaying the api endpoints.

The database is created and data bootstrapped on project start.

# Bootstrapped Data

The project contains 2 default roles:
- Standard
- Moderator

One Moderator user is setup by default with the following credentials:
- Username: mody@mody.com
- Password: Mod3r@tor123!
- Name: Moderator
- Surname: Moderator

Two Standard users:
- Standard 1
  - Username: standard1@standard1.com
  - Password: St@nd@rd123!
  - Name: John
  - Surname: Doe
- Standard 2
  - Username: standard2@standard2.com
  - Password: St@nd@rd123!
  - Name: Jane
  - Surname: Doe

Additional Standard users can be added using the Register API endpoint.

# Postman Setup

Open Postman and go to the Environments section on the left. 

Click on the Import button and provide environment file in the postman directory.
Use the following file in folder 'WebForumEnv.postman_environment.json'

If successful **WebForumEnv** should be visibile in the list of environments. 
**Set this as active.** 

Go back to the Collection section in Postman and click on the Import button.
Use the following file in postman folder: WebForumApi.postman_collection.json

If successful **WebFormApi** should be visible the list of collections.

# Test API Using Postman

Check the following steps before trying to test the API:

- Make sure the project is running in Visual Studio. Follow **Run API** section above
- Make sure the Postman environment and collection file is imported. Follow **Postman Setup** section above
- Make sure the API is running.
  - Check that the Swagger page is visible. 
  - Can also check using [ping](http://localhost:8966/ping) or [health](http://localhost:8966/health)

In the *WebForumApi* Postman collection endpoint calls are setup for each user in the **Bootstrapped Data** section.

The Postman requests are setup in such a way if the requests executed from top to bottom in collection folders the required values are stored in the environment file.

This minimize the effort for the assessor as access tokens and ids are stored and setup correctly for each request.

