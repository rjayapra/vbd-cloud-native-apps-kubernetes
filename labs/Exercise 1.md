## Container Core Concepts

## Exercise 1: Running Your First Container

In this exercise, you will launch a fully functional WordPress blog engine using a Linux-based Docker
container. You will learn the commands needed to pull the container image and then launch the container
using the Docker CLI. Finally, you will observe that by running the container, you don't need to install any of
the WordPress dependencies onto your machine; the WordPress engine binaries and all dependencies will be
packaged inside of the container.

### Running WordPress Blog Engine Container 

1. From your taskbar, click on the Windows Terminal icon to open Windows Terminal.
2. Type the command below
> docker pull msftcontainers/wordpress

> **Knowledge**:
> This will tell Docker client to connect to public Docker Registry and download the
> latest version of the WordPress container image published by msftcontainers (hence the format
> msftcontainers/wordpress). The container image has been pre-downloaded for you on the VM
> to save you a few minutes, so you will see the Status message as "Images is up to date for
> msftcontainers/wordpress:latest".

3. Run the command
> docker images

> **Note**: Notice "msftcontainers/wordpress" container image is now available locally for you to use.

![image](https://user-images.githubusercontent.com/85903942/233814496-b13e3a3b-5942-48c0-9ccf-270c893006ef.png)

4. That's it! You can now run the entire WordPress in a container. To do that run the command docker
> run -d -p 80:80 msftcontainers/wordpress
> **Note**: Pay close attention to the dash "-" symbol in front of "-p" and "-d" in the command.

5. Run the following _docker ps_ to see the running containers.
![image](https://user-images.githubusercontent.com/85903942/233814541-4945d569-936c-4efa-bab8-04a52fecd5cc.png)

6. Click on the Microsoft Edge browser icon in the taskbar
7. Navigate to http://localhost and you should see WordPress.
8. Let's launch two more containers based on "msftcontainers/wordpress" image. Execute following commands (one line at a time)
> docker run -d -p 8080:80 msftcontainers/wordpress
> docker run -d -p 9090:80 msftcontainers/wordpress

9. Run docker ps to see all 3 running containers and their port numbers:
![image](https://user-images.githubusercontent.com/85903942/233814561-a5d93bc7-5ba5-4520-8127-2e5cf61526dc.png)

10. Now open a new browser window and navigate to URL (using DNS or IP as before) but with port
"8080" append to it. You can also try port "9090".

> **Note:** Notice that you now have three WordPress blog instances running inside separate
> containers launched within few seconds. Contrast this to instead creating and running
> WordPress on virtual machine, which could take significantly more time.

![image](https://user-images.githubusercontent.com/85903942/233814570-d3ad0f6b-1116-43ec-842a-fbda80f6bc17.png)

11. If you want to run a container with a name, you can specify the parameter like this:
> docker run \--name mycontainer1 -d -p 8081:80 msftcontainers/wordpress
> Note: Run this on port 8081 so that it does not conflict with one of the previously running containers.

12. And, now if you run docker ps, you will see that the container has the name you assigned it using the
"--name parameter".
> **Knowledge:** 
> Now, let's learn about common Docker commands needed to work with containers. To
> see a comprehensive list of docker commands available **
> (https://docs.docker.com/engine/reference/commandline/docker) **

![image](https://user-images.githubusercontent.com/85903942/233814601-828ebd9c-3763-42f1-9e16-296ddfc60f2d.png)

