using System.Threading.Tasks;

namespace WilderMinds.MetaWeblog
{
  public interface IMetaWeblogProvider
  {
    Task<UserInfo> GetUserInfoAsync(string key, string username, string password);

    Task<BlogInfo[]> GetUsersBlogsAsync(string key, string username, string password);

    Task<Post> GetPostAsync(string postid, string username, string password);

    Task<Post[]> GetRecentPostsAsync(string blogid, string username, string password, int numberOfPosts);

    Task<string> AddPostAsync(string blogid, string username, string password, Post post, bool publish);

    Task<bool> DeletePostAsync(string key, string postid, string username, string password, bool publish);

    Task<bool> EditPostAsync(string postid, string username, string password, Post post, bool publish);

    Task<CategoryInfo[]> GetCategoriesAsync(string blogid, string username, string password);

    Task<int> AddCategoryAsync(string key, string username, string password, NewCategory category);

    Task<MediaObjectInfo> NewMediaObjectAsync(string blogid, string username, string password, MediaObject mediaObject);

    Page GetPage(string blogid, string pageid, string username, string password);
    Page[] GetPages(string blogid, string username, string password, int numPages);
    Author[] GetAuthors(string blogid, string username, string password);

    string AddPage(string blogid, string username, string password, Page page, bool publish);
    bool EditPage(string blogid, string pageid, string username, string password, Page page, bool publish);
    bool DeletePage(string blogid, string username, string password, string pageid);
  }
}