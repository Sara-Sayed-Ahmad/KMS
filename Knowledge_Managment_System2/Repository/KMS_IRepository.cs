using Azure;
using Knowledge_Managment_System2.Model.AddDTO;
using Knowledge_Managment_System2.Model.Password;
using Knowledge_Managment_System2.Model.UpdateDTOs;
using Knowledge_Managment_System2.Model.UserDTOs;
using Knowledge_Managment_System2.Model;
using Knowledge_Managment_System2.Model.DTOs;

namespace Knowledge_Managment_System2.Repository
{
    public interface KMS_IRepository
    {
        //Achievement:
        //Get Achievement
        Task<List<AchievementDTO>> GetAchievement();

        //Get Achievement By id
        Task<AchievementDTO> GetAchievementById(string ide);

        //Save courses for user in achievement table
        Task SaveCourse(SaveCourse course);

        //Add description of course and completion date in achievement table
        Task FinishCourse(AchieveDate data);

        //Account:

        //Authentication Login 
        //AuthenticateResponse Authenticate(AuthenticateRequest users);

        //Reigester
        //void Register(RegisterRequest registerUser);

        //ForgetPassword
        Task ForgetPassword(ForgetPassword user);

        //Reset Password
        Task ResetPassword(ResetPassword data);

        //change Password
        Task<bool> ChangePassword(ChangePassword data);

        //Employees:

        //Get list Employees
        Task<List<EmployeeDTO>> GetEmployee();

        //Get Employee by id
        Task<EmployeeDTO> GetEmployeeById(string id);

        //Update Employee using id 
        Task UpdateEmployee(string id, UpdateEmployeeDTOs employee);

        //Add experiences
        Task addExperience(AddExperienceDto data);

        Task<ExperienceDTO> GetExperience(string id);

        //Number of employees for each Position
        Task<int> GetEmployeePosition(int id);

        //Roles:

        //Get list Role
        Task<List<PositionDTO>> GetAllPosition();

        //Get Position by id
        Task<PositionDTO> GetPosition(int id);

        //Department:

        //Get list Department
        Task<List<DepartmentDTO>> GetAllDepartment();

        //Get Department by id
        Task<DepartmentDTO> GetDepartmentById(int id);

        //Tracks:

        //Add Tracks 
        Task AddTrack(AddTrackDTO track);

        //Get list Track
        Task<List<TrackDTO>> GetAllTrack();

        //Get Track by id
        Task<TrackDTO> GetTrackById(int id);

        //Update Track
        Task UpdateTrack(int id, UpdatetrackDTO track, string employeeId);

        //Delete Track by id 
        Task DeleteTrackById(int id);

        //Records:

        //Add Records
        Task AddRecord(AddRecordDTO record);

        //Approve Records
        Task ApproveRecord(int id);

        //Reject Records
        Task RejectRecord(int id);

        //Get list Record
        Task<List<RecordDTO>> GetAllRecord();

        //Get Record by id
        Task<RecordDTO> GetRecordById(int id);

        //Update Record using id 
        Task UpdateRecord(int id, UpdateRecordDTO record, string employeeId);

        //Delete Record by id
        Task DeleteRecordById(int id);

        //Number of record for each department
        Task<int> GetNumberRecord(int id);

        //Courses:

        //Add Courses
        Task AddCourse(AddCourseDTO course);

        //Approve Record updated
        Task ApproveUpdateRecord(int RecordId);

        //Reject Record Updated
        Task RejectUpdateRercord(int recordId);

        //Get list Courses
        Task<List<CourseDTO>> GetAllCourses();

        //Get Course by id 
        Task<CourseDTO> GetCourse(int id);

        //Delete Course by id 
        Task DeleteCourseById(int id);

        //Links:

        //Add links
        Task AddLink(List<AddLinkDTO> links);

        //Get list link
        Task<List<LinkDTO>> GetAllLinks();

        //Get Link by id 
        Task<LinkDTO> GetLinkId(int id);

        //Delete Link by id
        Task DeleteLinkById(int id);
    }
}
