# Module 4: Hosting Services On-Premises and in Containers

### Exercise 1: Creating a Web App IIS

#### Task 1: Prepare IIS

1. Download [.NET Core Hosting Bundle Installer](https://dotnet.microsoft.com/permalink/dotnetcore-current-windows-runtime-bundle-installer)
2. Restart the server or execute `net stop was /y` followed by `net start w3svc` in an **administrator** command shell.
3. On the IIS server, create a folder to contain the app's published folders and files.
4. Start IIS Manager  **(C:\Windows\System32\inetsrv\InetMgr.exe**)
5. In IIS Manager, open the server's node in the **Connections** panel. Right-click the **Sites** folder. Select **Add Website** from the contextual menu.
6. Provide a **Site name** and set the **Physical path** to the app's deployment folder that you created.
7. Provide the **Binding** configuration, set port to 8888 and create the website by selecting **OK**.
8. Add another  **https Binding** on port 8889. Use an SSL certificate (You might need to create one. Root node/Server Certificates)
9. In **Application Pools** open the pool that has your **Site name**.
10. Set **.NET CLR version** to **No Managed Code**
11. In Advanced Settings set **Identity** to your own user name and passworrd (not good practise but it prevents messing around in SqlServer which need grant access to the Identity account)

#### Task 2: Deploy ProductReviews.API

1. Open a command prompt and navigate to **[Drive:]\AllFiles\Mod04\Starter\Exercise 1\ProductReviews.API**.

2. Publish the site using

   ```bash
    dotnet publish --configuration Release -o <path_to_folder_you_created_in_step_3>
   ```

3. Test the deployment **ProductReviews.API** (http://localhost:8888/api/productgroup)

#### Task 3: Configure an environment variable and the database connection string

1. Open a command prompt and navigate to **[Drive:]\AllFiles\Mod04\Starter\Exercise 1\ProductReviews.API**.
2. Open the project Visual Studio Code.
3. In **Startup.cs** (**Program.cs** in .NET6) find the connection string. Copy the value and replace it for a string variable **connectionString**
3. Assign the environment entry **ASPNETCORE_DATABASE** to **connectionString**
4. In IIS Manager, Application Pools, stop  the pool from your web site
5. In Sites/<your_Site> open the configuration Editor.
6. Select: **system.webServer/aspNetCore** 
7. In **environmentVariables** add the entry **ASPNETCORE_DATABASE** with your connectionstring
8. Close window and **apply** changes
9. Now start the application pool again and test your web site.Exercise 2: Deploying an ASP.NET Core Web API to the Web App

### Exercise 2: Publishing the ASP.NET Core Web Service for Linux

Before you start make sure Docker is running (Docker Desktop)

Containers are isolated units on a host. Therefore they cannot communicate to SqlServer Express through named pipes. We need to configure SqlServer Express to allow remote connections.

#### Task 1: Prepare SqlServer for usage with containers

1. Open **Sql Server Managament Studio** and connect to **.\sqlexpress**.
2. On the connection node select **Properties** en then **Security. Select SQL Server and Windows Authentication Mode**.
3. On the connection node select **Properties** en then **Connections**. Make sure **Allow remote connections to this server** is checked
4. Open **SQL Server 2019 Configuration Manager**.
5. Under the node **SQL Server Network Configuration** select **Protocols for SQLEXPRESS**.
6. Select **TCP/IP** and enable it
7. On the tab **IP addresses**, under **IPAll** (last entry) clear **TCP Dynamic Ports** and set 1433 for **TCP Port** 
8. Restart Sql Server (select **SQL Server Services**).

#### Task 2: Create a login user.

1. Open **Sql Server Managament Studio** and connect to **.\sqlexpress**.
2. Under **Security->Login**s create a **New Login**.
   - Login Name: DbUser
   - SQL Server authentication: Select
   - Password: Pa$$w0rd
   - User must change password at next login: uncheck
   - Default database: Mod1DB (or whatever database you use for these exercise)
3. Once created double click the new login and navigate to **User Mapping**.
   - Check **Mod1DB** (or whatever you named your database)
   - Select roles **db_datareader**, **db_datawriter** and **public** (checked by default)

#### Task 3: Test the connection

1. Open the command prompt and go to the following directory:

   ```bash
   cd [Drive:]\Allfiles\Mod04\Labfiles]Starter\Exercise 2\ProductReviews.DatabaseConsole
   ```

2. Open project in Visual Studio Code

3. Modify connection string to (change xxx for your pc number.)

   ```sql
   Server=tcp:pc-xxx;Database=Mod1DB;User Id=DbUser;Password=Pa$$w0rd;MultipleActiveResultSets=True;
   ```
3. Test **ProductReviews.DatabaseConsole** if the connection string works.

#### Task 4: Use a Docker container to build a self-contained ASP.NET Core web service

1. Open the command prompt and go to the following directory:

   ```bash
   cd [Drive:]\Allfiles\Mod04\Labfiles]Starter\Exercise 2
   ```


2. Add a **ProductReviews.sln** solution file. ( ```dotnet new sln -n ProductReviews```)

3. Add the following projects to the solution (```dotnet sln add <PROJECT_DIR>```)

   - ProductReviews.API
   - ProductReviews.DAL.EntityFramework
   - ProductReviews.Interfaces
   - ProductReviews.Repositories.EntityFramework

4. Navigate to the **ProductReviews.API** project, add a new **Dockerfile** file.

5. In the **Dockerfile** file, first create a runtime image

   ```dockerfile
   FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
   WORKDIR /app
   EXPOSE 80
   EXPOSE 443
   ENV ASPNETCORE_DATABASE="Server=host.docker.internal;Database=Mod1DB;User Id=DbUser;Password=Pa\$\$w0rd;MultipleActiveResultSets=True"
   ```

   > host.docker.internal is a special url in docker which refers to the ip-address of the host machine. Only useful in development scenarios.

6. Next create a build image and copy the files from your machine into the build image.

   ```dockerfile
   FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
   WORKDIR /src
   COPY . ./
   RUN dotnet restore
   RUN dotnet build -c Release -o /app/build
   ```

7. Publish the code in the build image

   ```dockerfile
   FROM build AS publish
   RUN dotnet publish  -c Release -o /app/publish
   ```

8. Finally copy the publish output to the runtime environment

   ```dockerfile
   FROM base AS final
   WORKDIR /app
   COPY --from=publish /app/publish .
   ENTRYPOINT ["dotnet", "ProductReviews.API.dll"]
   ```

9. On the command prompt navigate to

   ```bash
   cd [Drive:]\Allfiles\Mod04\Labfiles]Starter\Exercise 2
   ```

10. Execute the command: (don't forget the . in the end)

    ```bash
    docker build -t productreviews -f .\ProductReviews.API\Dockerfile .
    ```

11. Run the container

    ```bash
    docker run -p 8080:80 productreviews
    ```

12. Open a web browser and navigate to http://localhost:8080/api/productgroup

#### Task 5: Create a docker-compose file to make things easier

docker-compose files are usefull in more complex startup scenarios. The docker run command in the previous exercise was pretty easy but will be more complex in more sophisticated  scenarios. 

1. On the command prompt navigate to

   ```bash
   cd [Drive:]\Allfiles\Mod04\Labfiles]Starter\Exercise 2
   ```

2. Open folder in Visual Studio Code

3. In Visual Studio Code press Ctrl+Shift+P

4. Type **Docker: Add Docker Compose Files to Workspace** (You might need to install the Docker extension for this)

   - Select: .NET: ASP.NET Core
   - Select: ProductReviews.API\ProductReviews.API.csproj
   - Select: Linux
   - Port: 80

5. In the generated yml file modify the ports section to 8080:80

6. Add the following below ports. (Make sure the code is aligned well. Note the $-character needs to be parsed)

   ```yaml
   environment:
         - ASPNETCORE_DATABASE=Server=host.docker.internal;Database=Mod1DB;User Id=DbUser;Password=Pa$$$$w0rd;MultipleActiveResultSets=True
   ```

7. On the command prompt run

   ```bash
   docker-compose build
   ```

8. After that:

   ```bash
   docker-compose up
   ```

9. Open a web browser and navigate to http://localhost:8080/api/productgroup

#### Task 6: Enable https in the container

1. Open the command prompt and navigate to

   ```bash
   cd [Drive:]\Allfiles\Mod04\Labfiles]Starter\Exercise 2
   ```

2. Create a self-signed certificate for the app.

   ```
   dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\ProductReviews.API.pfx -p Pa$$w0rd
   dotnet dev-certs https --trust
   ```

   > If the certificate already exists, you can reuse it.

3. Open **docker-compose.yml** in Visual Studio Code

3. Make sure the port map under ports is available: - 5001:443

4. Set the following environment variables

   ```yaml
   environment:
       - ASPNETCORE_DATABASE=Server=host.docker.internal;Database=Mod1DB;User Id=DbUser;Password=Pa$$$$w0rd;MultipleActiveResultSets=True
       - ASPNETCORE_URLS=https://+;http://+
       - ASPNETCORE_HTTPS_PORT=443
       - ASPNETCORE_Kestrel__Certificates__Default__Password=Pa$$$$w0rd
       - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/ProductReviews.API.pfx 
   ```

   > The ASPNETCORE_URLS environment variable is used to specify the URL for the app like `ASPNETCORE_URLS="https://+;http://+"`. This means that the APP will be opened in both http and https.

   > The `ASPNETCORE_Kestrel__Certificates__Default__Password` specifies the password for the SSL certificate.

   > The `ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx` specifies the default path of the https certificate. It is set to be inside the ‘https’ directory of the container. Into this path the certificate should load. In our case the certificate will load from the stored location in our system which is outside the docker container. I will use volume to specify this path.

5. Mount the certificate directory

   ```yaml
   volumes:
         - ~\.aspnet\https:/https/
   ```

6. On the command prompt run

   ```bash
   docker-compose build
   ```

8. After that:

   ```bash
   docker-compose up
   ```

8. Open a web browser and navigate to http://localhost:8080/api/productgroup.

> The command line version of the docker-compose file would look something like this
>
> `docker run -p 8080:80 -p 8081:443 -e ASPNETCORE_DATABASE=Server=host.docker.internal;Database=Mod1DB;User Id=DbUser;Password=Pa$$$$w0rd;MultipleActiveResultSets=True -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=443 -e ASPNETCORE_Kestrel__Certificates__Default__Password=Pa$$w0rd -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/ProductReviews.pfx -v ~\.aspnet\https:/https/ productreviewsapi`
