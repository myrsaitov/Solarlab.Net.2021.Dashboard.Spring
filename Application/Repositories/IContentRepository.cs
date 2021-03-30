﻿using System.Threading;
using System.Threading.Tasks;

namespace WidePictBoard.Application.Repositories
{
    public interface IContentRepository : IRepository<Domain.Content, int>
    {
        Task<Domain.Content> FindByIdWithUserInclude(int id, CancellationToken cancellationToken);
        Task<Domain.Content> FindByIdWithUserAndCategory(int id, CancellationToken cancellationToken);
    }
}