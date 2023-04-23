## Container Core Concepts

### Exercise 5: Tagging

In this exercise you will learn the role of tagging in container and how to tag new and existing container
images using Docker commands.

### Tagging Existing Container Image
---

In this task you will tag the mynodejs container image with v1. Recall from the last task that currently
this image has the latest tag associated with it. You can simply run docker images to verify that. When
working with container images it becomes important to provide consistent versioning information.

> Knowledge: Tagging provides you with the ability to tag container images properly at the time of
building a new image using the docker build -t imagename:tag . command. You can then refer to the
image (for example inside Dockerfile with FROM statement) using a format image-name:tag.

- If you don't provide a tag, Docker assumes that you meant latest and use it as a default tag for the
image. It is not good practice to make images without tagging them. You'd think you could assume
latest = most recent image version always? Wrong. Latest is just the tag which is applied to an image by
default which does not have a tag. If you push a new image with a tag which is neither empty nor
'latest', :latest will not be affected or created. Latest is also easily overwritten by default if you forget to
tag something again in the future. Careful!!!

1. When you run docker images notice the TAG column and pay attention to the fact that all of the
custom images except .NET 6 app created in the lab so far have tag value of latest.
![image](https://user-images.githubusercontent.com/85903942/233816317-2b4c7fba-515a-46d3-b4fd-ac14cf6e1fe2.png)

- To understand the importance of tagging take a look at the container image created in the previous
section mynodejsv2.
- The v2 at the very end was appended to provide an indicator that this is the second version of the
image mynodejs. The challenge with this scheme is that there is no inherent connection between the
mynodejs and mynodejsv2.
- With tagging, the same container image will take the format mynodejs:v2. This way you are telling
everyone that v2 is different but has relation to the mynodejs container image.
> Note: Tags are just strings. So, any string including v1, 1.0, 1.1, 1.0-beta, and banana all qualify
as a valid tag.
- You should always want to follow consistent nomenclature when using tagging to reflect versioning.
This is critical because when you start developing and deploying containers into production, you may
want to roll back to previous versions in a consistent manner.
- Not having a well-defined scheme for tagging will make it very difficult particularly when it comes to
troubleshooting containers.
> Knowledge: A good example of various tagging scheme chosen by Microsoft with dotnet core
framework is available at: https://hub.docker.com/r/microsoft/dotnet/tags

1. To tag an existing docker image, first run the command to list all the images locally cached, and get the
mynodejs Image ID:
```docker images```
![image](https://user-images.githubusercontent.com/85903942/233816418-ba93b565-3ff8-48de-8fd2-929a4a89d383.png)


2. In the textbox bellow enter either Image ID or simply name of the image - in this case mynodejs
```docker tag <ImageNodeJs> mynodejs:v1```
>Alert: If you have completed previous exercises, image ID will pre-populate in the command above.

3. To see the updated tag for "mynodejs" image run the command
```docker images```

![image](https://user-images.githubusercontent.com/85903942/233816414-e4521e86-ddff-438b-b8df-653f0b24af4a.png)

> Note:Notice how latest and v1 both exist. v1 is technically newer, and latest just signifies the
image that did not have a version/tag before and can feel misleading.

Also, note the Image ID for both are identical. The image and its content / layers are all cached on your
machine.

The Image ID is content addressable, so the full content of it is hashed through a hashing algorithm
and it spits out an ID.

If the content of any two (or more) images are the same, then the Image ID will be the same, and only
one copy of the actual layers are on your machine and pointed to by many different image names/tags.


### Tagging New Container Image
---

Tagging a new image is done at the time when you build a container image. it's a straightforward process that
requires you to simply add the :tag at the end of container image name.

1. Navigate to the directory "labs/module4/nginx" that contains the "nginx" files along with Dockerfile.
You can use the command
```cd c:\labs\module1\nginx```

2. Build a new image by running the command
```docker build -t nginxsample:v1 .```

Note:In this case you're creating a new image based on Dockerfile (covered in earlier exercise on NGINX).

![image](https://user-images.githubusercontent.com/85903942/233816402-f2d0d686-1536-4f5f-aadc-a9391c2a924b.png)

3. Run a command to list all the images
```docker images```

> Note: Notice, there is the new container image with tag "v1"
![image](https://user-images.githubusercontent.com/85903942/233816390-e40dee6e-c512-4068-b4c4-b5ffa490dfdb.png)

