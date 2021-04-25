using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TheLenderRD.Domain.Entitys;
//

namespace TheLenderRD.Persistence.Repository
{
    public class RepositoryLog
    {

        #region Singletom

        private static RepositoryLog Instantice { get; set; }

        public static RepositoryLog GetInstance()
        {
            if (Instantice == null)
                Instantice = new RepositoryLog();

            return Instantice;
        }

        #endregion

        private RepositoryLog()
        {

        }

        public async Task<bool> Create(Log log)
        {
            bool result = false;

            try
            {
                result = await DataAccess.DataAccess.GetInstance().OpenConnection().UserStoreProcedure("InsertLog",
                new SqlParameter[]
                {
                    new SqlParameter
                    {
                        ParameterName = "@ConsultationDate",
                        DbType = System.Data.DbType.DateTime,
                        Value = log.ConsultationDate
                    },
                    new SqlParameter
                    {
                        ParameterName = "@Edad",
                        DbType = System.Data.DbType.Int32,
                        Value = log.Edad
                    },
                    new SqlParameter
                    {
                        ParameterName = "@Amount",
                        DbType = System.Data.DbType.Decimal,
                        Value = log.Amount
                    },
                    new SqlParameter
                    {
                        ParameterName = "@AccountValue",
                        DbType = System.Data.DbType.Decimal,
                        Value = log.AccountValue
                    },
                    new SqlParameter
                    {
                        ParameterName = "@QueryIp",
                        DbType = System.Data.DbType.String,
                        Value = log.QueryIp
                    },
                    new SqlParameter
                    {
                        ParameterName = "@MonthId",
                        DbType = System.Data.DbType.Int32,
                        Value = log.MonthId
                    }

                }).ExecuteNonQueryAsync() != -1;
            }
            catch { }

            DataAccess.DataAccess.GetInstance().CloseConnection();

            return result;

        }

    }
}
