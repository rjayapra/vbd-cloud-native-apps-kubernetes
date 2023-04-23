## Advanced Container topic
### Exercise 10 : Docker Networking
In this exercise, you will work with various PowerShell and Docker CLI commands to view Docker default
networks and create a custom nat network. Finally, you will remove the custom nat network and observe how
Docker responds by creating a new default nat network automatically.

### Display all Docker networks
You can retrieve container networks using the Docker CLI.

1. Docker provides native docker command that provides list of networks available to docker. To view the
list of networks available to docker run the command
```docker network ls.```

> Knowledge: The 'nat' network is the default network for containers running on Windows. Any
containers that are run on Windows without any flags or arguments to implement specific network 
configurations will be attached to the default 'nat' network, and automatically assigned
an IP address from the 'nat' network's internal prefix IP range. The default nat network also
supports port forwarding from container host to internal containers. For example, you can
simply run SQL Server Express in a container by providing the "p" flag so that specified port
numbers will be mapped from host to container.

2. To view detail information about the Docker default nat network, run the command
```docker inspect nat```

> Note:Notice the output is in JSON format. The "Containers" key (which is empty in this case)
refers to all containers that are using the specified network. The containers key is empty in this
case because there are no containers currently running.

3. Run **ipconfig** to see the two networks: the physical network, localdomain, and the local container network, nat.

4. Launch a new container by running a command
```docker run -d mcr.microsoft.com/windows/nanoserver:20H2 ping -t localhost```

Once the container is running execute the command
```docker inspect nat```

> Note:Notice that this time the "Containers" section now includes information pertaining to the
container that is using the nat network including its ID and IPv4 address

Congratulations!
You have successfully completed this topic. Click Next to create a custom network.

### Create a custom Docker nat network
Docker allows you to create custom nat networks. In this task, you will create and configure a custom nat
network replacing the default nat network.

1. Create a new docker network by running the command
```docker network create -d nat --subnet=192.168.15.0/24 --gateway=192.168.15.1 custom-nat```

> Note:The "d" flag stands for network driver and specifies the network type you want to create
which in this case is "nat". You are also providing the IP prefix and gateway address using the -
subnet and -gateway flags.

2. To validate newly created network custom-nat run following command:
**+++docker network ls+++**
To use the new custom nat network for containers launch a new container by using the command
```docker run -d --network=custom-nat mcr.microsoft.com/windows/nanoserver:20H2 ping -t localhost```

![image](https://user-images.githubusercontent.com/85903942/233838967-5f9b6ff8-dbb9-4727-ae89-0bdefd1bb5cd.png)

> Note:Notice the use of --network switch which allows you to force docker to use specific
network for the container.

4. Now, use the docker inspect command to get the detailed information about custom-nat network
and container(s) that is using it.

**+++docker network inspect custom-nat+++**

> Note: Notice the subnet and gateway values reflect the values you used earlier during the
creation of the network. Also note that the container's IPv4 Address, 192.168.15.224 (may be
different in your case), is in the custom-nat network.

![image](https://user-images.githubusercontent.com/85903942/233838953-fb2d6699-2c9f-468e-86e3-d1c33b8ca4a0.png)

5. Enter the Container IPv4 Address in the area bellow:
![image](https://user-images.githubusercontent.com/85903942/233838936-9485a061-2894-4b9d-b8d4-9276305cefb7.png)

6. To confirm that the container host and access container run the command
```ping <containerip>```

![image](https://user-images.githubusercontent.com/85903942/233838877-ebe0e477-c186-47fe-a3d8-92510a1e2151.png)

> Note: You can look for container's IP Address in the output from previous command. Notice that
the host can successfully access the container using its IP.

7. Now let's start a new container in interactive mode on the nat network and open a command prompt
```docker run -it --network=nat mcr.microsoft.com/windows/nanoserver:20H2 cmd```

![image](https://user-images.githubusercontent.com/85903942/233838862-8f9fb5ab-f8f6-41eb-9d42-4e0d8c104fe5.png)

8. We can try to ping the previous container with
```ping <containerip>```

![image](https://user-images.githubusercontent.com/85903942/233838843-839e135e-5c94-4636-95db-0842bf808935.png)

> Note: Because the two containers are on separated networks, they cannot ping each other using
their IP Address.

9. To validate that IP address belongs to the nat network, run following command from container:
```ipconfig```

To go back to the host run:
```exit```
![image](https://user-images.githubusercontent.com/85903942/233838805-ad4832ce-6b08-4c86-a764-f9c39457759a.png)

  Remove all containers so that you can then remove the custom network you have created (if containers
are still attached to the network, the network deletion will fail).
```docker rm (docker ps -aq) -f```

11. You may now remove the custom-nat network
```docker network rm custom-nat```
![image](https://user-images.githubusercontent.com/85903942/233838761-648e2d1d-e02b-4246-b50a-f0c3cb4545ae.png)


12. Check that only nat network remains
```docker network ls```
![image](https://user-images.githubusercontent.com/85903942/233838745-7a07a029-5bd9-4b34-b290-e4e3fd807185.png)

Congratulations!
  

