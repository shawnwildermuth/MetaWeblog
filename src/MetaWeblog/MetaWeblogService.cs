namespace WilderMinds.MetaWeblog
{
  public class MetaWeblogService : XmlRpcService
  {
    private IMetaWeblogProvider _provider;

    public MetaWeblogService(IMetaWeblogProvider provider)
    {
      _provider = provider;
    }

    [XmlRpcMethod("blogger.getUsersBlogs")]
    public BlogInfo[] GetUsersBlogs(string key, string username, string password)
    {
      return _provider.GetUsersBlogs(key, username, password);
    }

    [XmlRpcMethod("blogger.getUserInfo")]
    public UserInfo GetUserInfo(string key, string username, string password)
    {
      return _provider.GetUserInfo(key, username, password);
    }

    [XmlRpcMethod("metaWeblog.getPost")]
    public Post GetPost(string postid, string username, string password)
    {
      return _provider.GetPost(postid, username, password);
    }

    [XmlRpcMethod("metaWeblog.getRecentPosts")]
    public Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
    {
      return _provider.GetRecentPosts(blogid, username, password, numberOfPosts);
    }

    [XmlRpcMethod("metaWeblog.newPost")]
    public string AddPost(string blogid, string username, string password, Post post, bool publish)
    {
      return _provider.AddPost(blogid, username, password, post, publish);
    }

    [XmlRpcMethod("metaWeblog.editPost")]
    public bool EditPost(string postid, string username, string password, Post post, bool publish)
    {
      return _provider.EditPost(postid, username, password, post, publish);
    }

    [XmlRpcMethod("blogger.deletePost")]
    public bool DeletePost(string key, string postid, string username, string password, bool publish)
    {
      return _provider.DeletePost(key, postid, username, password, publish);
    }

    [XmlRpcMethod("metaWeblog.getCategories")]
    public CategoryInfo[] GetCategories(string blogid, string username, string password)
    {
      return _provider.GetCategories(blogid, username, password);
    }

    [XmlRpcMethod("metaWeblog.newMediaObject")]
    public MediaObjectInfo NewMediaObject(string blogid, string username, string password, MediaObject mediaObject)
    {
      return _provider.NewMediaObject(blogid, username, password, mediaObject);
    }
  }
}