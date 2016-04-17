using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WilderMinds.MetaWeblog;

namespace MetaWeblog.Tests
{
  public class TestMetaWeblogService : IMetaWeblogProvider
  {
    public int AddCategory(string key, string username, string password, NewCategory category)
    {
      return 1;
    }

    public string AddPost(string blogid, string username, string password, Post post, bool publish)
    {
      return "123";
    }

    public bool DeletePost(string key, string postid, string username, string password, bool publish)
    {
      return true;
    }

    public bool EditPost(string postid, string username, string password, Post post, bool publish)
    {
      return true;
    }

    public CategoryInfo[] GetCategories(string blogid, string username, string password)
    {
      return new CategoryInfo[]
      {
        new CategoryInfo() { categoryid = "1", title = "ASP.NET", htmlUrl = "/cats/aspnet" }
      };
    }

    public Post GetPost(string postid, string username, string password)
    {
      return new Post()
      {
        postid = 1,
        dateCreated = DateTime.UtcNow,
        description = "<p>This post is a long post</p>",
        permalink = "/123",
        title = "This is a post",
        userid = "swildermuth",
        categories = new string[] { "usda" }
      };
    }

    public Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
    {
      throw new NotImplementedException();
    }

    public UserInfo GetUserInfo(string key, string username, string password)
    {
      return new UserInfo()
      {
        firstname = "Shawn",
        lastname = "Wildermuth",
        email = "me@us.com",
        userid = "1"
      };
    }

    public BlogInfo[] GetUsersBlogs(string key, string username, string password)
    {
      return new BlogInfo[]
      {
        new BlogInfo()
        {
          blogid = "1",
          blogName = "Test Blog",
          url = "http://foo.com"
        }
      };
    }

    public MediaObjectInfo NewMediaObject(string blogid, string username, string password, MediaObject mediaObject)
    {
      return new MediaObjectInfo();
    }
  }
}
