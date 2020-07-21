using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace stimulTest.Controllers
{
    public interface IRepairReportRepository
    {
        Task<List<RepairReportViewModel>> GetReport(RepairReportDto dto);
    }

    public class RepairReportRepository : IRepairReportRepository
    {
        private readonly RTProSLContext _context;

        public RepairReportRepository(RTProSLContext context)
        {
            _context = context;
        }

        public async Task<List<RepairReportViewModel>> GetReport(RepairReportDto dto)
        {
            //var parameters = new List<SqlParameter>()
            //{
            //    //new SqlParameter(){ParameterName="RETURN_VALUE", Value = dto.RETURN_VALUE, SqlDbType = System.Data.SqlDbType.Int},
            //    new SqlParameter(){ParameterName="Date1", Value = dto.Date1, SqlDbType = SqlDbType.Date},
            //    new SqlParameter(){ParameterName="Date2", Value = dto.Date2, SqlDbType = SqlDbType.Date},
            //    new SqlParameter(){ParameterName="Production", Value = dto.Production, SqlDbType = SqlDbType.VarChar,Size=10},
            //    new SqlParameter(){ParameterName="RepLocationList", Value = dto.RepLocationList, SqlDbType = SqlDbType.VarChar,Size=-1},
            //    new SqlParameter(){ParameterName="Location", Value = dto.Location, SqlDbType = SqlDbType.VarChar,Size=-1},
            //    new SqlParameter(){ParameterName="Currency", Value = dto.Currency, SqlDbType = SqlDbType.VarChar,Size=10},
            //    new SqlParameter(){ParameterName="UserId", Value = dto.UserId, SqlDbType = SqlDbType.VarChar,Size=10},
            //    new SqlParameter(){ParameterName="EquipList", Value = dto.EquipList, SqlDbType = SqlDbType.VarChar,Size=-1},
            //    new SqlParameter(){ParameterName="Language", Value = dto.Language, SqlDbType = SqlDbType.VarChar,Size=10},
            //    new SqlParameter(){ParameterName="OwnerList", Value = dto.OwnerList, SqlDbType = SqlDbType.VarChar,Size=-1},
            //    new SqlParameter(){ParameterName="IncludeBilled", Value = dto.IncludeBilled, SqlDbType = SqlDbType.Bit},
            //    new SqlParameter(){ParameterName="CurrentlyInRepair", Value = dto.CurrentlyInRepair, SqlDbType = SqlDbType.Bit},
            //};
            //return await _context.ExecuteStoreProcedureAsync<RepairReportViewModel>("rpRepair", parameters);

            return new List<RepairReportViewModel>()
            {
                new RepairReportViewModel()
                {
                    Barcode = new Random().Next().ToString(),
                    Category = Guid.NewGuid().ToString(),
                    Cost = 10,
                    Customer = "aa",
                    DateIn = DateTime.Now,
                    DateOut = DateTime.Now,
                    Department = "pol",
                    Description = Guid.NewGuid().ToString(),
                    Equipment = new Random().Next().ToString()
                },
                new RepairReportViewModel()
                {
                    Barcode = new Random().Next().ToString(),
                    Category = Guid.NewGuid().ToString(),
                    Cost = 10,
                    Customer = "aa1",
                    DateIn = DateTime.Now,
                    DateOut = DateTime.Now,
                    Department = "pol2",
                    Description = Guid.NewGuid().ToString(),
                    Equipment = new Random().Next().ToString()
                }
            };
        }
    }
}