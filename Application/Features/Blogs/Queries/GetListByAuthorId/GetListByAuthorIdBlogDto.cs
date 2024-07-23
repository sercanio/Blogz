using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Blogs.Queries.GetList;

public class GetListByAuthorIdBlogDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Biography { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public IdentityUser User { get; set; }
    public Blog Blog { get; set; }

}