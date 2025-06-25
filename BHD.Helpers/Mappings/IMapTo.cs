﻿using AutoMapper;

namespace BHD.Helpers.Mappings;

public interface IMapTo<T>
{
    void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
}

