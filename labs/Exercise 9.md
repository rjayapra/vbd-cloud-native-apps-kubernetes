## Advanced Container Topics

### Exercise 9: Working with Data Volumes
---

In this exercise, you will learn how to mount a host directory as a data volume. The host directory will be
available inside the container along with all the files (and sub directories).
Later you will update a file on the host shared through a data volume from within the container. Remember
that by default, data volumes at the time of mounting are read/write (unless you choose to only enable them
for read only access).

Docker containers, by design, are volatile when it comes to data persistence. This means that if you remove a
container, for example, using docker "rm command, all the data that was in the container (running or
stopped) with be lost. This certainly causes a challenge for applications that are running in the container and
need to manage state.

A good example here would be a SQL Server Database file from a previous lab that is required to be persisted
beyond the life of the container running the SQL engine. The solution to this problem is to use data volumes.
> Hint:Data volumes are designed to persist data, independent of the container's lifecycle.
Volumes are initialized when a container is created. Some of the key characteristics of volumes are listed
below:

Data volumes can be shared and reused among containers.
Changes to a data volume are made directly by the container or the host.
Data volumes persist even if the container itself is deleted.

> Knowledge:Docker never automatically deletes volumes when you remove a container nor will it
"garbage collect" volumes that are no longer referenced by a container. This means you are
responsible for cleaning up volumes yourself.

### Mount a host directory as a data volume
---

1. Open your Windows Terminal and navigate to your C:\ drive:
```cd C:\\```

2. Create a directory on the host operating system called host-data. To create a new directory on the C
drive run the following command
```mkdir host-data```

3. Display the contents of the folder you just created in previous step by running the command
```ls host-data.```
It is currently empty.

