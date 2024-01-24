using AutoMapper;
using Knowledge_Managment_System2.Authorization;
using Knowledge_Managment_System2.Model;
using Knowledge_Managment_System2.Model.AddDTO;
using Knowledge_Managment_System2.Model.DTOs;
using Knowledge_Managment_System2.Model.Password;
using Knowledge_Managment_System2.Model.UpdateDTOs;
using Knowledge_Managment_System2.Model.UserDTOs;
using Knowledge_Managment_System2.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

namespace Knowledge_Managment_System2.Repository
{
    public class KMS_Repository : KMS_IRepository
    {
        private readonly SystemDbContext _context;
        private readonly IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<Permission> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUserServices _userServices;
        private readonly IMailService _mailService;

        public KMS_Repository(SystemDbContext context, IMapper mapper,IJwtUtils jwtUtils, 
            UserManager<Employee> userManager, RoleManager<Permission> roleManager, 
            IConfiguration configuration, IUserServices userServices, IMailService mailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
            _userServices = userServices;
            _mailService = mailService;
        }

        //Achievement:
        //Get Achievement
        public async Task<List<AchievementDTO>> GetAchievement()
        {
            var ach = await _context.Achievements
                .Include(e => e.Employee)
                //.Include(e => e.Employee.Courses)
                //.Include(e => e.Employee.Records)
                //.Include(e => e.Employee.Tracks)
                .Include(c => c.Course)
                .ToListAsync();

            return _mapper.Map<List<AchievementDTO>>(ach);
        }

        //Get Achievemet using id
        public async Task<AchievementDTO> GetAchievementById(string ide)
        {
            var achId = await _context.Achievements
                .Include(e => e.Employee)
                //.Include(e => e.Employee.Records)
                //.Include(e => e.Employee.Tracks)
                //.Include(e => e.Employee.Permissions)
                //.Include(e => e.Employee.Position)
                .Include(c => c.Course)
                .Where(x => x.Id == ide).ToListAsync() ;

            return _mapper.Map<AchievementDTO>(achId);
        }

        //Save courses for user in achievement table
        public async Task SaveCourse(SaveCourse course)
        {
            var AchievementCourse = new Achievement()
            {
                Id = course.EmployeeId,
                CourseId = course.courseId,
                StartDate = course.StartDate
            };

            _context.Achievements.Add(AchievementCourse);
            await _context.SaveChangesAsync();
        }

        //Add description of course and completion date in achievement table
        public async Task FinishCourse(AchieveDate data)
        {
            var DataAchievement = await _context.Achievements
                .Where(x => x.Id == data.EmployeeId && x.CourseId == data.CourseId)
                .FirstOrDefaultAsync();

            if(DataAchievement.AchievDate == null && DataAchievement.Description == null)
            {
                DataAchievement.AchievDate = data.achieveDate;
                DataAchievement.Description = data.Description;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ApplicationException("Achievement is not found");
            }
        }

        //Registretion and Login:
        //Authentication (Login)
        //public AuthenticateResponse Authenticate(AuthenticateRequest users)
        //{
        //    var user = _context.Employees.Include(e => e.Permissions).SingleOrDefault(x => x.Email == users.Email);

        //    //Validate
        //    if (user == null)
        //    {
        //        throw new ApplicationException("Email or password is incorrect");

        //    }

        //    //Authentication Successfulreject 
        //    var response = _mapper.Map<AuthenticateResponse>(user);
        //    response.Token = _jwtUtils.GenerateToken(user);

        //    return response;
        //}

        //Register
        //public void Register(RegisterRequest registerUser)
        //{
        //    //validate
        //    if (_context.Employees.Any(x => x.Email == registerUser.Email))
        //    {
        //        throw new ApplicationException("Email '" + registerUser.Email + "' is already taken");
        //    }

        //    var user = _mapper.Map<Employee>(registerUser);

        //    //var password = registerUser.Password;
        //    //user.Password = BCrypt.Net.BCrypt.HashPassword(password);

