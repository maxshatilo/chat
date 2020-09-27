# Requirements
1. Visual Studio 2019 (for .Net Core app) and Visual Studio Code (for Angular app)
2. MsSQL Server 2012 R2 or higher
3. [Angular CLI](https://github.com/angular/angular-cli)
4. [.NET Core SDK](https://www.microsoft.com/net/download)


# How to run the Angular SPA application
1. In Visual Studio Code navigate inside of **ChatClient** folder and run _**npm install**_ command
2. Start the client app by entering _**ng serve --o**_

# How to run the .Net Core Web API application
1. Copy provided **appsettings.json** file into **ChatServer** folder and set your database connection string in **DefaultConnection** setting
2. In Visual Studio 2019 open **ChatServer.sln** solution and in Package Manager Console run _**Update-Database -context ApplicationDbContext**_ and _**Update-Database -context MessageDbContext**_ commands
3. Start the server app