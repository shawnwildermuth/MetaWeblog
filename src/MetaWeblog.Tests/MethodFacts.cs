using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.TestHost;
using WilderMinds.MetaWeblog;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Microsoft.AspNet.Hosting;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Xml.Linq;

namespace MetaWeblog.Tests
{
  public class MethodFacts : IDisposable
  {
    private TestServer _server;

    public MethodFacts()
    {
      _server = TestServer.Create(app =>
      {
        app.UseMetaWeblog("/livewriter");
      }, svcs =>
      {
        svcs.AddMetaWeblog<TestMetaWeblogService>();
      });

    }

    public void Dispose()
    {
      _server.Dispose();
    }

    [Fact]
    public async Task ShouldReturnBlogInfo()
    {
      var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<methodCall>
 <methodName>blogger.getUsersBlogs</methodName>
 <params>
  <param>
   <value>
    <string>0123456789ABCDEF</string>
   </value>
  </param>
  <param>
   <value>
    <string>TestUser</string>
   </value>
  </param>
  <param>
   <value>
    <string>testPassword</string>
   </value>
  </param>
 </params>
</methodCall>";

      var result = await IssueMethod(xml);
      Assert.True(!result.Descendants("fault").Any(), "Should not contain a fault");
      Assert.True(result.Descendants("string").Any(s => s.Value == "Test Blog"), "Should contain name of test blog");
      var blogid = result.Descendants("name").Where(x => x.Value == "blogid").FirstOrDefault();
      Assert.NotNull(blogid);
      Assert.True(blogid.Parent.Parent.Parent.Name.LocalName == "value");
    }

    [Fact]
    public async Task ShouldReturnUserInfo()
    {
      var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<methodCall>
 <methodName>blogger.getUserInfo</methodName>
 <params>
  <param>
   <value>
    <string>0123456789ABCDEF</string>
   </value>
  </param>
  <param>
   <value>
    <string>TestUser</string>
   </value>
  </param>
  <param>
   <value>
    <string>testPassword</string>
   </value>
  </param>
 </params>
</methodCall>";

      var result = await IssueMethod(xml);
      Assert.True(!result.Descendants("fault").Any(), "Should not contain a fault");
      Assert.True(result.Descendants("string").Any(s => s.Value == "me@us.com"), "Should contain the email of the user");
    }

    [Fact]
    public async Task ShouldReturnCategoryInfo()
    {
      var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<methodCall>
 <methodName>metaWeblog.getCategories</methodName>
 <params>
  <param>
   <value>
    <string>0123456789ABCDEF</string>
   </value>
  </param>
  <param>
   <value>
    <string>TestUser</string>
   </value>
  </param>
  <param>
   <value>
    <string>testPassword</string>
   </value>
  </param>
 </params>
</methodCall>";

      var result = await IssueMethod(xml);
      Assert.True(!result.Descendants("fault").Any(), "Should not contain a fault");
      Assert.True(result.Descendants("name").Any(s => s.Value == "title"), "Should contain categories");
      Assert.True(result.Descendants("value").Any(s => s.Value == "ASP.NET"), "Should contain a category name");
    }

    [Fact]
    public async Task ShouldReturnPost()
    {
      var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<methodCall>
 <methodName>metaWeblog.getPost</methodName>
 <params>
  <param>
   <value>
    <string>0123456789ABCDEF</string>
   </value>
  </param>
  <param>
   <value>
    <string>TestUser</string>
   </value>
  </param>
  <param>
   <value>
    <string>testPassword</string>
   </value>
  </param>
 </params>
</methodCall>";

      var result = await IssueMethod(xml);
      Assert.True(!result.Descendants("fault").Any(), "Should not contain a fault");
      Assert.True(result.Descendants("name").Any(s => s.Value == "title"), "Should contain a Post");
      Assert.True(result.Descendants("value").Any(s => s.Value == "This is a post"), "Should contain a post title");
    }


    [Fact]
    public async Task ShouldReturnFailure()
    {
      var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<methodCall>
 <methodName>foo.bar</methodName>
 <params>
    <param>
     <value>
      <string>0123456789ABCDEF</string>
     </value>
    </param>
  </params>
</methodCall>";

      var result = await IssueMethod(xml);
      Assert.True(result.Descendants("fault").Any(), "Should contain the fault");
      Assert.True(result.Descendants("name").Any(s => s.Value == "faultCode"), "Should contain the fault code");
      Assert.True(result.Descendants("name").Any(s => s.Value == "faultString"), "Should contain the fault string");
    }

    async Task<XDocument> IssueMethod(string xml)
    {
      var content = new StringContent(xml, Encoding.UTF8, "text/xml");
      var result = await _server.CreateClient().PostAsync("/livewriter", content);

      Assert.True(result.StatusCode == HttpStatusCode.OK, "Success");

      var doc = XDocument.Parse(await result.Content.ReadAsStringAsync());

      return doc;
    }


  }
}
