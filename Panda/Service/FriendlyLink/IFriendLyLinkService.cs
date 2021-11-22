using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Entity.Responses;

namespace Panda.Services.FriendlyLink;

public interface IFriendlyLinkService
{
    Task<PageDto<FriendlyLinkResponse>> AdminGetList(FriendlyLinkRequest request);


    Task<PageDto<FriendlyLinkResponse>> GetList(FriendlyLinkRequest request);


    Task Audit(int Id, AuditStatusEnum auditStatus);


    Task AddFriendLink(AddFriendLinkRequest request, AuditStatusEnum auditStatus = AuditStatusEnum.unaudit);
}
