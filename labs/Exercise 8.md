## Building an ASP.NET 6 application


In the previous task, you built container images using some of the more mature technologies and products
released by Microsoft. In this task, you will build container that will run ASP.NET Web Application using .NET
6.0. If you completed the Exercise 2 in this Module, this will be very similar. However, we will now build the
aspnet application on Windows instead of Linux.

Furthermore, we will use the multi-stage build process rather than building the application manually with
dotnet CLI.

In this exercise, you will package a simple ASP.NET MVC application into a container image using a
Dockerfile. Finally, you will run container hosting the ASP.NET Core application using the docker run command.

### Building and Running ASP.NET 6.0 Application Inside Container
---

1. Change to the relevant directory using the following command:
```cd C:\labs\module1\aspnet-win```

2. You are provided with a Dockerfile. View the content of the Dockerfile in Visual Studio Code by running
the command
```code .\Dockerfile```

The Dockerfile should look like the one below
Note: This is a multi-stage Dockerfile

![image](https://user-images.githubusercontent.com/85903942/233817160-1a41a96e-353d-4419-8ea0-d5b400e18b82.png)

3. Close the Visual Studio Code.

4. To create the container image run the command
```docker build -t aspnetapp:6.0 .```

>Note: Notice the use of tag 6.0 that signifies the dotnet 6.0 framework version

5. Launch the container running the app inside it by running the command
```docker run -d -p 9000:80 aspnetapp:6.0```

6. You are now running ASP.NET Web application inside the container listening on the port 80 which is
mapped to port 9000 on the host.

7. To see the ASP.NET Web application in action open the web browser and navigate to localhost port 9000

```start http://localhost:9000```

This will take you to the Home page of the Web Application.
8. Run the following command to stop and remove all containers:

```docker rm (docker ps -aq) -f```

Congratulations!
