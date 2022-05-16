namespace MISA.Amis.Api.Entities
{
    public class Employee
    {   
        /// <summary>
        /// Thông tin nhân viên
        /// </summary>
        /// CreateBy:Trần Công Minh(3.5.2022)
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính(0-Nữ,1-Nam,2-Khác)
        /// </summary>
        public int? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string? Email { get; set; }
    }
}
