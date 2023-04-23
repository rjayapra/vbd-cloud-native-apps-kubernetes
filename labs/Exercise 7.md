## Container Core Concepts - Windows containers

### Build and run an IIS Server Image

In the exercise you will learn how to install IIS Web Server (Web Server Role) on a Windows Server Core base
core image. IIS Server is a popular Web Server released by Microsoft. Considering the strong footprint of IIS
within enterprises, Microsoft supports IIS on Windows Server Core.


### Build and run an IIS Server Image
---

1. Make sure you have a Windows Terminal and change the current directory to "iis" by running the
command
```cd c:\labs\module1\iis```

2. Run command to list all the files in directory
```ls```

3. The iis folder contains the Dockerfile with instructions to install IIS Server (Web Server Role) on the
Windows Server Core base image. Display the Dockerfile in Visual Studio code by running the command

```code .\Dockerfile```

![image](https://user-images.githubusercontent.com/85903942/233816894-bdb3aa2b-ea14-4866-a7ac-c97444b9bb8c.png)

- The FROM instruction points to the mcr.microsoft.com/windows/servercore to be used as a
base image for the new container image
- The RUN instruction executes PowerShell to install Windows Feature "Web Server" (IIS Server)
- The next command is the ADD instruction which copies the ServiceMonitor.exe utility to the
container image. The ServiceMonitor.exe is a utility that monitors w3svc service inside the
container, if the service fails, the exe fails, so Docker knows the container is unhealthy.
> Hint:The ServiceMonitor.exe is developed and released by Microsoft
(https://github.com/microsoft/iis-docker/tree/master/windowsservercore-ltsc2019)
- The EXPOSE instruction does not actually publish the port. It functions as a type of
documentation between the person who builds the image and the person who runs the
container, about which ports are intended to be published.
- The ENTRYPOINT instruction makes sure that monitoring of w3svc begins immediately as soon
as container starts running. This is what will keep the container in running state.

4. Close the Visual Studio code.

5. To build the new image with IIS installed on it, run the command

```docker build -t myiis:v1 .```

This command builds a new container image with name myiis and tag v1. The tag conveniently tells
everyone information pertaining to the version of the image.

> Alert: STEP 2/6 of the build process performs the installation of the Web-Server (IIS Server) and
may take few minutes. Eventually you should see the results as follow


![image](https://user-images.githubusercontent.com/85903942/233816927-4865156f-f9b4-49ec-9bd8-ea9d9af5081b.png)

6. Run a new container based on myiis:v1 image by using the command:

```docker run -d -p 8099:80 myiis:v1```

![image](https://user-images.githubusercontent.com/85903942/233816960-dd8c1e1b-46f5-4446-abed-3ddddfe15e62.png)

7. The full container ID is shown after the run command (de2 in the above screenshot), or can be obtained by using

```docker ps```

8. To open up your container in browser, run the following command:

```start http://localhost:8099```

![image](https://user-images.githubusercontent.com/85903942/233816973-5c7b20d8-9d40-4393-8ff5-7184af86c188.png)

> Note: You can get the container's IP address of the container with **docker inspect** command as
follow: **docker inspect --format '{{ .NetworkSettings.Networks.nat.IPAddress}} ' containerid**.
When accessing the container using it's IP Address you would use the port the container is
listening on (port 80 in this case)

Congratulations!

This concludes the exercise on creating a new image with IIS server. If you are looking to leverage IIS server
beyond this lab, then you may want to use Microsoft official IIS server image
(mcr.microsoft.com/windows/servercore/iis) which is available at https://hub.docker.com/r/microsoft/iis/

The underlying process is pretty much same but the main benefit of using the official IIS image is that
Microsoft releases updated images on a regular basis including patches and fixes.

