using AutoMapper;
using Azure.Core;
using Calendar.Database.DTO;
using Calendar.Database.Entities;

namespace Calendar.Infrastructure
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Note, NoteDTO>()
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
                cfg.CreateMap<string, NoteType>()
                    .ConvertUsing(src => 
                        src.ToLower() == "event" ? NoteType.Event
                            : src.ToLower() == "info" ? NoteType.Info
                                : NoteType.Other);
                cfg.CreateMap<NoteDTO, Note>();
            });
        }
    }
}