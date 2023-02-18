using QingCheng.Tools.EFCore;

namespace QingCheng.Site.Data.Blogs;

public class PostComments : BaseTableModel<int>
{
    public PostComments()
    {
        
    }
    public PostComments(string content, string email, string nickName, int pid, Posts posts)
    {
        Content = content;
        Email = email;
        NickName = nickName;
        Pid = pid;
        Post = posts;
    }

    public string Content { get; set; }

    public string Email { get; set; }

    public string NickName { get; set; }

    public int Pid { get; set; }

    public Posts Post { get; set; }
}