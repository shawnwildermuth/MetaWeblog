namespace WilderMinds.MetaWeblog
{
  public interface IMetaWeblogProvider
  {
    UserInfo GetUserInfo(string key, string username, string password);
    BlogInfo[] GetUsersBlogs(string key, string username, string password);

    Post GetPost(string postid, string username, string password);
    Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts);

    string AddPost(string blogid, string username, string password, Post post, bool publish);
    bool DeletePost(string key, string postid, string username, string password, bool publish);
    bool EditPost(string postid, string username, string password, Post post, bool publish);

    CategoryInfo[] GetCategories(string blogid, string username, string password);
    int AddCategory(string key, string username, string password, NewCategory category);

    MediaObjectInfo NewMediaObject(string blogid, string username, string password, MediaObject mediaObject);
  }
}