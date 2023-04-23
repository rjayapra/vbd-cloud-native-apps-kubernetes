### Interaction with a Running Container
---

In the previous exercise, you were able to build and run containers based on Dockerfiles. However, there may
be situations that warrant interacting with a running container for the purposes of troubleshooting,
monitoring etc. You may also want to make changes/updates to a running container and then build a new
image based on those changes. In this exercise, you will interact with a running container and then learn to
persist your changes as a new image.

1. On the command line run
```docker ps```

to list all the currently running containers on your virtual machine.
> Note:Notice that multiple containers are running. To establish interactive session a with a
running container you will need its CONTAINER ID or NAME. Please note that your
CONTAINER ID or NAME will probably be different. And, unless you specified a name, Docker
came up with a random adjective and noun and smushed them together to come up with its own clever name.

2. Let's establish an interactive session to a container based on "mynodejs" image. Type first few
characters of the mynodejs container id in the textbox bellow:
> Container ID

3. Run a command
```docker exec -it <ContainerId2> bash```

>Knowledge: docker exec is used to run a command in a running container. The it parameter will
invoke an interactive bash shell inside the container.
> Note:Notice that a new interactive session is now establish to a running container. Since "bash"
is the program that was asked to be executed you now have access to full bash shell inside the
container.

![image](https://user-images.githubusercontent.com/85903942/233816165-23186dea-cf11-43fc-b6a0-7483059a3be4.png)

4. You can run a command ls to view the listing of files and directories.
Note: Notice it has all the files copied by Dockerfile command in previous section.
Knowledge: Additional information regarding running commands inside docker container

![image](https://user-images.githubusercontent.com/85903942/233816169-e7b8fad9-3908-4884-aa03-7ecd43feffe4.png)

Congratulations!
You have successfully completed this topic. Click Next to advance to make changes to a running container.
  
 
### Making Changes to a Running Container
---
 
While you are interacting and running commands inside a running container, you may also want to make
changes/updates to it. Later, you may also create a brand-new image out of these changes. In this task you
will make changes to "mynodejs" container image, test them out and finally create a new image (without the
need of Dockerfile).

> Note:Please note that this approach of creating container images is generally used to quickly test
various changes, but the best practice to create container images is to use a Dockerfile since it is a
declarative file that can be kept in source control repositories.

1. First, you will make updates to server.js file. You should have an active session already established from
previous exercise (if not then please follow the instructions from the previous section to create an active
session now).

2. Before we can edit the server.js file we need to install a text editor. To keep the size of container to a
minimum, the nodejs container image does not have any extra software installed in the container. This
is a common theme when building images and is also the recommend practice.

3. Before installing any software run the command
```apt-get update```

![image](https://user-images.githubusercontent.com/85903942/233816173-888a961d-c42d-4303-ad04-5abcffc7a2cb.png)

> Warning:Note the dash between "apt" and "-get".
4. To install "nano" run a command
```apt-get install nano```

![image](https://user-images.githubusercontent.com/85903942/233816176-0bc09c5e-5ac7-4080-92bc-4e27d13d2a6b.png)

5. After "nano" is installed successfully, run the command nano server.js to open "server.js" file for editing.
![image](https://user-images.githubusercontent.com/85903942/233816181-1647afea-8670-4847-bd96-3cbed66c5619.png)


6. Use the arrow keys to go to the line starting with "res.Send(..." and update the text from "Hello World
From Container!" to "Hello AGAIN!".
> Note:Your final changes should look like following:

![image](https://user-images.githubusercontent.com/85903942/233816186-80f90bd1-110d-480e-8bf2-ee2802b7a982.png)

7. Once you finish making changes press "CTRL + S" to save the file and then press "CTRL + X" to exit.
This will close the Nano text editor.

8. To save the updates and exit the interactive bash session, run the command
```exit```

9. The running container needs to be stopped first and then started again to reflect the changes. Run the
command
```docker stop <ContainerId2> to stop the container.```

10. Run the command
```docker start <ContainerId2> to start the container.```

11. Finally, to test the update you have made to the container go to Microsoft Edge and localhost:8080.

> Note:Notice the output "Hello AGAIN!". This verifies that changes to the container were persisted.

Congratulations!

You have successfully completed this topic. Click Next to advance to the save running container to a new
image.

---
  
### Saving running container to a new image

In the previous task you have made changes to running container. However, these changes are only available
to that container and if you were to remove the container, these changes would be lost.
One way to address this is by creating a new container image based on running container that has the
changes. This way changes will be available as part of a new container image.

This is helpful during dev/test phases, where rapid development and testing requires a quick turnaround time.
> Knowledge: This approach is generally not recommended, as it's hard to manage and scale at the
production level. Also, if content is the only piece that needs to be changed and shared, then using
volumes may be another viable option. 

1. To create new image run the command
```docker commit <ContainerId2> mynodejsv2```

> Knowledge:The docker commit command is used to create a new image from a container's changes.

![image](https://user-images.githubusercontent.com/85903942/233816193-17067f62-1a34-449e-8604-4f5f437b642d.png)

2. Now, view the list of all container images by running the command
```docker images```
> Note:Notice the availability of new image with name "mynodejsv2"
Note:You now have a container image with the changes you made and tested earlier and is
ready to be used.

![image](https://user-images.githubusercontent.com/85903942/233816200-4d82d5c8-54d3-4122-86e5-87b5a632d11c.png)

3. To test the new image run a command
```docker run -d -p 8081:8080 mynodejsv2```
> Note:This will create a new container based on the image "mynodejsv2".

![image](https://user-images.githubusercontent.com/85903942/233816202-d2e47efa-aecc-4270-aef3-faefba8998df.png)

4. Finally, to test the container, go to localhost:8081 in Microsoft Edge.
>Note:Notice the text "Hello AGAIN!" is returned from the node.js application. This attest that
changes were committed properly to new image and hence available to any container created
based on the that image.

Congratulations!
You have successfully completed this exercise. Click Next to advance to the next exercise.
