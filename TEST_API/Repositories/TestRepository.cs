using Dapper;
using Dapper.Oracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TEST_API.Models;

namespace TEST_API.Repositories
{
    public class TestRepository
    {
        private readonly string _connectionString;

        public TestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<GeneralBranchPerformanceRank> CallStoredProcedure(int month, string region)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                // Define the input and output parameters
                var parameters = new OracleDynamicParameters();
                parameters.Add("p_recordset", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                parameters.Add("p_month", month, OracleMappingType.Int32, ParameterDirection.Input);
                parameters.Add("p_region", region, OracleMappingType.Varchar2, ParameterDirection.Input);

                // Execute the stored procedure and map the result to the MotorPerformanceData model
                var result = conn.Query<GeneralBranchPerformanceRank>(
                    "SLIC_AGENT.GENERAL_NMOTOR_PERF_RGBRCMLTV",
                    parameters,
                    commandType: CommandType.StoredProcedure).ToList();

                return result;
            }
        }
    }
}