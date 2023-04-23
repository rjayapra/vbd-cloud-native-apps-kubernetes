## Building and Running Node.JS Application as Container

In this task you will create a new image based on the Node.js base image. You will start with a Dockerfile with
instructions to copy the files needed to host a custom Node.js application, install necessary software inside
the image and expose ports to allow the traffic. Later, you will learn how to build the image using Dockerfile
and finally will run and test it out.

> **Note:** The relevant files related to a node.js application along with the Dockerfile are available inside the
directory "labs/module1/nodejs".

1. From Windows Terminal navigate to the directory:

> cd C:\labs\module4\nodejs

2. On the command prompt type **ls** and press Enter.
> **Note:** Notice the available files include "server.js", "package.json", "Dockerfile" and
".dockerignore".

![image](https://user-images.githubusercontent.com/85903942/233815186-cf05d20e-d11a-4fac-9a7e-35add39b1403.png)


3. Let's examine the Dockerfile by typing the command code Dockerfile and press Enter.
> Note: The file is case sensitive, so make sure the D in Dockerfile is capitalized.
> Note: Dockerfile will open in Visual Studio Code for editing. Notice the structure of Dockerfile.

![image](https://user-images.githubusercontent.com/85903942/233815191-8e23c96c-7d7e-4100-bb21-e4154d78d423.png)


4. Find the line starting with LABEL author="sampleauthor@contoso.com" and change the email
address to your own email. Once you finish making changes press save the file by pressing CTRL+S,
and close the Visual Studio Code.

You are now ready to build a new image based on the Dockerfile you just modified.

5. Run the command docker build -t mynodejs .
> **Alert:** Pay close attention to the period that is at the end of command.
> **Knowledge:** Notice how the build command is reading instructions from the Dockerfile staring
from the top and executing them one at a time. This will take a few minutes to pull the image
down to your VM.

![image](https://user-images.githubusercontent.com/85903942/233815239-e25524fe-4e39-4fbb-af68-fefb11022743.png)

> **Alert:** When it is complete, you might see a couple of npm warnings, you can ignore this.

6. Run the command
**docker images**

and notice the new container image appears with the name "mynodejs". Also notice the presence of
parent image "node" that was also pulled from Docker Hub during the build operation (if you were
using the provided lab machines, they may have already been cached locally).

![image](https://user-images.githubusercontent.com/85903942/233815244-c60f7bb2-8b5a-43db-b42c-1504f9acd90a.png)


7. Finally, lets create and run a new container based on "mynodejs" image. Run command
**docker run -d -p 8080:8080 mynodejs**

> **Knowledge:** The -d parameter will run the container in the background, whereas the -p
parameter publishes the container port to the host.
> **Note:** Here, we are binding the port of the container (port number on right-side of colon) to the
port of the host machine (port number on the left-side of the colon).

![image](https://user-images.githubusercontent.com/85903942/233815248-1c901c0e-350d-4607-a0a3-88255499949f.png)

8. To test the "mynodejs" application, go back to your Microsoft Edge browser and go to **localhost:8080.**
Congratulations!

You have successfully completed this topic. Click Next to advance to the building Nginx container image

---

## Building and Running NGINX Container

In this task you will create a new image using the NGINX web server base image hosting a simple static html
page. You will start with a Dockerfile with instructions to define its base image, then copy the static html file
inside the image and then specify the startup command for the image (using CMD instruction). Later, you will
learn how to build the image using Dockerfile and finally will run and test it out.

> **Note:** The relevant files including static html file index.html along with the Dockerfile are available
inside the directory labs/module4/nginx.

1. From Windows Terminal navigate to the directory:
**cd C:\labs\module1\nginx**

2. Type **ls** and press Enter. Notice the available files include "index.html" and "Dockerfile".

3. Let's examine the Dockerfile by typing the command code Dockerfile and press Enter.
> **Knowledge:** You can use any other text editor, but instructions are provided for Visual Studio Code.
**Note:** Notice the structure of Dockerfile.
![image](https://user-images.githubusercontent.com/85903942/233815365-7a66cba2-8c59-4c54-beef-0e0836f9d94f.png)


4. Find the line starting with LABEL author="sampleauthor@contoso.com" and change the email
address to your own email. Once you finish making changes press save the file by pressing CTRL+S,
and close the Visual Studio Code.

5. You are now ready to build a new container image based on the Dockerfile you just modified.
Run the command
**docker build -t mynginx .**

![image](https://user-images.githubusercontent.com/85903942/233815361-643895ec-2238-4408-bf80-c015775a4f93.png)

> Note:Notice how the build command is reading instructions from the Docker file starting from
the top and executing them one at a time. The image will download much faster as this is a very
small image.

6. If you want to see the layers of an image, you can do
**docker history mynginx**
![image](https://user-images.githubusercontent.com/85903942/233815354-af66264d-a8aa-40a7-843e-df704b2fd90b.png)


and see the one you just built. You can also try running this command on other images you have on your VM too.

7. Run the command
**docker images
> **Note:** Notice the new container image appears with the name mynginx. Also notice the presence
of parent image nginx that was pulled from Docker Hub during the build operation. Take a look
at the sizes of different images also. This will become important when you build your own
custom images to reduce the size for both security and performance.

![image](https://user-images.githubusercontent.com/85903942/233815340-abfbab8a-e6a9-4b72-b20f-e3004d3fe5fd.png)

8. Finally, create and run a new container based on "mynginx" image. Run command
**docker run -d -p 80:80 mynginx

![image](https://user-images.githubusercontent.com/85903942/233815335-976592f9-b43d-410a-845f-3c09a3af08e2.png)

9. To test the node app, go to your Microsoft Edge browser and go to localhost.

Congratulations!
You have successfully completed this topic. 

---


## Building and Running ASP.NET Core Web Application inside of a Container

In this task you will build ASP .NET Core Web Application and then package and run it as a container.
1. Change to the relevant directory

**cd C:\labs\module1\aspnetcore-linux**

2. First, we need to run dotnet build, and publish to generate the binaries for our application. This can be
done manually or by leveraging a Dockerfile. In this example, we will run the commands manually to
produce the artifacts in a folder called published. The Dockerfile will only contain instructions to copy
the files from the published folder into the image. Later on in the lab we will learn how we can take an
advantage of multi-stage Dockerfiles to achieve the same result.

3. Run following commands to build and publish your .NET code
**dotnet build**
![image](https://user-images.githubusercontent.com/85903942/233815463-b08de0a6-44c0-4c2f-8739-1336cb597e5d.png)


**dotnet publish -o published**

![image](https://user-images.githubusercontent.com/85903942/233815451-32527379-86d0-4fce-8441-e96d794e7d9b.png)


4. Now that the application is ready, you will create your container image. The Dockerfile is provided to
you. View the content of Dockerfile by running the following command:
**code Dockerfile**
> **Note:** The Dockerfile contents should match the screenshot below:
 **Note:** After reviewing a file, you can close Visual Studio Code.

![image](https://user-images.githubusercontent.com/85903942/233815446-b77424d4-9752-4078-a1fc-c53d54419699.png)

5. To create the container image run the command
**docker build -t myaspcoreapp:6.0 .**
> **Note:** Notice the 6.0 tag representing the dotnet version.

![image](https://user-images.githubusercontent.com/85903942/233815440-66038aec-770d-4b5c-9f89-657859c31f05.png)

6. Launch the container running your application using the command
**docker run -d -p 8090:80 myaspcoreapp:6.0**

![image](https://user-images.githubusercontent.com/85903942/233815437-4851a6d5-737a-4051-a82c-41a67b2f9d09.png)

> **Note:** You are now running ASP.NET Core application inside the container listening at port 80
which is mapped to port 8090 on the host.

7. To test the application, go to localhost:8090 in your Microsoft Edge browser.
Congratulations!

