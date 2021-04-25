using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TheLenderRD.Domain.Dto;
using TheLenderRD.Persistence.EntensionMethods;
//

namespace TheLenderRD.Persistence.Repository
{
    public class RepositoryAgeRate
    {

        #region Singletom

        private static RepositoryAgeRate Instantice { get; set; }

        public static RepositoryAgeRate GetInstance()
        {
            if (Instantice == null)
                Instantice = new RepositoryAgeRate();

            return Instantice;
        }

        #endregion

        private RepositoryAgeRate()
        {

        }


        public async Task<List<AgeRageDto>> Get()
        {
            List<AgeRageDto> ageRage = new List<AgeRageDto>();

            try
            {
                SqlDataReader reader = await DataAccess.DataAccess
                    .GetInstance()
                    .OpenConnection()
                    .UserStoreProcedure("GetAgeRates")
                    .ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    ageRage.Add(new AgeRageDto
                    {
                        Id = reader["Id"].ToInt(),
                        Age = reader["Age"].ToInt(),
                        Rate = reader["Rate"].ToDecimal()
                    });
                }
            }
            catch
            {
                ageRage.Add(new AgeRageDto { IsError = true });
            }

            DataAccess.DataAccess.GetInstance().CloseConnection();

            return ageRage;
        }

    }
}
