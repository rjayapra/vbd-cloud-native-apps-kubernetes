## Container Core Concepts - Windows containers

### Working with Nano Server and Windows Server Core containers

In this exercise, you will learn about the Windows Nano Server and Server Core images. Please read below for
an overview of each image. Then you will complete the steps to build and run these containers.


> Alert: IMPORTANT: Following three exercises will focus on Windows Containers. To work with
Windows Containers we have to change tell Docker to point to the Windows. Steps bellow must be
completed before continuing with the exercises.

1. Locate Docker Icon in your Taskbar
> Note:You might have to click on the ^ icon first to get to Docker Icon
![image](https://user-images.githubusercontent.com/85903942/233816646-27a7b0e9-659c-47d4-96c1-25793f25fff2.png)


2. Right click on Docker Icon and click on Switch to Windows Containers.
> Alert: You might get a message that existing linux containers will continue to run, click the switch
button to move on. If you did not get a message ignore this step. 
![image](https://user-images.githubusercontent.com/85903942/233816652-8b87ee32-96c8-4780-9079-c5ba9bc03000.png)

![image](https://user-images.githubusercontent.com/85903942/233816658-bbb3795e-6b97-483e-a8bc-b51a2bdaaab3.png)


3. It might take few minutes for switch to complete and you will see the Docker Icon moving at the
moment. Once the Docker Icon stops moving, navigate to Powershell and type following to validate
that you switched.
```docker version```
![image](https://user-images.githubusercontent.com/85903942/233816667-a6348710-151a-4bb7-98ea-f7b18b3bae7b.png)


4. That's it, you are now ready to move on to the exercises.

### Windows Server Core Overview

The Server Core option is a minimal installation option that is available when you are deploying the Standard
or Datacenter edition of Windows Server. Server Core includes most but not all server roles. Server Core has a
smaller disk footprint, and therefore a smaller attack surface due to a smaller code base.
It is recommended that you choose the Server Core installation unless you have a need for the additional user
interface elements and graphical management tools that are included in the Server with Desktop Experience
option. For an even more lightweight option, see the next section on Nano Server. Server Core allows you to
install various Server roles that may not be available in Nano Server including those listed below:

> Knowledge:

- Active Directory Certificate Services
- Active Directory Domain Services
- Active Directory Federation Services
- Active Directory Lightweight Directory Services
- Active Directory Rights Management Services
- Device Health Attestation
- DHCP Server
- DNS Server
- File and Storage Services
- Hyper-V
- Print and Document Services
- Streaming Media Services
- Web Server (including a subset of ASP.NET)
- Windows Server Update Server
- Active Directory Rights Management Server
- Routing and Remote Access Server and the following sub-roles:
- Remote Desktop Services Connection Broker
- Licensing
- Virtualization
- Volume Activation Services

For a comprehensive list of features available in Server Core click (here)[https://docs.microsoft.com/en-us/windows-server/administration/server-core/server-core-roles-and-services#features-included-in-server-core].

#### Windows Nano Server Overview

Nano Server is optimized as a lightweight operating system for running "cloud-native" applications based on
containers and micro-services. There are important differences in Nano Server versus Server Core. As of
Windows Server 2016, version 1803, Nano Server is available only as a container base OS image. You must run
it as a container in a container host, such as a Server Core installation of Windows Server. Running a container
based on Nano Server in this release differs from releases prior to 1803 in the ways listed below:
Nano Server has been optimized for .NET Core applications
Nano Server size has been optimized in Windows Server 2019 version
PowerShell Core, .NET Core, and WMI are no longer included by default as they were in Windows Server
2016, but you can include PowerShell Core and .NET Core container packages when building your
container

There is no longer a servicing stack included in Nano Server. Microsoft publishes an updated Nano
image to Docker Hub that you redeploy

You troubleshoot the new Nano Container by using Docker
You Can now run Nano containers on IoT Core
For a comprehensive list of capability differences between Nano Server and Server Core, visit
https://docs.microsoft.com/en-us/windows-server/get-started/getting-started-with-nano-server)

Microsoft hosts base OS images in Microsoft Container Registry (MCR), and they are fully searchable and
available through Docker Hub. In the following exercise, you will run a basic "hello world" container leveraging
both the Nano and Server Core images.

### Run a container based on the Nano Server base image
---

1. Oper your Windows Terminal, in the case you closed it earlier and run the following command to view
your images:
```docker images```

2. Notice that you already have Server Core and Nano Server images locally cached on your Virtual
Machine.
> Knowledge: It's important to understand that you can always download specific version of
windows/servercore and windows/nanoserver images by using an appropriate tag. For
example, docker pull mcr.microsoft.com/windows/servercore:10.0.17763.437 will pull
the server core image that has a version number 10.0.17763.431. Notice the mcr.microsoft.com
registry that is the container registry hosted on Microsoft servers, even though the images are
discoverable on Docker Hub. All the concepts you learned about docker (Linux) containers and
images generally apply as-is to windows containers too. The main deference is the fact that
windows containers require the windows operating system as a host, while the Linux containers
require Linux operating system.

3. You will now run a container based on Server Core image (mcr.microsoft.com/windows/servercore).
Before you do that, run the command
```hostname```
This will reveal the hostname of your virtual machine.
> Note:Please note that your host machine name may be different.

4. Run the command
```docker run -it mcr.microsoft.com/windows/servercore:20H2 powershell```

Please be patient as it will take a minute or so for this command to work.

- The **-it** switch provides you with an interactive session.
- The **powershell** is a parameter passed as an argument which basically gives you access to
Powershell (command line) running inside the container.

Technically, the **-it** switch puts you inside a running container.
![image](https://user-images.githubusercontent.com/85903942/233816729-c4ba677b-fa09-47a0-a6ec-78a8007cf070.png)


5. Run the command
```hostname```

![image](https://user-images.githubusercontent.com/85903942/233816758-5fa45bac-1f0b-45fe-a708-d8c2df18ee77.png)


This time you are running it inside the running container.

> Note:Notice that the host name is different from the host name you get in step 1. The host name
you see inside the container is the host name of the container itself. It is based on the container
ID. You may want to run other commands as you wish or checkout the filesystem that is
independent from the host's filesystem.

6. Finally, exit the interactive session by typing
```exit```

This will take you back to the PowerShell console on the host.

7. Now let's run another container based on Nano Server image
(mcr.microsoft.com/windows/nanoserver). To do that run the command
```docker run -it mcr.microsoft.com/windows/nanoserver:20H2 CMD```

> Hint:It might take a few seconds to load. This time we are starting a Windows Command prompt
instead of Powershell inside of the container)

8. Run the command
```hostname```

![image](https://user-images.githubusercontent.com/85903942/233816776-5d5f7834-2c0f-4de3-b3d8-9416ee833764.png)

Notice that the host name is different from host name you get in the previous steps. Again, the host
name you see inside the container is the host name of the container itself, which is based on the
container id. You can run other commands as you wish.

9. Finally, exit the interactive session by typing
```exit```

This will take you back to the PowerShell console on the host.

![image](https://user-images.githubusercontent.com/85903942/233816791-3214221c-f1ec-46ff-833b-cd8017bc3c14.png)

Congratulations!

In this exercise, you have created and run containers based on the Windows Server Core & Nano Server
container images that Microsoft provides and maintains. 

