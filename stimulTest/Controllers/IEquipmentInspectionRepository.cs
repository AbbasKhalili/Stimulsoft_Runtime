using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace stimulTest.Controllers
{
    public interface IEquipmentInspectionRepository
    {
        Task<List<EquipmentInspectionViewModel>> GetReport(EquipmentInspectionDto dto);
    }

    public class EquipmentInspectionRepository : IEquipmentInspectionRepository
    {
        private readonly RTProSLContext _context;

        public EquipmentInspectionRepository(RTProSLContext context)
        {
            _context = context;
        }

        public async Task<List<EquipmentInspectionViewModel>> GetReport(EquipmentInspectionDto dto)
        {
            var parameters = new List<SqlParameter>()
            {
                new SqlParameter{ParameterName="DepartmentList", Value = dto.DepartmentList, SqlDbType = SqlDbType.VarChar,Size=-1},
                new SqlParameter{ParameterName="CategoryList", Value = dto.CategoryList, SqlDbType = SqlDbType.VarChar,Size=-1},
                new SqlParameter{ParameterName="Equip1", Value = dto.Equip1, SqlDbType = SqlDbType.VarChar,Size=10},
                new SqlParameter{ParameterName="Equip2", Value = dto.Equip2, SqlDbType = SqlDbType.VarChar,Size=10},
                new SqlParameter{ParameterName="Barcode1", Value = dto.Barcode1, SqlDbType = SqlDbType.VarChar,Size=17},
                new SqlParameter{ParameterName="Barcode2", Value = dto.Barcode2, SqlDbType = SqlDbType.VarChar,Size=17},
                new SqlParameter{ParameterName="Date1", Value = dto.Date1, SqlDbType = SqlDbType.Date},
                new SqlParameter{ParameterName="Date2", Value = dto.Date2, SqlDbType = SqlDbType.Date},
                new SqlParameter{ParameterName="InspectionType", Value = dto.InspectionType, SqlDbType = SqlDbType.VarChar,Size=10},
                new SqlParameter{ParameterName="Location", Value = dto.Location, SqlDbType = SqlDbType.VarChar,Size=-1},
                new SqlParameter{ParameterName="UserId", Value = dto.UserId, SqlDbType = SqlDbType.VarChar,Size=10},
                new SqlParameter{ParameterName="EquipList", Value = dto.EquipList, SqlDbType = SqlDbType.VarChar,Size=-1},
                new SqlParameter{ParameterName="Language", Value = dto.Language, SqlDbType = SqlDbType.VarChar,Size=10},
            };
            return await _context.ExecuteStoreProcedureAsync<EquipmentInspectionViewModel>("rpEquipmentInspection", parameters);
        }
    }
}