using Application.UserManagement.Common.Models;
using MediatR;

namespace Application.UserManagement.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<List<UserViewModel>>
    {
        public string SearchText { get; set; } = null!;
    }
}
