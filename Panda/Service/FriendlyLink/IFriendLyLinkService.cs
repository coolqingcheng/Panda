using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Entity.Responses;

namespace Panda.Services.FriendlyLink;

public interface IFriendlyLinkService
{
    Task<PageDto<FriendlyLinkResponse>> AdminGetList(FriendlyLinkRequest request);


    Task<PageDto<FriendlyLinkResponse>> GetList(FriendlyLinkRequest request);
    
    Task<PageDto<FriendlyLinkResponse>> GetListByCache(FriendlyLinkRequest request);



    Task Delete(int id);


    Task Audit(int id, AuditStatusEnum auditStatus);


    Task AddOrUpdateFriendLink(AddFriendLinkRequest request);
}