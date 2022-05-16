using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Api.Entities;
using MySqlConnector;
using Dapper;
namespace MISA.Amis.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //Lấy dữ liệu:- GET
        /// <summary>
        /// Lấy toàn bộ danh sách nhân viên
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: Trần Công Minh(2-5-2022)
        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                // 1.Khai báo thông tin kết nối đến Database:
                var connectionString = "Host=3.0.89.182;Port=3306;Database=MISA.WEB03.TCMINH;User Id=dev;Password=12345678";

                // 2.Khởi tạo kết nối --> Sử dụng MySqlConnector
                var sqlConnection = new MySqlConnection(connectionString);

                // 3.Thực hiện lấy dữ liệu -->Dapper
                var data = sqlConnection.Query<object>("SELECT * FROM Employee");

                return Ok(data);
            }
            catch (Exception ex)
            {
                ///Ghi log vào hệ thống 
                ///
                var result = new MISAServiceResult
                {
                    UserMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
                    DevMsg = ex.Message,
                    data = null,
                };

                return StatusCode(500, result);
                
            }
            
        }


        /// <summary>
        /// Lấy 1 nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpGet("{employeeCode}")]
        public IActionResult GetByCode(string? employeeCode)
        {
            // 1.Khai báo thông tin kết nối đến Database:
            var connectionString = "Host=3.0.89.182;Port=3306;Database=MISA.WEB03.TCMINH;User Id=dev;Password=12345678";

            // 2.Khởi tạo kết nối --> Sử dụng MySqlConnector
            var sqlConnection = new MySqlConnection(connectionString);
            //3.Gọi câu lệnh truy vấn
            var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeCode = @EmployeeCode";

            //Xử lí param cho lệnh sql  tranh sql injection
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add("@EmployeeCode", employeeCode);

            // 4.Thực hiện lấy dữ liệu -->Dapper
            var employee=sqlConnection.QueryFirstOrDefault<object>(sqlCommand,param: dynamicParam);

            return Ok(employee);
        }


        //Thêm mới dữ liệu: - POST
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            try
            {
                //Tạo EmployeeId mới cho nhân viên
                employee.EmployeeId = Guid.NewGuid();
                // 1.Khai báo thông tin kết nối đến Database:
                var connectionString = "Host=3.0.89.182;Port=3306;Database=MISA.WEB03.TCMINH;User Id=dev;Password=12345678";

                // 2.Khởi tạo kết nối --> Sử dụng MySqlConnector
                var sqlConnection = new MySqlConnection(connectionString);
                //3.Gọi câu lệnh truy vấn
                var sqlCommand = $"Proc_InsertEmployee";

                //Xử lí param cho lệnh sql  tranh sql injection
                var dynamicParam = new DynamicParameters();
                dynamicParam.Add("@m_EmployeeId", employee.EmployeeId);
                dynamicParam.Add("@m_EmployeeCode",employee.EmployeeCode);
                dynamicParam.Add("@m_FullName", employee.FullName);
                

                // 4.Thực hiện thêm mới dữ diệu dữ liệu -->Dapper
                var res=sqlConnection.Execute(sql:sqlCommand,param:dynamicParam,commandType:System.Data.CommandType.StoredProcedure);
                if(res>0)
                {
                    return StatusCode(201,res);
                }
                else
                {
                    return StatusCode(200, res);
                }
            }
            catch (Exception ex)
            {
                var result = new MISAServiceResult
                {
                    UserMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được trợ giúp",
                    DevMsg = ex.Message,
                    data = null,
                };

                return StatusCode(500, result);

            }
            return Ok(employee);
        }

        //Sửa dữ liệu: - PUT
        [HttpPut]
        public IActionResult Put(Employee employee, Guid employeeId)
        {
            return Ok();
        }

        //Xóa dữ liệu: DELETE
        [HttpDelete]
        public IActionResult Delete(Employee employee)
        {
            return Ok();
        }
    }
}
