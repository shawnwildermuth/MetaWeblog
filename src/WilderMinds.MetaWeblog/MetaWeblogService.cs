using Microsoft.Extensions.Logging;

namespace WilderMinds.MetaWeblog
{
  public class MetaWeblogService : XmlRpcService
  {
    private IMetaWeblogProvider _provider;
    private ILogger<MetaWeblogService> _logger;

    public MetaWeblogService(IMetaWeblogProvider provider, ILogger<MetaWeblogService> logger) : base(logger)
    {
      _provider = provider;
      _logger = logger;
    }

    [XmlRpcMethod("blogger.getUsersBlogs")]
    public BlogInfo[] GetUsersBlogs(string key, string username, string password)
    {
      _logger.LogInformation($"MetaWeblog:GetUserBlogs is called");
      return _provider.GetUsersBlogs(key, username, password);
    }

    [XmlRpcMethod("blogger.getUserInfo")]
    public UserInfo GetUserInfo(string key, string username, string password)
    {
      _logger.LogInformation($"MetaWeblog:GetUserInfo is called");
      return _provider.GetUserInfo(key, username, password);
    }

    [XmlRpcMethod("wp.newCategory")]
    public int AddCategory(string key, string username, string password, NewCategory category)
    {
      _logger.LogInformation($"MetaWeblog:AddCategory is called");
      return _provider.AddCategory(key, username, password, category);
    }

    [XmlRpcMethod("metaWeblog.getPost")]
    public Post GetPost(string postid, string username, string password)
    {
      _logger.LogInformation($"MetaWeblog:GetPost is called");
      return _provider.GetPost(postid, username, password);
    }

    [XmlRpcMethod("metaWeblog.getRecentPosts")]
    public Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
    {
      _logger.LogInformation($"MetaWeblog:GetRecentPosts is called");
      return _provider.GetRecentPosts(blogid, username, password, numberOfPosts);
    }

    [XmlRpcMethod("metaWeblog.newPost")]
    public string AddPost(string blogid, string username, string password, Post post, bool publish)
    {
      _logger.LogInformation($"MetaWeblog:AddPost is called");
      return _provider.AddPost(blogid, username, password, post, publish);
    }

    [XmlRpcMethod("metaWeblog.editPost")]
    public bool EditPost(string postid, string username, string password, Post post, bool publish)
    {
      _logger.LogInformation($"MetaWeblog:EditPost is called");
      return _provider.EditPost(postid, username, password, post, publish);
    }

    [XmlRpcMethod("blogger.deletePost")]
    public bool DeletePost(string key, string postid, string username, string password, bool publish)
    {
      _logger.LogInformation($"MetaWeblog:DeletePost is called");
      return _provider.DeletePost(key, postid, username, password, publish);
    }

    [XmlRpcMethod("metaWeblog.getCategories")]
    public CategoryInfo[] GetCategories(string blogid, string username, string password)
    {
      _logger.LogInformation($"MetaWeblog:GetCategories is called");
      return _provider.GetCategories(blogid, username, password);
    }

    [XmlRpcMethod("metaWeblog.newMediaObject")]
    public MediaObjectInfo NewMediaObject(string blogid, string username, string password, MediaObject mediaObject)
    {
      _logger.LogInformation($"MetaWeblog:NewMediaObject is called");
      return _provider.NewMediaObject(blogid, username, password, mediaObject);
    }
  }
}