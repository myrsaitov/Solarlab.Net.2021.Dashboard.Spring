﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WidePictBoard.Application.Services.Comment.Contracts
{
    public static class GetAll
    {
        public sealed class Response : Paged.Response<Response.CategoryResponse>
        {
            public sealed class CategoryResponse
            {
                public int Id { get; set; }
                public string Body { get; set; }

                public DateTime CommentDate { get; set; }
            }
        }
    }
}