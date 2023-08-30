# NBA Players Server
NBA Player endPoint to get specific list by CSV file 
This is the back-end server application for the NBA Players project. The server is built using .NET Core and is responsible for handling player data, making API requests, and sending real-time notifications to the client.
## Use Cache
One of the most commonly used patterns in software development is Caching. It’s a simple, but very effective concept. The idea is to reuse operation results. When performing a heavy operation, we will save the result in our cache container. The next time that we need that result, we will pull it from the cache container, instead of performing the heavy operation again.

Caching works great for data that changes infrequently. Or even better, never changes. Data that constantly changes, like the current machine’s time shouldn’t be cached or you will get wrong results.

In-process Cache, persistent in-process Cache, and Distributed Cache
There are 3 types of caches:

- In-memory cache is used for when you want to implement cache in a single process. When the process dies, the cache dies with it. If you’re running the same process on several servers, you will have a separate cache for each server.
- Persistent in-process Cache is when you back up your cache outside of process memory. It might be in a file, or in a database. This is more difficult, but if your process is restarted, the cache is not lost. Best used when getting the cached item is expensive, and your process tends to restart a lot.
- Distributed Cache is when you want to have a shared cache for several machines. Usually, it will be several servers. With a distributed cache, it is stored in an external service. This means if one server saves a cache item, other servers can use it as well. Services like Redis are great for this.
- 
In order to improve the performance of this project use a cache. The type cache is Persistent in-process because I want the file to be most responsible 
## Getting Started

To install those packages using the .NET CLI, you can use the dotnet add package command followed by the package name and version. Open your terminal or command prompt and navigate to your project's root directory. Then, use the following commands to install each package:
## CsvHelper (Version 30.0.1):
dotnet add package CsvHelper --version 30.0.1
## Microsoft.AspNet.SignalR.Core (Version 2.4.3)
dotnet add package Microsoft.AspNet.SignalR.Core --version 2.4.3
## Microsoft.AspNetCore.SignalR.Common (Version 7.0.10)
dotnet add package Microsoft.AspNetCore.SignalR.Common --version 7.0.10
## Newtonsoft.Json (Version 13.0.3):
dotnet add package Newtonsoft.Json --version 13.0.3
## Swashbuckle.AspNetCore (Version 6.5.0)
dotnet add package Swashbuckle.AspNetCore --version 6.5.0
## System.IO (Version 4.3.0)
dotnet add package System.IO --version 4.3.0
