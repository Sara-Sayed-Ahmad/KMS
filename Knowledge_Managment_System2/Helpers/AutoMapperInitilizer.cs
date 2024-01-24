using AutoMapper;
using Knowledge_Managment_System2.Model.DTOs;
using Knowledge_Managment_System2.Model.UserDTOs;
using Knowledge_Managment_System2.Model;

namespace Knowledge_Managment_System2.Helpers
{
    public class AutoMapperInitilizer : Profile
    {
        public AutoMapperInitilizer()
        {
            //Achievement -> AchievementDTO
            CreateMap<Achievement, AchievementDTO>().ReverseMap();

            //Employee -> EmployeeDTO
            CreateMap<Employee, EmployeeDTO>().ReverseMap();

            //Experience -> ExperienceDTO
            CreateMap<Experience, ExperienceDTO>().ReverseMap();

            //Track -> TrackDTO
            CreateMap<Track, TrackDTO>().ReverseMap();

            //Course -> CourseDTO
            CreateMap<Course, CourseDTO>().ReverseMap();

            //Department -> DepartmentDTO
            CreateMap<Department, DepartmentDTO>().ReverseMap()
                 .ForMember(dest => dest.Positions, opt => opt.MapFrom(src => src.Positions));

            //File -> FileDTO
            CreateMap<FileR, FileDTO>().ReverseMap();

            //Link -> LinkDTO, CreateLinkDTO
            CreateMap<Link, LinkDTO>().ReverseMap();

            //Record -> RecordDTO
            CreateMap<Record, RecordDTO>().ReverseMap();

            //Role -> RoleDTO
            CreateMap<Position, PositionDTO>().ReverseMap();

            //Permission -> PermissionDTO
            CreateMap<Permission, PermissionDTO>().ReverseMap();

            //Employee -> AuthenticateResponse
            CreateMap<Employee, AuthenticateResponse>().ReverseMap();

            //RegisterRequest -> Employee
            CreateMap<RegisterRequest, Employee>().ReverseMap();
        }
    }
}