        //    //Save data
        //    _context.Employees.Add(user);
        //    _context.SaveChanges();
        //}

        //Forget Password
        public async Task ForgetPassword(ForgetPassword data)
        {
            var User = await _userManager.FindByEmailAsync(data.Email);

            if(User == null)
            {
                throw new ApplicationException("Email not found");
            }
            
            var Token = await _userManager.GeneratePasswordResetTokenAsync(User);

            //Encoded
            var encodedToken = Encoding.UTF8.GetBytes(Token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"https://localhost:7061/ResetPassword?userid={User.Id}&token={validToken}";


            await _mailService.SendEmail(User.Email, "Reset Password", $"<h1>Welcome to Vital</h1>" +
                $"<p>Your request for resetting Vital account password, if this wasn't you please ignore this event. To reset password<a href='{url}'>Clicking here</a></p>");

            //if(result.IsCompletedSuccessfully)
            //{
            //    data.SendEmail = true;
            //}

            //return data.SendEmail;
        }

        //Reset Password
        public async Task ResetPassword(ResetPassword data)
        {
            var employee = await _userManager.FindByEmailAsync(data.Email);

            if (employee != null)
            {
                if (data.NewPassword != data.ConfirmNewPassword)
                {
                    throw new ApplicationException("Password does not match");
                }

                var decodedToken = WebEncoders.Base64UrlDecode(data.Token);
                string normalToken = Encoding.UTF8.GetString(decodedToken);

                var result = await _userManager.ResetPasswordAsync(employee, normalToken, data.NewPassword);

                if (result.Succeeded)
                {
                    throw new ApplicationException("Save Reset password :)");
                }
            }
        }

        //Change Password
        public async Task<bool> ChangePassword(ChangePassword data)
        {
            //var employee = await _context.Employees.FindAsync(darEmployeeId);
            var employeeId = _userServices.GetUserId();

            var employee = await _userManager.FindByIdAsync(employeeId);

            var result = await _userManager.ChangePasswordAsync(employee, data.currentPassword, data.newPassword);

            if (!result.Succeeded)
            {
                throw new ApplicationException("User creation failed! Please check user details and try again.");
            }
            return true;

            //return result.Succeeded;
            //if (employee != null)
            //{
            //    var CurrentPassword = employee.Password;
            //    var NewPassword = data.newPassword;
            //    var passwordConfigure = data.ConfirmNewPassword;

            //    if (CurrentPassword != data.currentPassword)
            //    {
            //        throw new ApplicationException("Password is incorrect");
            //    }
            //    else
            //    {
            //        if (NewPassword == passwordConfigure)
            //        {
            //            employee.Password = passwordConfigure;
            //            _context.SaveChanges();
            //        }
            //    }
            //}
        }

        //Employee:

        //Get Employees
        public async Task<List<EmployeeDTO>> GetEmployee()
        {
            var emp = await _context.Employees
                 .Include(ro => ro.Position)
                 .Include(ex => ex.Experiences)
                 .Include(t => t.Tracks)
                 .Include(r => r.Records)
                 .Include(c => c.Courses)
               // .Include(p => p.Permissions)
                .Include(a => a.Achievements)
                .ToListAsync();

            //Number of employee
            var numEmployee = emp.Count();

            return _mapper.Map<List<EmployeeDTO>>(emp);
        }

        //Get Employee by id
        public async Task<EmployeeDTO> GetEmployeeById(string id)
        {
            var employee = await _context.Employees
                .Include(ro => ro.Position)
                 .Include(ex => ex.Experiences)
                 .Include(t => t.Tracks)
                 .Include(r => r.Records)
                 .Include(c => c.Courses)
                // .Include(p => p.Permissions)
                .Include(a => a.Achievements).Where(x => x.Id == id).FirstOrDefaultAsync();

            return _mapper.Map<EmployeeDTO>(employee);
        }

        //Edit Employee using id
        public async Task UpdateEmployee(string id, UpdateEmployeeDTOs employee)
        {
            var UpdateEmployee = await _context.Employees.FindAsync(id);

            if (UpdateEmployee != null)
            {
                UpdateEmployee.Address = employee.Address;
                UpdateEmployee.PhoneNumber = employee.PhoneNumber;
                UpdateEmployee.Email = employee.Email;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ApplicationException("employee is not found");
            }
        }

        //Add experience 
        public async Task addExperience(AddExperienceDto data)
        {
            var experience = new Experience()
            {
                PositionName = data.PositionName,
                Year = data.Year,
                Description = data.Description,
                EmployeeId = data.EmployeeId
            };

            _context.Experiences.Add(experience);

            await _context.SaveChangesAsync();
        }

        //Get experinces for employee
        public async Task<ExperienceDTO> GetExperience(string id)
        {
            var data = await _context.Experiences.Where(x => x.EmployeeId == id).FirstOrDefaultAsync();

            return _mapper.Map<ExperienceDTO>(data);
        }

        //Number of employee for each Position:
        public async Task<int> GetEmployeePosition(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            int employees;

            if(position == null)
            {
                throw new ApplicationException("Position does not exist");
            }

            employees = _context.Employees.Count(r => r.PositionId == id);

            return employees;
        }

        //Department:

        //Get All Departments:
        public async Task<List<DepartmentDTO>> GetAllDepartment()
        {
            var departments = await _context.Departments
                .Include(r => r.Positions)
                .ToListAsync();

            //Number of departments
            var numDepartment = departments.Count();

            return _mapper.Map<List<DepartmentDTO>>(departments);
        }

        //Get Department using id :
        public async Task<DepartmentDTO> GetDepartmentById(int id)
        {
            var department = await _context.Departments
                .Include(r => r.Positions)
                .Where(x => x.DepartmentId == id).FirstOrDefaultAsync();

            return _mapper.Map<DepartmentDTO>(department);
        }

        //Position:

        //Get all Positions:
        public async Task<List<PositionDTO>> GetAllPosition()
        {
            var Positions = await _context.Positions
                .Include(e => e.Employees)
                .Include(t => t.Tracks)
                .Include(d => d.Department).ToListAsync();

            //Number of Positions
            var numPosition = Positions.Count();

            return _mapper.Map<List<PositionDTO>>(Positions);
        }

        //Get Position using id:
        public async Task<PositionDTO> GetPosition(int id)
        {
            var Position = await _context.Positions
                .Include(e => e.Employees)
                .Include(t => t.Tracks)
                .Include(d => d.Department)
                .Where(x => x.PositionId == id).FirstOrDefaultAsync();

            return _mapper.Map<PositionDTO>(Position);
        }

        //Trcak:

        //Add Tracks
        public async Task AddTrack(AddTrackDTO track)
        {
            var trackAdded = new Track()
            {
                EmployeeId = track.EmployeeId,
                TrackName = track.TrackName,
                PositionId = track.PositionId,
                RequiredSkills = track.RequiredSkills,
                Created = track.Created,
            };

            _context.Tracks.Add(trackAdded);
            await _context.SaveChangesAsync();
        }

        //Get all Trcaks
        public async Task<List<TrackDTO>> GetAllTrack()
        {
            var tracks = await _context.Tracks
                .Include(c => c.Courses)
                .Include(re => re.Records)
                .Include(r => r.Position)
                .Include(e => e.Employee).ToListAsync();

            //Number of tracks
            var numTrack = tracks.ToList().Count();

            return _mapper.Map<List<TrackDTO>>(tracks);
        }

        //Get Track using id:
        public async Task<TrackDTO> GetTrackById(int id)
        {
            var track = await _context.Tracks
                .Include(c => c.Courses)
                .Include(r => r.Records)
                .Include(ro => ro.Position)
                .Include(e => e.Employee)
                .Where(x => x.TrackId == id).FirstOrDefaultAsync();

            return _mapper.Map<TrackDTO>(track);
        }

        //Edit Track using id
        public async Task UpdateTrack(int id, UpdatetrackDTO track, string employeeId)
        {
            var employee = await _context.Employees.Where(x => x.Id == employeeId).FirstOrDefaultAsync();

            if (employee != null)
            {
                var UpdateTracks = await _context.Tracks.FindAsync(id);

                if (UpdateTracks != null)
                {
                    UpdateTracks.TrackName = track.TrackName;
                    UpdateTracks.RequiredSkills = track.RequiredSkills;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ApplicationException("Track is not found");
                }
            }
        }

        //Delete Track using id
        public async Task DeleteTrackById(int id)
        {
            //var employee = _context.Employees.Where(x => x.Id == response.EmployeeId).FirstOrDefaultAsync();

            //if (employee != null)
            //{
            //    var TrackId = new Track { TrackId = id };

            //    _context.Tracks.Remove(TrackId);

            //    await _context.SaveChangesAsync();
            //}

            var trackId = await _context.Tracks.FindAsync(id);

            if (trackId == null)
            {
                throw new ApplicationException("Track is not found");
            }

            _context.Tracks.Remove(trackId);
            await _context.SaveChangesAsync();
        }

        //Record:

        //Post Record
        public async Task AddRecord(AddRecordDTO record)
        {
            var recordAdded = new Record()
            {
                EmployeeId = record.EmployeeId,
                RecordName = record.RecordName,
                Description = record.Description,
                DepartmentId = record.DepartmentId,
                TrackId = record.TrackId,
                Created = record.Created,
            };

            _context.Records.Add(recordAdded);
            await _context.SaveChangesAsync();
        }

        //if admin approve record
        public async Task ApproveRecord(int id)
        {
            
            var record = await _context.Records.FindAsync(id);

            if (record != null)
            {
                record.Status = true;
                record.Wait = false;
            }

            await _context.SaveChangesAsync();
        }

        //if admin reject record
        public async Task RejectRecord(int id)
        {
            var record = await _context.Records.FindAsync(id);

            if (record != null)
            {
                record.Status = false;
                record.Wait = false;
            }

            await _context.SaveChangesAsync();
        }

        //Get All Records
        public async Task<List<RecordDTO>> GetAllRecord()
        {
            var record = await _context.Records
                .Include(l => l.Links)
                .Include(f => f.FileRs)
                //.Include(e => e.Employee.Permissions)
                //.Include(c => c.Employee.Courses)
                .Include(t => t.Track)
                .Include(t => t.Employee)
                .ToListAsync();

            //var num = _context.Records.Include(r => r.Track.Position).Count();

            var numRecord = record.Count();

            return _mapper.Map<List<RecordDTO>>(record);
        }


        //Get Record by id
        public async Task<RecordDTO> GetRecordById(int id)
        {
            var recordId = await _context.Records
                .Include(l => l.Links)
                .Include(r => r.FileRs)
                .Include(f => f.FileRs)
                //.Include(e => e.Employee.Permissions)
                //.Include(c => c.Employee.Courses)
                .Include(t => t.Track)
                .Where(x => x.RecordId == id).FirstOrDefaultAsync();

            return _mapper.Map<RecordDTO>(recordId);
        }

        //Edit Record using id
        public async Task UpdateRecord(int id, UpdateRecordDTO record, string employeeId)
        {
            var employee = await _context.Employees.Where(x => x.Id == employeeId)
                .FirstOrDefaultAsync();

            if (employee != null)
            {
                var UpdateRecord = await _context.Records.FindAsync(id);

                if (UpdateRecord != null)
                {
                    UpdateRecord.RecordName = record.RecordName;
                    UpdateRecord.Description = record.Description;
                    UpdateRecord.Status = false;
                    UpdateRecord.Wait = true;


                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ApplicationException("Record is not found");
                }
            }
        }

        //Delete Record using id
        public async Task DeleteRecordById(int id)
        {
            //var employee = _context.Employees.Where(x => x.Id == response.EmployeeId).FirstOrDefaultAsync();
            var recordId = await _context.Records.FindAsync(id);

            if (recordId == null)
            {
                throw new ApplicationException("Record is not found");
            }

            _context.Records.Remove(recordId);
            await _context.SaveChangesAsync();
        }

        //Number of record for each department
        public async Task<int> GetNumberRecord(int id)
        {

            var records = _context.Records.Count(re => re.DepartmentId == id);

            return records;
        }

        //Courses:

        //Add Courses
        public async Task AddCourse(AddCourseDTO course)
        {
            var CourseAdd = new Course()
            {
                CourseName = course.CourseName,
                Link_course = course.Link_course,
                RequiredSkills = course.RequiredSkills,
                Mandantory = course.Mandantory,
                TrackId = course.TrackId,
                Created = course.Created,
            };

            _context.Courses.Add(CourseAdd);
            await _context.SaveChangesAsync();
        }

        //if admin approve course
        public async Task ApproveUpdateRecord(int RecordId)
        {

            var record = await _context.Records.FindAsync(RecordId);

            if (record != null)
            {
                record.Status = true;
                record.Wait = false;
            }

            await _context.SaveChangesAsync();
        }

        //if admin reject course
        public async Task RejectUpdateRercord(int recordId)
        {
            
            var record = await _context.Records.FindAsync(recordId);

            if (record != null)
            {
                throw new ApplicationException("Record not approved");
            }

            await _context.SaveChangesAsync(); 
        }

        //Get All Courses
        public async Task<List<CourseDTO>> GetAllCourses()
        {
            var cour = await _context.Courses
                .Include(a => a.Achievements)
                .ToListAsync();

            //Number of Courses
            var numCourse = cour.Count();

            return _mapper.Map<List<CourseDTO>>(cour);
        }

        //Get Course by id
        public async Task<CourseDTO> GetCourse(int id)
        {
            var courId = await _context.Courses
                .Include(e => e.Employees)
                .Include(t => t.Track)
                .Include(a => a.Achievements)
                .Include(e => e.Track.Position.Department)
                .Where(x => x.CourseId == id).FirstOrDefaultAsync();

            return _mapper.Map<CourseDTO>(courId);
        }

        //Delete Course using id
        public async Task DeleteCourseById(int id)
        {
            var CourseId = await _context.Courses.FindAsync(id);

            if (CourseId == null)
            {
                throw new ApplicationException("Course is not found");
            }

            _context.Courses.Remove(CourseId);
            await _context.SaveChangesAsync();
        }

        //Links:

        //Add links
        public async Task AddLink(List<AddLinkDTO> links)
        {
            foreach (AddLinkDTO link in links)
            {
                var linkAdd = new List<Link>
                {
                    new Link
                    {
                        LinkName = link.LinkName,
                        LinkData = link.LinkData,
                        RecordId = link.RecordId,
                    }
                };

                _context.Links.AddRange(linkAdd.ToArray());

                await _context.SaveChangesAsync();
            }
        }

        //Get All Links:
        public async Task<List<LinkDTO>> GetAllLinks()
        {
            var links = await _context.Links
                .Include(r => r.Record)
                .ToArrayAsync();

            //Number of links
            var numLinks = links.Count();

            return _mapper.Map<List<LinkDTO>>(links);
        }

        //Get Link by id:
        public async Task<LinkDTO> GetLinkId(int id)
        {
            var linkId = await _context.Links
                .Include(r => r.Record)
                .Where(x => x.LinkId == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<LinkDTO>(linkId);
        }

        //Delete Link using id
        public async Task DeleteLinkById(int id)
        {
            var linkId = await _context.Links.FindAsync(id);

            if (linkId == null)
            {
                throw new ApplicationException("link is not found");
            }

            _context.Links.Remove(linkId);
            await _context.SaveChangesAsync();
        }
    }
}