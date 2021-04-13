﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Services.Category.Contracts.Exceptions;
using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using WidePictBoard.Application.Services.Content.Interfaces;
using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Content.Implementations
{
    public sealed partial class ContentServiceV1 : IContentService
    {
        public async Task<Update.Response> Update(
            Update.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ContentUpdateRequestIsNullException();
            }

            var content = await _contentRepository.FindByIdWithUserAndCategoryAndTags(
                request.Id,
                cancellationToken);

            if (content == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            var isAdmin = await _identityService.IsInRole(
                userId,
                RoleConstants.AdminRole,
                cancellationToken);

            if (!isAdmin && content.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            content.Title = request.Title;
            content.Body = request.Body;
            content.Price = request.Price;
            content.CategoryId = request.CategoryId;

            content.IsDeleted = false;
            content.UpdatedAt = DateTime.UtcNow;

            var categoryRequest = new Category.Contracts.GetById.Request()
            {
                Id = content.CategoryId
            };
            var category = await _categoryRepository.FindById(
                categoryRequest.Id,
                cancellationToken);

            if (category == null)
            {
                throw new CategoryNotFoundException(categoryRequest.Id);
            }

            content.Category = category;
            if (content.Tags == null)
            {
                content.Tags = new List<Domain.Tag>();
            }
            content.Tags.Clear();

            foreach (string body in request.TagBodies)
            {
                var tag = await _tagRepository.FindWhere(
                    a => a.Body == body,
                    cancellationToken);

                if (tag == null)
                {
                    var tagRequest = new Tag.Contracts.Create.Request()
                    {
                        Body = body
                    };

                    tag = _mapper.Map<Domain.Tag>(tagRequest);
                    tag.CreatedAt = DateTime.UtcNow;
                    await _tagRepository.Save(tag, cancellationToken);
                }

                content.Tags.Add(tag);
            }

            await _contentRepository.Save(content, cancellationToken);

            return new Update.Response
            {
                Id = content.Id
            };
        }
    }
}