using System;
using System.Reflection;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Collections;

namespace WilderMinds.MetaWeblog
{
  public class XmlRpcService
  {

    // <?xml version="1.0"?>
    // <methodCall>
    //    <methodName>examples.getStateName</methodName>
    //    <params>
    //       <param>
    //          <value><i4>41</i4></value>
    //          </param>
    //   </params>
    //</methodCall>

    public string Invoke(string xml)
    {
      var doc = XDocument.Parse(xml);
      var methodNameElement = doc
        .Descendants("methodName")
        .FirstOrDefault();
      if (methodNameElement != null)
      {
        var method = methodNameElement.Value;

        var theType = GetType();
        var typeMethods = theType.GetMethods();

        foreach (var typeMethod in typeMethods)
        {
          var attr = typeMethod.GetCustomAttribute<XmlRpcMethodAttribute>();
          if (method.ToLower() == attr.MethodName.ToLower())
          {
            var parameters = GetParameters(doc);
            var result = typeMethod.Invoke(this, parameters);
            return SerializeResponse(result);
          }
        }
      }

      throw new MetaWeblogException("Failed to handle XmlRpcService call");
    }

    private string SerializeResponse(object result)
    {
      if (result is MetaWeblogException)
      {
        return SerializeFaultResponse((MetaWeblogException)result);
      }

      var doc = new XDocument();
      var response = new XElement("methodResponse");
      doc.Add(response);
      var theParams = new XElement("params");
      response.Add(theParams);

      SerializeResponseParameters(theParams, result);

      return doc.ToString(SaveOptions.DisableFormatting);
    }

    private XElement SerializeValue(object result)
    {
      var theType = result.GetType();
      XElement newElement = new XElement("value");

      if (theType == typeof(int))
      {
        newElement.Add(new XElement("i4", result.ToString()));
      }
      else if (theType == typeof(long))
      {
        newElement.Add(new XElement("long", result.ToString()));
      }
      else if (theType == typeof(double))
      {
        newElement.Add(new XElement("double", result.ToString()));
      }
      else if (theType == typeof(bool))
      {
        newElement.Add(new XElement("boolean", result.ToString()));
      }
      else if (theType == typeof(string))
      {
        newElement.Add(new XElement("string", result.ToString()));
      }
      else if (theType == typeof(DateTime))
      {
        var date = (DateTime)result;
        newElement.Add(new XElement("datetime.iso8601", date.ToString("yyyyMMdd'T'HH':'mm':'ss",
                        DateTimeFormatInfo.InvariantInfo)));
      }
      else if (result is IEnumerable)
      {
        var data = new XElement("data");
        foreach (var item in ((IEnumerable)result))
        {
          data.Add(SerializeValue(item));
        }
        newElement.Add(new XElement("array", data));
      }
      else
      {
        var theStruct = new XElement("struct");
        // Reference Type
        foreach (var field in theType.GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            var member = new XElement("member");
            member.Add(new XElement("name", field.Name));
            var value = field.GetValue(result);
            member.Add(SerializeValue(value));
            theStruct.Add(member);
        }
        newElement.Add(new XElement("struct", theStruct));
      }

      return newElement;
    }

    private void SerializeResponseParameters(XElement theParams, object result)
    {
      theParams.Add(new XElement("param", SerializeValue(result)));
    }

    private XElement CreateStringValue(string typeName, string value)
    {
      return new XElement("value", new XElement(typeName, value));
    }

    private string SerializeFaultResponse(MetaWeblogException result)
    {
      throw new MetaWeblogException("Not implemented");
    }

    private object[] GetParameters(XDocument doc)
    {
      var parameters = new List<object>();
      var paramsEle = doc.Descendants("params");

      // Handle no parameters
      if (paramsEle == null)
      {
        return parameters.ToArray();
      }

      foreach (var p in paramsEle.Descendants("param"))
      {
        parameters.AddRange(Parse(p));
      }

      return parameters.ToArray();
    }

    private List<object> Parse(XElement param)
    {
      var value = param.Element("value");
      var type = value.Descendants().FirstOrDefault();
      if (type != null)
      {
        var typename = type.Name.LocalName;
        switch (typename)
        {
          case "array":
            return ParseArray(type);
          case "struct":
            return ParseStruct(type);
          case "i4":
          case "int":
            return ParseInt(type);
          case "i8":
            return ParseLong(type);
          case "string":
            return ParseString(type);
          case "boolean":
            return ParseBoolean(type);
          case "double":
            return ParseDouble(type);
          case "dateTime.iso8601":
            return ParseDateTime(type);

        }
      }

      throw new MetaWeblogException("Failed to parse parameters");

    }

    private List<object> ParseLong(XElement type)
    {
      return new List<object> { long.Parse(type.Value) };
    }

    private List<object> ParseDateTime(XElement type)
    {
      DateTime parsed;

      if (DateTime8601.TryParseDateTime8601(type.Value, out parsed))
      {
        return new List<object>() { parsed };
      }

      throw new MetaWeblogException("Failed to parse date");
    }

    private List<object> ParseBoolean(XElement type)
    {
      return new List<object> { type.Value == "1" };
    }

    private List<object> ParseString(XElement type)
    {
      return new List<object> { type.Value };
    }

    private List<object> ParseDouble(XElement type)
    {
      return new List<object> { double.Parse(type.Value) };
    }

    private List<object> ParseInt(XElement type)
    {
      return new List<object> { int.Parse(type.Value) };
    }

    private List<object> ParseStruct(XElement type)
    {
      throw new NotImplementedException();
    }

    private List<object> ParseArray(XElement type)
    {
      var result = new List<object>();
      var data = type.Element("array").Element("data");
      foreach (var ele in data.Elements())
      {
        result.AddRange(Parse(ele));
      }
      return result;
    }
  }
}