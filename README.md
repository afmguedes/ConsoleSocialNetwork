# ConsoleSocialNetwork

Console-based social network application that responds to a set of command (posting messages, reading user posts, following other users, reading user wall)

## Commands Accepted

Here you can find instructions on the commands available in this application.

### Posting

```<UserName> -> <Message>```

A user can post messages to the wall via command line.

If the user doesn't already exist, it will be created with the first post.

```
> Andre -> Great sunset today!
> Graziano -> Having a great time in London.
```

### Reading

```<UserName>```

A user can read someone's posts, ordered by date from the most recent to the oldest.

```
> Andre
Great sunset today! (2 minutes ago)
```

### Following

```<UserName> follows <UserName>```

A user can follow another user and get the followed user's messages on the wall.

```
> Andre follows Graziano
```

### Wall

```<UserName> wall```

A user can read someone's wall. The wall agregates all the users posts and the posts of the followed users. All the posts are ordered by date from the most recent to the oldest.

```
> Andre wall
Graziano - Having a great time in London. (5 minutes ago)
Andre - Great sunset today! (7 minutes ago)
```

### Logout

```Logout```

To exit the application simply type "Logout".

```
> Logout


Good bye!
```

## Deployment

Instructions on how to deploy and run the application.

Please make sure you have .NET Core 1.0.1 installed.

Get the solution from this repository.

### From Visual Studio

Open the solution on Visual Studio 2015.

Press Crtl+F5.

### From the Command Prompt

Navigate to the solution folder.

Run the application with the following command:

``` dotnet src\ConsoleSocialNetwork\bin\Release\PublishOutput\ConsoleSocialNetwork.dll ```
