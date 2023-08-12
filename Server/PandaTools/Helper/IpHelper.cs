using System.Net;
using System.Reflection;
using MaxMind.Db;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;

namespace PandaTools.Helper;

public class IpHelper
{
    public static CityResponse? City(string ipString)
    {
        try
        {
            var dic = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(dic, "MaxMindDb", "GeoLite2-City.mmdb");
            using var reader = new DatabaseReader(path);
            var response = reader.City(ipString);
            return response;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public static AsnResponse? ASN(string ipString)
    {
        try
        {
            var dic = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(dic, "MaxMindDb", "GeoLite2-ASN.mmdb");
            using var reader = new DatabaseReader(path);
            var response = reader.Asn(ipString);
            return response;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}