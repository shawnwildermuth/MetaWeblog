using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WilderMinds.MetaWeblog;

namespace MetaWeblog.Tests
{
  public class TestMetaWeblogService : IMetaWeblogProvider
  {
    public Task<int> AddCategoryAsync(string key, string username, string password, NewCategory category)
    {
      return Task.FromResult(1);
    }

    public Task<string> AddPostAsync(string blogid, string username, string password, Post post, bool publish)
    {
      return Task.FromResult("123");
    }

    public Task<bool> DeletePostAsync(string key, string postid, string username, string password, bool publish)
    {
      return Task.FromResult(true);
    }

    public Task<bool> EditPostAsync(string postid, string username, string password, Post post, bool publish)
    {
      return Task.FromResult(true);
    }

    public Task<CategoryInfo[]> GetCategoriesAsync(string blogid, string username, string password)
    {
      return Task.FromResult(new CategoryInfo[]
      {
        new CategoryInfo() { categoryid = "1", title = "ASP.NET", htmlUrl = "/cats/aspnet" }
      });
    }

    public Task<Post> GetPostAsync(string postid, string username, string password)
    {
      return Task.FromResult(new Post()
      {
        postid = 1,
        dateCreated = DateTime.UtcNow,
        description = "<p>This post is a long post</p>",
        permalink = "/123",
        title = "This is a post",
        userid = "swildermuth",
        categories = new string[] { "usda" }
      });
    }

    public Task<Post[]> GetRecentPostsAsync(string blogid, string username, string password, int numberOfPosts)
    {
      throw new NotImplementedException();
    }

    public Task<UserInfo> GetUserInfoAsync(string key, string username, string password)
    {
      return Task.FromResult(new UserInfo()
      {
        firstname = "Shawn",
        lastname = "Wildermuth",
        email = "me@us.com",
        userid = "1"
      });
    }

    public Task<BlogInfo[]> GetUsersBlogsAsync(string key, string username, string password)
    {
      return Task.FromResult(new BlogInfo[]
      {
        new BlogInfo()
        {
          blogid = "1",
          blogName = "Test Blog",
          url = "http://foo.com"
        }
      });
    }

    public Task<MediaObjectInfo> NewMediaObjectAsync(string blogid, string username, string password, MediaObject mediaObject)
    {
      return Task.FromResult(new MediaObjectInfo());
    }

    public Page GetPage(string blogid, string pageid, string username, string password)
    {
      return new Page()
      {
        page_id = "1",
        dateCreated = DateTime.UtcNow,
        description = "<p>This page is a long page</p>",
        title = "This is a page",
        categories = new string[] { "usda" }
      };
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
      return "123";
    }

    public bool EditPage(string blogid, string pageid, string username, string password, Page page, bool publish)
    {
      return true;
    }

    public bool DeletePage(string blogid, string username, string password, string pageid)
    {
      return true;
    }
  }
}
