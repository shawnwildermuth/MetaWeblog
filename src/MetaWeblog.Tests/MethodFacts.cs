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

namespace MetaWeblog.Tests
{
  public class MethodFacts
  {
    [Fact]
    public async Task ShouldReturnBlogInfo()
    {
      var server = TestServer.Create(app =>
      {
        app.UseMetaWeblog("/livewriter");
      }, svcs =>
      {
        svcs.AddMetaWeblog<TestMetaWeblogService>();
      });

      using (server)
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

        var content = new StringContent(xml, Encoding.UTF8, "text/xml");       
        var result = await server.CreateClient().PostAsync("/livewriter", content);

        Assert.True(result.StatusCode == HttpStatusCode.OK, "Success");
        var msg = await result.Content.ReadAsStringAsync();
        Assert.True(msg.Contains("Test Blog"), "Should contain name of test blog");
      }
    }
  }
}
