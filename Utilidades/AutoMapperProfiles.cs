using AutoMapper;

public class AutoMapperProfiles : Profile{
   
   public AutoMapperProfiles(){
    CreateMap<usuarioDto, Usuario>();
   }

}