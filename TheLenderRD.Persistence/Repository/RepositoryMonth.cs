using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TheLenderRD.Domain.Dto;
using TheLenderRD.Persistence.EntensionMethods;
//

namespace TheLenderRD.Persistence.Repository
{
    public class RepositoryMonth
    {

        #region Singletom
        private static RepositoryMonth Instantice { get; set; }

        public static RepositoryMonth GetInstance()
        {
            if (Instantice == null)
                Instantice = new RepositoryMonth();

            return Instantice;
        }

        #endregion

        private RepositoryMonth()
        {

        }

        public async Task<List<MonthDto>> Get()
        {
            List<MonthDto> months = new List<MonthDto>();

            try
            {
                SqlDataReader reader = await DataAccess.DataAccess
                    .GetInstance()
                    .OpenConnection()
                    .UserStoreProcedure("GetMonths")
                    .ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    months.Add(new MonthDto
                    {
                        Id = reader["Id"].ToInt(),
                        Description = reader["Description"].ToString(),
                        Value = reader["Value"].ToInt()
                    });
                }
            }
            catch
            {
                months.Add(new MonthDto { IsError = true });
            }

            DataAccess.DataAccess.GetInstance().CloseConnection();

            return months;
        }

    }
}
