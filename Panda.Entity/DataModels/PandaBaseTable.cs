using FreeSql.DataAnnotations;

namespace Panda.Entity.DataModels;

public class PandaBaseTable
{
    [Column(IsIdentity = true,IsPrimary = true,CanInsert = false)]
    public virtual int Id { get; set; }

    [Column(ServerTime = DateTimeKind.Utc,CanInsert = false,CanUpdate = false)]
    public DateTime AddTime { get; set; }
}