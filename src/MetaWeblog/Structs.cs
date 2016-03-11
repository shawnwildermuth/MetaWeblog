using System;

namespace WilderMinds.MetaWeblog
{
  public struct BlogInfo
  {
    public string blogid;
    public string url;
    public string blogName;
  }

  public struct Category
  {
    public string categoryId;
    public string categoryName;
  }

  public struct CategoryInfo
  {
    public string description;
    public string htmlUrl;
    public string rssUrl;
    public string title;
    public string categoryid;
  }

  public struct Enclosure
  {
    public int length;
    public string type;
    public string url;
  }

  public struct Post
  {
    public DateTime dateCreated;
    public string description;
    public string title;
    public string[] categories;
    public string permalink;
    public object postid;
    public string userid;
    public string wp_slug;
  }


  public struct Source
  {
    public string name;
    public string url;
  }

  public struct UserInfo
  {
    public string userid;
    public string firstname;
    public string lastname;
    public string nickname;
    public string email;
    public string url;
  }

  public struct MediaObject
  {
    public string name;
    public string type;
    public byte[] bits;
  }

  public struct MediaObjectInfo
  {
    public string url;
  }

}
