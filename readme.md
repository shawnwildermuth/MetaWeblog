#WilderMinds.MetaWeblog

Build Status: [![Build status](https://ci.appveyor.com/api/projects/status/yc3leb1t5t6ue01i?svg=true)](https://ci.appveyor.com/project/shawnwildermuth/metaweblog)

To install, use the Nuget "WilderMinds.MetaWeblog":

```
  PM> Install-Package WilderMinds.MetaWeblog
```

This project is an ASP.NET Core 1.0 Middleware component to support the MetaWeblog API 
that WordPress and Windows LiveWriter uses to support adding and editing of content
in blogs.

To support MetaWeblog, you must first create a class that implements the IMetaWeblogProvider interface:

```C#
  public class TestMetaWeblogService : IMetaWeblogProvider
  {
    public UserInfo GetUserInfo(string key, string username, string password)
    {
      // TODO
    }

    public BlogInfo[] GetUsersBlogs(string key, string username, string password)
    {
      // TODO
    }


    public Post GetPost(string postid, string username, string password)
    {
      // TODO
    }

    public Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
    {
      // TODO
    }


    public string AddPost(string blogid, string username, string password, Post post, bool publish)
    {
      // TODO
    }

    public bool DeletePost(string key, string postid, string username, string password, bool publish)
    {
      // TODO
    }

    public bool EditPost(string postid, string username, string password, Post post, bool publish)
    {
      // TODO
    }


    public CategoryInfo[] GetCategories(string blogid, string username, string password)
    {
      // TODO
    }

    public MediaObjectInfo NewMediaObject(string blogid, string username, string password, MediaObject mediaObject)
    {
      // TODO
    }
  }
```

Once you've implemented the class, you can register the middleware by first adding MetaWeblog in 
ConfigureServices supplying the name of the implemented service class:
```C#
    public void ConfigureServices(IServiceCollection svcs)
    {
      //...

      // Supporting Live Writer (MetaWeblogAPI)
      svcs.AddMetaWeblog<TestWeblogService>();

      //...
    }

```

Finally, you have to add MetaWeblog in the Configure call to specify the endpoint to listen on when 
waiting for the MetaWeblog calls:

```C#
    public void Configure(IApplicationBuilder app,
                          ILoggerFactory loggerFactory,
                          WilderInitializer initializer)
    {
      //...

      // Support MetaWeblog API
      app.UseMetaWeblog("/livewriter");

      //...
    }
```

This simply handles the routing to your methods. It does not implement the service at all. 

NOTE: This project does not support .NET Core yet as the dependency on XmlRpcLight is only 
.NET 4.5.x currently but I'm looking for options or PRs that can help support both.
