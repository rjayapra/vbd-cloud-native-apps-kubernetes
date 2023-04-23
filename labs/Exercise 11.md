## Advanced Container topic
### Exercise 11 : Docker Compose

### Challenges with Multi-Container Applications
When working with multi-containers applications, you will need to make sure that applications can discover
each other in a seamless fashion. Consider a quite common scenario where a web application (that acts as a
front-end) calls to a backend RESTful web API to fetch content. In this scenario, the web app would need to
access the web API in a consistent fashion. In addition, due to the fact the web application has a dependency
on the web API, that dependency must be expressed when launching the applications in containers. It is
imperative that we are able to launch and test a multi-container application the same way across
development, test and production environments.

> Hint: Docker has provided a tool called "docker-compose" that enables you to describe your
applications as services within a YAML file, docker-compose.yml.

A service in this context really means, "a container in production". A service only runs one image, but it
codifies the way that image runs - what ports it should use, how many replicas of the container image should
run (so that the service has the capacity it needs), and so on. Scaling a service increases the number of
container instances running the application image, assigning more computing resources to that service in the
process.

### Working with Docker-Compose
In this exercise, we will take a look at already generated Docker-compose.yaml file that was generated in the
first lab. This file is composed of all services for the Auction app. All of the microservices in the Auction sample
app are running in the separate containers. We will use the benefit of docker compose to spin the entire
application all at once.

As this is a multi-container scenario, you will use a docker-compose file to:
- Ensure that services can communicate with each other without using their FDQN or IP Address. Instead
of hardcoding the IP Address (or FDQN) you can use a docker-compose.yml file to make these
services discoverable
> Note: Recall from the previous lab where a web application needed to access SQL Server running
in a separate container. In that situation, we provided the web application the IP Address of the
container running SQL Server in the Program.cs configuration file.

- Express specific dependencies with depends on section. This will ensure the order by which the services
are being run.

- Get all application services up and running in separate containers with a single command (i.e., without
using individual docker-run commands for each container).

> Alert: IMPORTANT: Following exercises will focus back on Linux Containers. To work with Linux
Containers we have to change tell Docker to point to the Linux. Steps bellow must be completed
before continuing with the exercises.

1. Locate Docker Icon in your Taskbar
> Note:You might have to click on the ^ icon first to get to Docker Icon

