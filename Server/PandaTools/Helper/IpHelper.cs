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
            if (dic != null)
            {
                var path = Path.Combine(dic, "MaxMindDb", "GeoLite2-City.mmdb");
                using var reader = new DatabaseReader(path);
                var response = reader.City(ipString);
                return response;
            }

            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static AsnResponse? Asn(string ipString)
    {
        try
        {
            var dic = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (dic != null)
            {
                var path = Path.Combine(dic, "MaxMindDb", "GeoLite2-ASN.mmdb");
                using var reader = new DatabaseReader(path);
                var response = reader.Asn(ipString);
                return response;
            }

            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}