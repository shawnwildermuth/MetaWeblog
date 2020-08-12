# WilderMinds.MetaWeblog

Build Status: ![Build Status](https://github.com/shawnwildermuth/metaweblog/workflows/BuildAndTest/badge.svg)

To install, use the Nuget "WilderMinds.MetaWeblog":

```
  PM> Install-Package WilderMinds.MetaWeblog
```

This project is an ASP.NET Core 2.0 Middleware component to support the MetaWeblog API 
that WordPress and Windows LiveWriter uses to support adding and editing of content
in blogs.

To support MetaWeblog, you must first create a class that implements the IMetaWeblogProvider interface:

```C#
  public class TestMetaWeblogService : IMetaWeblogProvider
  {
    public async Task<UserInfo> GetUserInfoAsync(string key, string username, string password)
    {
      throw new NotImplementedException();
    }

    public async Task<BlogInfo[]> GetUsersBlogsAsync(string key, string username, string password)
    {
      throw new NotImplementedException();
    }


    public async Task<Post> GetPostAsync(string postid, string username, string password)
    {
      throw new NotImplementedException();
    }

    public async Task<Post[]> GetRecentPostsAsync(string blogid, string username, string password, int numberOfPosts)
    {
      throw new NotImplementedException();
    }


    public async Task<string> AddPostAsync(string blogid, string username, string password, Post post, bool publish)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> DeletePostAsync(string key, string postid, string username, string password, bool publish)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> EditPostAsync(string postid, string username, string password, Post post, bool publish)
    {
      throw new NotImplementedException();
    }


    public async Task<CategoryInfo[]> GetCategoriesAsync(string blogid, string username, string password)
    {
      throw new NotImplementedException();
    }

    public async Task<MediaObjectInfo> NewMediaObjectAsync(string blogid, string username, string password, MediaObject mediaObject)
    {
      throw new NotImplementedException();
    }

    public async Task<int> AddCategoryAsync(string key, string username, string password, NewCategory category)
    {
      throw new NotImplementedException();
    }

    public Task<Tag[]> GetTagsAsync(string blogid, string username, string password)
    {
      throw new NotImplementedException();
    }

    public Page GetPage(string blogid, string pageid, string username, string password)
    {
      throw new NotImplementedException();
    }

    public Page[] GetPages(string blogid, string username, string password, int numPages)
    {
      throw new NotImplementedException();
    }

    public Author[] GetAuthors(string blogid, string username, string password)
    {
      throw new NotImplementedException();
    }

    public string AddPage(string blogid, string username, string password, Page page, bool publish)
    {
      throw new NotImplementedException();
    }

    public bool EditPage(string blogid, string pageid, string username, string password, Page page, bool publish)
    {
      throw new NotImplementedException();
    }

    public bool DeletePage(string blogid, string username, string password, string pageid)
    {
      throw new NotImplementedException();
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
      svcs.AddMetaWeblog<TestMetaWeblogService>();

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

