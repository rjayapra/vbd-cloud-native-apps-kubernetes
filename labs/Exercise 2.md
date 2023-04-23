## Docker commands 

1. First list all the containers currently running by executing **docker ps** command. You should see list of
all running containers.

> **Note:** Notice, the list contains multiple containers based on WordPress image that you ran
previously.

![image](https://user-images.githubusercontent.com/85903942/233814723-878f3772-d0d7-4713-bb08-2976a09fa643.png)

2. You can stop a running container by using **docker stop <CONTAINER_ID>** command. Where
CONTAINER_ID is the identifier of a running container.

>**Note:** You can just use the first couple characters to identity the container ID, such as "1c1" from
the sample screenshot above, or you can use the full container ID.

3. Enter the first couple characters of one of the container ID in the textbox bellow. Do not enter 1c1, as
your container ID will most likely start with a different characters.
**Container ID**
4. Type **docker stop [ContainerId1]** to stop the container with the ID 

5. Now run the **docker ps** command and notice the listing show one less container running.

![image](https://user-images.githubusercontent.com/85903942/233814880-38984196-fff5-4806-882b-ce8f58b825fb.png)

6. If you want see the Container ID of the stopped container, and you forgot the Container ID, you can run
**docker ps -a** to see all containers, even those that are stopped/exited.

![image](https://user-images.githubusercontent.com/85903942/233814887-8489ee88-d649-4540-a58a-52b75e874349.png)

7. You can also issue command to start the container which was stopped. To start a container run 
**docker start [ContainerId1]**

![image](https://user-images.githubusercontent.com/85903942/233814899-71dc1d3c-2d48-48f3-904e-ffa5c948a31d.png)
  
> **Tip:** This uses the container identifier you use in previous section to stop the container.

![image](https://user-images.githubusercontent.com/85903942/233814909-216fa83d-f633-464a-aef1-41e9b7d6b206.png)

8. To make sure that container has started successfully run docker ps command.
> **Note:** Notice that WordPress container is now started.

![image](https://user-images.githubusercontent.com/85903942/233814927-96843bd8-0071-4a7b-a869-d59ee4fd8084.png)

9. Stopping a container does not remove it and that's the reason why you were able to start it again in the
previous task.

To **delete/remove** a container and free the resources you need to issue a different command. Please
note that this command does not remove the underlying image but rather the specific container that
was based on the image. To remove the image and reclaim its resources, like disk space, you'll will need
to issue a different command which is covered later.

10. To remove a container, run **docker rm -f [ContainerId1]** command.
![image](https://user-images.githubusercontent.com/85903942/233814933-06f85751-40b9-4429-a09d-357081ec8d48.png)

> **Note:** The "-f" switch is used to force the remove operation. It's needed if you are trying to
remove a container that is not already stopped.

11. To validate that container has been removed completely run docker ps -a command.
> **Note:** Note that container starting with the ID is not in the list.

![image](https://user-images.githubusercontent.com/85903942/233814938-9fd9567c-128a-40a4-ba73-b6efba9c69c1.png)


12. At times you may want to stop all of the running containers and avoid issuing command to stop one
container at a time. Run docker stop **$(docker ps -aq)** command to stop all running containers.
Basically, you are issuing two commands: First the docker ps with relevant switches to capture list of
container IDs and then passing list of IDs to docker stop command.
![image](https://user-images.githubusercontent.com/85903942/233814944-c6a93ac0-9061-40b6-babb-c908a4e831fc.png)

13. Now that we stopped all the containers, we can remove the image (msftcontainers/wordpress) used
to run the containers. Removing a container image form a local system will let you reclaim its disk
space.

> **Note:** Please note that this operation is irreversible so proceed with caution. In this task you will
remove the WordPress container image as you will not be using it any more. You must stop all
containers using the image before you can delete the image, unless you use the force
parameter.

14. To remove a container image, you'll need its IMAGE ID. Run command **docker images**

![image](https://user-images.githubusercontent.com/85903942/233814959-341d730d-7722-4c85-b1f4-10279e0f5cda.png)

15. Enter the Image ID in the textbox bellow:
16. Run the command docker rmi <ImageId1> -f
> **Note:** Notice the command to remove docker container is "docker rm" and to remove an image
is "docker rmi", with an 'i' for the image. Don't confuse these two commands! The -f is to force
the removal, you cannot remove an image associated with a stopped container unless you use
the force parameter.

![image](https://user-images.githubusercontent.com/85903942/233814973-7072ee75-0337-4dd4-88dc-4c9f573bae48.png)

17. Now, run the command docker images

> **Note:** Notice that "msftcontainers/wordpress" image is no longer available.

![image](https://user-images.githubusercontent.com/85903942/233815074-7c01ba7b-bbfe-4cfc-81f0-8888aae00677.png)

Congratulations!
You have successfully completed this exercise 2.  
