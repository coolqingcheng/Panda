using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace QingCheng.Tools.Swagger;

public class SwaggerEnumFilter : IDocumentFilter
{
    /// <summary>
    /// 实现IDocumentFilter接口的Apply函数
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        Dictionary<string, Type> dict = GetAllEnum();

        foreach (var item in swaggerDoc.Components.Schemas)
        {
            var property = item.Value;
            var typeName = item.Key;
            if (property.Enum != null && property.Enum.Count > 0)
            {
                Type? itemType;
                if (dict.ContainsKey(typeName))
                {
                    itemType = dict[typeName];
                }
                else
                {
                    itemType = null;
                }

                List<OpenApiInteger> list = new List<OpenApiInteger>();
                foreach (var val in property.Enum)
                {
                    list.Add((OpenApiInteger)val);
                }

                property.Description += DescribeEnum(itemType!, list);
            }
        }
    }

    private static Dictionary<string, Type> GetAllEnum()
    {
        var assList = Assembly.GetEntryAssembly()?.GetReferencedAssemblies();
        var dict = new Dictionary<string, Type>();
        var list = new List<Assembly>();
        list.Add(Assembly.GetEntryAssembly()!);
        foreach (var name in assList!)
        {
            Assembly ass = Assembly.Load(name); //枚举所在的命名空间的xml文件名，我的枚举都放在Model层里（类库）
            list.Add(ass);
        }

        foreach (var assembly in list)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type item in types)
            {
                if (item.IsEnum)
                {
                    dict.TryAdd(item.Name, item);
                }
            }
        }

        return dict;
    }

    private static string DescribeEnum(Type type, List<OpenApiInteger> enums)
    {
        var enumDescriptions = new List<string>();
        foreach (var item in enums)
        {
            if (type == null) continue;
            var value = Enum.Parse(type, item.Value.ToString());
            var desc = GetDescription(type, value);

            if (string.IsNullOrEmpty(desc))
                enumDescriptions.Add($"{item.Value.ToString()}：{Enum.GetName(type, value)}；");
            else
                enumDescriptions.Add($"{item.Value.ToString()}：{Enum.GetName(type, value)}，{desc}；");
        }

        return $"<br><div>{Environment.NewLine}{string.Join("<br/>" + Environment.NewLine, enumDescriptions)}</div>";
    }

    private static string GetDescription(Type t, object value)
    {
        foreach (MemberInfo mInfo in t.GetMembers())
        {
            if (mInfo.Name == t.GetEnumName(value))
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    if (attr.GetType() == typeof(DescriptionAttribute))
                    {
                        return ((DescriptionAttribute)attr).Description;
                    }
                }
            }
        }

        return string.Empty;
    }
}