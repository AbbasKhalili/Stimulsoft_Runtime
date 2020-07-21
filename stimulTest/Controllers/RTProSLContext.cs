using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace stimulTest.Controllers
{
    public partial class RTProSLContext : DbContext
    {
        
        public RTProSLContext(DbContextOptions<RTProSLContext> options)
            : base(options)
        {
        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                //optionsBuilder.UseSqlServer("Server=192.168.1.101;Database=SL-Trunk-MBS;uid=rti;password=rti;MultipleActiveResultSets=true;", x => x.UseNetTopologySuite());
                throw new Exception("Connection string not found!");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Farhad: introduce two keys to this table
            

        }

        public async Task<List<T>> ExecuteStoreProcedureAsync<T>(string storedProcedure, List<SqlParameter> parameters) where T : new()
        {
            using (var cmd = this.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = storedProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        parameter.Value = parameter.Value ?? DBNull.Value;
                        cmd.Parameters.Add(parameter);
                    }
                }

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                using (var dataReader = await cmd.ExecuteReaderAsync())
                {
                    var result = DataReaderMapToList<T>(dataReader);
                    return result;
                }

            }
        }

        public List<T> ExecuteStoreProcedure<T>(string storedProcedure, List<SqlParameter> parameters) where T : new()
        {
            try
            {
                using (var cmd = this.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = storedProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;

                    // set some parameters of the stored procedure
                    foreach (var parameter in parameters)
                    {
                        parameter.Value = parameter.Value ?? DBNull.Value;
                        cmd.Parameters.Add(parameter);
                    }

                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        var result = DataReaderMapToList<T>(dataReader);
                        return result;
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private static List<T> DataReaderMapToList<T>(DbDataReader dr)
        {
            List<T> list = new List<T>();
            //try
            //{
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var obj = Activator.CreateInstance<T>();

                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {

                        //Skipping properties with [NotMapped] attribute
                        var attributes = prop.GetCustomAttributes(false);
                        if (attributes.ToList().Any(x => x.GetType() == typeof(NotMappedAttribute)))
                            continue;


                        if (!Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            //}
            //catch (Exception e)
            //{
            //    throw new Exception(e.Message);
            //}

            return new List<T>();
        }
    }
}