4. You are now ready to run a container in the interactive mode and mount the host directory as a data
volume. Run the command
```docker run -it -v C:/host-data/:C:/container-data/ mcr.microsoft.com/windows/nanoserver:20H2 cmd```
![image](https://user-images.githubusercontent.com/85903942/233837878-64f1fbbc-7769-4f53-bd82-da622b911cf3.png)

> Knowledge: Notice the -v switch that is required to mount the host directory C:\host-data
inside the container as C:\container-data. This will result in container access to contents of
C:\host-data on the host inside the container as C:\container-data.

> Hint:You can choose same name for the directory inside the container and host but it's not
mandatory. We just used the prefix of host and container to help you visualize how the
directories are bound.

5. On the container Windows Terminal first check the hostname by running the command
hostname.
> Note:The actual hostname for your container may be different than pictured below. Most
importantly though, the container hostname will be different from you VM hostname.

![image](https://user-images.githubusercontent.com/85903942/233837895-ae962c7c-c67b-47bc-9230-2c2f58b7a99d.png)

6. List the directories by running the command
**dir.**
![image](https://user-images.githubusercontent.com/85903942/233837902-ca8c9059-dfab-43fc-8436-479735cb576e.png)

> Note:Notice the container-data directory as part of the listing.

7. Create a file in the folder and add more text to it. Run the command:
```echo File is created by container: %COMPUTERNAME% >> c:\container-data\file.txt```
![image](https://user-images.githubusercontent.com/85903942/233837914-724a6e3a-0c61-4e89-9f1a-cdd83ca48fcc.png)

> Note: %COMPUTERNAME% is equivalent to hostname

8. You can access and update the content of the container-data directory. First, run the command
```dir c:\container-data```
![image](https://user-images.githubusercontent.com/85903942/233837939-7caf6af1-d394-47c2-89d9-e71dcc54ec7f.png)

> Knowledge: This will list the content structure residing inside the container-data directory.

> Note:Notice file.txt is present inside the container-data directory. This is the same file you
created earlier on the host.

9. Look at content inside the file.txt by running the command
```more c:\container-data\file.txt```

10. You can now exit the container and return to host by running the command
```exit```
You should be back at the host machine prompt.

11. On the host Windows Terminal run the command
```gc C:\host-data\file.txt.```

![image](https://user-images.githubusercontent.com/85903942/233837963-61daf0e6-c506-4216-9bc9-de7610b4755b.png)

> Knowledge: gc stands for Get-Content.
Note:Notice that changes made from the container persist on the host by the file.txt.

12. Run following command to get the ID of stopped containers
```docker ps -a```
Record the container ID in the following text box:
> Alert: Your Container ID will be different than one on the picture. Please type in first few
characters of your container ID.
**Container ID**
> Note:To gather more information about container and volumes that has been mounted you can
run the command docker inspect <ContainerID2>.

13. The docker inspect command outputs a rather large JSON file on the display. You may need to scroll
down to find the section labeled "Mounts".

  ![image](https://user-images.githubusercontent.com/85903942/233837984-e6e41642-4ed4-4acb-958b-543fc5d46fc3.png)

> Note:Notice that c:\host-data is the source and c:\container-data is the destination. Also, RW
refers to Read/Write.

14. Let's run another container in interactive mode and mount the host directory as a data volume. Run the
command
```docker run -it -v C:/host-data/:C:/container-data/ mcr.microsoft.com/windows/nanoserver:20H2 cmd```

15. Look at content inside the file.txt by running the command
```more c:\container-data\file.txt```
  
16. Add more text to it. Run the command:
```echo File is updated by container: %COMPUTERNAME% >> c:\container-data\file.txt```

![image](https://user-images.githubusercontent.com/85903942/233838003-57a5e450-6365-4236-b35d-c2d456715e1e.png)

17. Open a new tab in the Windows Terminal by clicking the + sign in the header
![image](https://user-images.githubusercontent.com/85903942/233838031-bff929b5-db44-4ba6-a379-a556cf261285.png)


> Note:New tab should point to C:\labs folder on your host machine.

![image](https://user-images.githubusercontent.com/85903942/233838045-15a9e893-9526-4ac4-a186-d9a3e1d19bcb.png)

18. Open file.txt with notepad using the following command:
notepad C:\host-data\file.txt

19. Update the content of the file with notepad and save it.
Note: The two different hostnames correspond to the two Ids of the containers that wrote in the file.

20. Close the notepad.

21. Go back to the Windows Terminal and check that the container can see the host changes with the command.

![image](https://user-images.githubusercontent.com/85903942/233838058-9c70b3d3-87cd-44c9-b548-2e940cd4ff8b.png)

Warning: Make sure you are on the right tab in the Windows Terminal. First tab should be your
container interactive session.
more c:\container-data\file.txt

  
### Mount a shared-storage volume as a data volume
---

In the previous section you learn how to mount a directory on the host as a data volume. That's a very handy
way to share the content from host to container but it's not ideal in terms of portability. Basically, if you later
run the container on a different host there is no guarantee that host will have the same directory. This would
cause the app inside the container to break as it depends on a share that is not implemented by the host.

  In cases like these when a higher level of portability is desired, you can mount a shared storage volume.
Docker has some volume plugins that allow you to provision and mount shared storage, such as iSCSI, NFS, or
FC. A benefit of using shared volumes is that they are host-independent. This means that a volume can be
made available on any host on which a container is started as long as the container has access to the shared
storage backend, and has the plugin installed.

  In this exercise, you will learn how to create and use a shared-storage volume. To keep the lab accessible and
easy to follow, you will use the local driver which uses local host for the storage.

  However, the exact same concepts will work against production ready storage drivers like Convoy and others.
For more information on the Convoy volume plugin, please visit: https://github.com/rancher/convoy

1. First, let's create a volume by running the command following command from a Windows Terminal
```docker volume create -d local myvolume```
  
2. You can list all the volumes by running the command
```docker volume ls.```
  
> Note:Notice that myvolume is available as a local driver.
  
3. You can use docker inspect command with the volumes too. Run the command
```docker inspect myvolume```

![image](https://user-images.githubusercontent.com/85903942/233838074-a78a8031-4d5f-44ef-8429-671bbdaac334.png)

> Note: Mountpoint is set at a location on the C drive under the ProgramData\docker folder.
This is the default location for local storage drivers. If you use another commercial storage
driver, the location may be different.
  
4. To launch a container and make that storage volume available inside the container run the command
```docker run -it -v myvolume:C:/container-data/ mcr.microsoft.com/windows/servercore:20H2 powershell ```

> Knowledge:This command is like the command from last section where you shared the host
directory, except that within the -v switch you are using the name of the storage volume rather
than the path to the host directory.

5. On the PowerShell command prompt inside the container list all files and directories by running the command
**ls**
  
6. Notice the container-data directory. You can now add/remove files to it.
7. Let's create a new text file and add text content to it. On the command prompt run the command
```"File created on the host: $(hostname)" >> c:\container-data\sample.txt.```
![image](https://user-images.githubusercontent.com/85903942/233838086-c48c1e6f-28fa-43d6-a2e5-5658112e78d5.png)

8. Confirm that file sample.txt has been created successfully by running the command
```gc c:\container-data\sample.txt```
![image](https://user-images.githubusercontent.com/85903942/233838093-b5c43092-b6c8-4230-82fc-c978c0352bbf.png)

9. Now exit the container by running the command
```exit```
  
This will take you back to Windows Terminal console on the host.
  
10. To check the content of sample.txt file from the host run the command
```gc C:\\ProgramData\\docker\\volumes\\myvolume\\\_data\sample.txt.```

![image](https://user-images.githubusercontent.com/85903942/233838110-e806d59c-1159-49b2-b555-057d8b0a77d1.png)
  
Congratulations!
You have successfully completed this exercise. 
