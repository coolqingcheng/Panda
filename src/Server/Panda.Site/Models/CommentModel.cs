using System.Text.Json.Serialization;

namespace Panda.Site.Models;

public class CommentModel
{
    public int Id { get; set; }
    public string NickName { get; set; }
    public string Content { get; set; }
    public DateTime AddTime { get; set; }
    public string Os { get; set; }
    public string Browser { get; set; }

    [JsonIgnore]
    public string UA { get; set; }
}

public class QueryComment
{
    public string Link { get; set; }
    public int Index { get; set; }
}