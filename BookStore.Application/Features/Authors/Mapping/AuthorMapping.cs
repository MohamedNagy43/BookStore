using BookStore.Application.Features.Authors.Contracts.Requests;
using BookStore.Domain.Entities.Authors;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Features.Authors.Mapping;

public class AuthorMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthorRequest, Author>()
            .Map(dest => dest.FileId, src => src.ImageId);
    }
}
