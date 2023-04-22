Exercise 1: Running Your First Container

In this exercise, you will launch a fully functional WordPress blog engine using a Linux-based Docker
container. You will learn the commands needed to pull the container image and then launch the container
using the Docker CLI. Finally, you will observe that by running the container, you don't need to install any of
the WordPress dependencies onto your machine; the WordPress engine binaries and all dependencies will be
packaged inside of the container.

## Running WordPress Blog Engine Container 

1. From your taskbar, click on the Windows Terminal icon to open Windows Terminal.
2. Type  
> docker pull msftcontainers/wordpress

### Knowledge:
This will tell Docker client to connect to public Docker Registry and download the
latest version of the WordPress container image published by msftcontainers (hence the format
msftcontainers/wordpress). The container image has been pre-downloaded for you on the VM
to save you a few minutes, so you will see the Status message as "Images is up to date for
msftcontainers/wordpress:latest".

3. Run the command
> docker images

### Note:Notice "msftcontainers/wordpress" container image is now available locally for you to use.
4. That's it! You can now run the entire WordPress in a container. To do that run the command docker
> run -d -p 80:80 msftcontainers/wordpress
### Note: Pay close attention to the dash "-" symbol in front of "-p" and "-d" in the command.
5. Run the following docker ps to see the running containers.

6. Click on the Microsoft Edge browser icon in the taskbar:
7. Navigate to http://localhost and you should see WordPress.
8. Let's launch two more containers based on "msftcontainers/wordpress" image. Execute following
commands (one line at a time)
> docker run -d -p 8080:80 msftcontainers/wordpress
> docker run -d -p 9090:80 msftcontainers/wordpress
9. Run docker ps to see all 3 running containers and their port numbers:
10. Now open a new browser window and navigate to URL (using DNS or IP as before) but with port
"8080" append to it. You can also try port "9090".

### Note:Notice that you now have three WordPress blog instances running inside separate
containers launched within few seconds. Contrast this to instead creating and running
WordPress on virtual machine, which could take significantly more time.
11. If you want to run a container with a name, you can specify the parameter like this:
> docker run \--name mycontainer1 -d -p 8081:80 msftcontainers/wordpress
### Note:Run this on port 8081 so that it does not conflict with one of the previously running
containers.
12. And, now if you run docker ps, you will see that the container has the name you assigned it using the
"--name parameter".
### Knowledge: 
Now, let's learn about common Docker commands needed to work with containers. To
see a comprehensive list of docker commands available **[click here]
(https://docs.docker.com/engine/reference/commandline/docker) **

1. First list all the containers currently running by executing docker ps command. You should see list of
all running containers.
Note:Notice, the list contains multiple containers based on WordPress image that you ran
previously.
2. You can stop a running container by using docker stop <CONTAINER_ID>" command. Where
CONTAINER_ID is the identifier of a running container.
Note:You can just use the first couple characters to identity the container ID, such as "1c1" from
the sample screenshot above, or you can use the full container ID.
3. Enter the first couple characters of one of the container ID in the textbox bellow. Do not enter 1c1, as
your container ID will most likely start with a different characters.
Container ID
4. Type docker stop <ContainerId1> to stop the container with the ID that starts with ** **
5. Now run the docker ps command and notice the listing show one less container running.
6. If you want see the Container ID of the stopped container, and you forgot the Container ID, you can run
docker ps -a to see all containers, even those that are stopped/exited.
7. You can also issue command to start the container which was stopped. To start a container run docker
start <ContainerId1>.
Tip: This uses the container identifier you use in previous section to stop the container.
)
8. To make sure that container has started successfully run docker ps command.
Note:Notice that WordPress container is now started.
instructions.md 4/29/2022
67 / 234
9. Stopping a container does not remove it and that's the reason why you were able to start it again in the
previous task.
To delete/remove a container and free the resources you need to issue a different command. Please
note that this command does not remove the underlying image but rather the specific container that
was based on the image. To remove the image and reclaim its resources, like disk space, you'll will need
to issue a different command which is covered later.
10. To remove a container, run docker rm -f <ContainerId1> command.
Note:The "-f" switch is used to force the remove operation. It's needed if you are trying to
remove a container that is not already stopped.
11. To validate that container has been removed completely run docker ps -a command.
Note:Note that container starting with the ID is not in the list.
12. At times you may want to stop all of the running containers and avoid issuing command to stop one
container at a time. Run docker stop $(docker ps -aq) command to stop all running containers.
Basically, you are issuing two commands: First the docker ps with relevant switches to capture list of
container IDs and then passing list of IDs to docker stop command.
13. Now that we stopped all the containers, we can remove the image (msftcontainers/wordpress) used
to run the containers. Removing a container image form a local system will let you reclaim its disk
space.
Note:Please note that this operation is irreversible so proceed with caution. In this task you will
remove the WordPress container image as you will not be using it any more. You must stop all
containers using the image before you can delete the image, unless you use the force
parameter.
instructions.md 4/29/2022
68 / 234
14. To remove a container image, you'll need its IMAGE ID. Run command docker images
15. Enter the Image ID in the textbox bellow:
16. Run the command docker rmi <ImageId1> -f
Note:Notice the command to remove docker container is "docker rm" and to remove an image
is "docker rmi", with an 'i' for the image. Don't confuse these two commands! The -f is to force
the removal, you cannot remove an image associated with a stopped container unless you use
the force parameter.
17. Now, run the command docker images
Note:Notice that "msftcontainers/wordpress" image is no longer available.
Congratulations!
You have successfully completed this exercise