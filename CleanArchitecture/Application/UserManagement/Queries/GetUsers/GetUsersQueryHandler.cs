using Application.UserManagement.Common.Models;
using MediatR;

namespace Application.UserManagement.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserViewModel>>
    {
        public async Task<List<UserViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
        //    var users = await _repository.GetAllAsync();
        //    var response = _mapper.Map<List<UserViewModel>>(users);
        //    return response;
            throw new NotImplementedException();
        }
    }
}
