using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repository;

        public AttendanceService(
            IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AttendanceDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();

            return data.Select(a => new AttendanceDto
            {
                AttendanceId = a.AttendanceId,
                EmployeeId = a.EmployeeId,
                AttendanceDate = a.AttendanceDate,
                Status = a.Status,
                CheckIn = a.CheckIn,
                CheckOut = a.CheckOut
            }).ToList();
        }

        public async Task<AttendanceDto?> GetByIdAsync(int id)
        {
            var a = await _repository.GetByIdAsync(id);

            if (a == null)
                return null;

            return new AttendanceDto
            {
                AttendanceId = a.AttendanceId,
                EmployeeId = a.EmployeeId,
                AttendanceDate = a.AttendanceDate,
                Status = a.Status,
                CheckIn = a.CheckIn,
                CheckOut = a.CheckOut
            };
        }

        public async Task<List<AttendanceDto>>
            GetByEmployeeIdAsync(int employeeId)
        {
            var data =
                await _repository.GetByEmployeeIdAsync(employeeId);

            return data.Select(a => new AttendanceDto
            {
                AttendanceId = a.AttendanceId,
                EmployeeId = a.EmployeeId,
                AttendanceDate = a.AttendanceDate,
                Status = a.Status,
                CheckIn = a.CheckIn,
                CheckOut = a.CheckOut
            }).ToList();
        }

        public async Task<AttendanceDto> AddAsync(
            AttendanceDto dto)
        {
            var attendance = new Attendance
            {
                EmployeeId = dto.EmployeeId,
                AttendanceDate = dto.AttendanceDate,
                Status = dto.Status,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut
            };

            var result =
                await _repository.AddAsync(attendance);

            dto.AttendanceId = result.AttendanceId;

            return dto;
        }

        public async Task<bool> UpdateAsync(
            int id,
            AttendanceDto dto)
        {
            var attendance =
                await _repository.GetByIdAsync(id);

            if (attendance == null)
                return false;

            attendance.EmployeeId = dto.EmployeeId;
            attendance.AttendanceDate = dto.AttendanceDate;
            attendance.Status = dto.Status;
            attendance.CheckIn = dto.CheckIn;
            attendance.CheckOut = dto.CheckOut;

            await _repository.UpdateAsync(attendance);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var attendance =
                await _repository.GetByIdAsync(id);

            if (attendance == null)
                return false;

            await _repository.DeleteAsync(attendance);

            return true;
        }
    }
}