2. Right click on Docker Icon and click on Switch to Linux Containers.
![image](https://user-images.githubusercontent.com/85903942/233840148-010bb904-de5e-4aa2-a134-03117fe5777e.png)

> Alert: You might get a message that existing linux containers will continue to run, click the switch
button to move on. If you did not get a message ignore this step. 
![image](https://user-images.githubusercontent.com/85903942/233840130-decc5b9b-f801-45dd-a793-9d53cd835ab7.png)


3. It might take few minutes for switch to complete and you will see the Docker Icon moving at the
moment. Once the Docker Icon stops moving, navigate to Windows Terminal and type following to
validate that you switched.

```docker version```

![image](https://user-images.githubusercontent.com/85903942/233840109-0e8702e9-a5d9-461b-853a-207b5c689075.png)


### Reviewing and understanding the docker-compose file

1. Launch the Windows Terminal (if not already running) and change your current directory to
"c:\labs\oas\src" folder by running the command
```cd c:\labs\oas\src```

 Before proceeding further let's remove all the running containers from previous tasks. Run the command
```docker rm (docker ps -aq) -f```

> Note: If you are getting a warning message that you need to pass an argument, you do not have
any containers running. Go to the next task.

3. First, lets open up this directory in Visual Studio code. Run the following command:
```code .```

4. You should see all of your microservices in the Explorer inside separate folders, 
and also Dockercompose.yaml file (you might need to collapse the folders to reflect the folders to reflect to the image bellow).

![image](https://user-images.githubusercontent.com/85903942/233840044-fb89e5aa-9deb-476e-b0e3-eca6bbe13377.png)

5. Click on the Docker-compose.yaml file to open it up in Visual Studio code
![image](https://user-images.githubusercontent.com/85903942/233840029-9f97004c-1401-4159-80ae-00ad165af54c.png)

A Compose file MUST declare a services root element as a map whose keys are string representations
of service names, and whose values are service definitions. A service definition contains the
configuration that is applied to each container started for that service.
> Knowledge: A Service is an abstract definition of a computing resource within an application
which can be scaled/replaced independently from other components. Services are backed by a
set of containers, run by the platform according to replication requirements and placement
constraints. Being backed by containers, Services are defined by a Docker image and set of
runtime arguments. All containers within a service are identically created with these arguments.

6. First service listed in docker-compose file is oasapp. This is a front end application.
![image](https://user-images.githubusercontent.com/85903942/233839942-31285914-35f2-420b-9085-053b777e60e2.png)
![image](https://user-images.githubusercontent.com/85903942/233840010-7cca50bd-7a93-4b5e-9d82-ab23a2e10f68.png)


There are certain specifications being set in this section:
- **image** :  specifies the image to start the container from.
- **container_name** : is a string that specifies a custom container name, rather than a generated
default name. We are setting the name of the container to be oasapp hostname declares a custom host 
name to use for the service container. MUST be a valid RFC 1123 hostname.
- **build** : specifies the build configuration for creating container image from source. You will see two
things specified as part of the build (context and dockerfile). Context is required.
  - **context**:  defines either a path to a directory containing a Dockerfile, or a url to a git
repository
  - **dockerfile** :  allows to set an alternate Dockerfile. A relative path MUST be resolved from the
build context
- **depends_on**: expresses startup and shutdown dependencies between services. This is important
to set up properly so that services are built and loaded in proper order so that dependencies are
resolved successfully.
- **ports**: exposes container ports. Port mapping MUST NOT be used with network_mode: host and
doing so MUST result in a runtime error. We are mapping port 4200 to oasapp container.
- **environment**: defines environment variables set in the container. environment can use either an
array or a map. We are using array syntax in our example. Any boolean values; true, false, yes, no,
SHOULD be enclosed in quotes to ensure they are not converted to True or False by the YAML
parser. For our environment variables we are defining the URL addresses of the services front end
would need to talk to.

> Hint:You can see here that we are not using FQDN for the services but service name from the
docker compose file, and docker compose will handle all the wiring behind the scenes.

7. Next service listed in docker-compose file is apigateway.

> Knowledge:The API Gateway sits between the frontend application and the collection of backend
Microservices. An API Gateway acts as a reverse proxy that accepts all the Application
Programming Interface calls (API), aggregates the various services according to the front end
needs, and fulfils the appropriate data

> Note: Notice that apigateway is running on port 5010, and also has many services that are it
depends on being run before docker can start building the container and running the service.

8. There are several services that do not have any dependecies in Docker compose file, like
notificationservice and smtp4dev. These are used for sending out alerts and notifications back to the
end user. These services are being built first with docker compose, as other services might have
dependecies on them.
![image](https://user-images.githubusercontent.com/85903942/233840265-1514585e-59de-4a5f-bb0f-64a4b39ad5c1.png)

> Knowledge: We are using the smtp4dev - the fake SMTP email server for development and
testing. A dummy SMTP server for Windows, Linux, Mac OS-X (and maybe elsewhere where .NET
Core is available). Lets you test your application without spamming your real customers and
without needing to set up a complicated real email server with a special configuration. Messages
received in smtp4dev can be viewed and inspected.

9. Core services are also without any additional dependencies and will build first. These are identity,
auction, bid and payment services. As we seen earlier, API Gateway service depends on these services
to be ready before building the api gateway service itself.

These services might also have data store associated with them, or other configurations needed for the
application. For example, lets take a look at the auction service:
![image](https://user-images.githubusercontent.com/85903942/233840281-3b98ed62-ec6f-4ac2-9939-83c8213e49fe.png)

Auction service has instrumentation key for Applicaiton Insights passed to the container. Also, note that
auction service has MySql database provisioned in Azure, and we are passing the connection string
information to the container.

> Knowledge: Using environment variables we can ensure that we can use the same container in
multiple environments witout altering the container image. Configurations in orchestrated
environments like Kubernetes are done using ConfigMaps and Secrets.

10. Now that we reviewed the Docker-compose.yaml file lets see it in action.

11. Close Visual Studio Code.
Congratulations!

Now lets start application using docker compose up.

  
### Docker Compose Up

docker-compose up is a Docker command to start and run an entire app on a standalone host that contains
multiple services, for example, Web, DB, etc. It can also create volumes and networks at the same time and
attach to the containers that are defined in a file called ‘docker-compose.yml’.
It is used in all environments such as development, staging, and production as well.

1. At this point, you are all set to run the multi-container application with a single command. Open up
Windows Terminal and make sure you are in the c:\labs\oas\src directory
```cd c:\labs\oas\src```

2. List all the files and directories to make sure Docker-compose.yml is there
```ls```
![image](https://user-images.githubusercontent.com/85903942/233840305-07a9adba-b739-4ce2-93a0-fd5f1a057819.png)

3. If you see Docker-compose file in your directory, run the following command to run the multi-container
application:
```docker-compose.exe up -d```
![image](https://user-images.githubusercontent.com/85903942/233840320-898b27e7-44e7-41cb-af5d-657b38849b74.png)

> Knowledge: The docker-compose.exe tries to make it simple to start and stop the services
(running containers) with commands like up and down. The -d switch works the same as when
used with the docker build command, which instructs docker to run the container in the
background rather than interactively. If you don't provide any switch parameter, the default is
set to interactive.

> Note:As the command executes, you will notice that the bidservice, smtp4dev,
paymentservice, auctionservice, identityservice and notificationservice are build first. This is
because we mention in the yml file that apigateway depends on it, so it will build first. Also, if
the images already exists, then it won't build them again

4. You can check details about running docker cmpose services by executing the command
```docker-compose ps```
![image](https://user-images.githubusercontent.com/85903942/233840339-bee6cf1a-7f6d-4cdd-85e9-833ee003c7af.png)


5. Open web browser of your choice and browse to localhost
```start http://localhost:4200.```
![image](https://user-images.githubusercontent.com/85903942/233840354-f8e8bb0e-77be-47a7-8193-8b5ac1813f83.png)

You should land on the home page of web application as shown below.
> Note:To test the application, you can login and create an auction on the web site.

6. Click on Login and enter username as
```testuser@oas.com```
and password as
```password```

7. Click on Create Auction option from the Home page to create auction.

![image](https://user-images.githubusercontent.com/85903942/233840392-f1cdce67-e770-4baf-9f5c-7cb131d4c761.png)


8. Explore the other features of the application. Once you are done, move on to the next exercise.

### Docker Compose Down

When you wish to stop and remove the multi-container application that was launched by docker compose,
you will use docker-compose down command. The down command safely stops and removes all the
containers that were launched by the up command earlier using the docker-compose.yml file.

> Knowledge: If you only wish to stop the multi-container applications and associated running containers
use "docker-compose stop" command instead. This command will stop the containers, but won't
remove them.

1. On the Windows Terminal run the command
```docker-compose down```
![image](https://user-images.githubusercontent.com/85903942/233840475-e30e7dad-ce41-416c-9cbf-160e983edd1d.png)

Note: First the containers are stopped and then they are removed.
Congratulations!

  
  
