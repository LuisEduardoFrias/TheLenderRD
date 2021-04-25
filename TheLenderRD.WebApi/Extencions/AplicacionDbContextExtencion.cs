using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheLenderRD.Domain.Entitys;
using TheLenderRD.Persistence.DbContext;

namespace TheLenderRD.WebApi.Extencions
{
    public enum DBState
    {
        Fetched,
        Unfetched,
        Unmigrated
    }

    public static class AplicacionDbContextExtencion
    {

        public static DBState IsDataFetched(this TheLenderRD_DBContext context)
        {
            try
            {
                return (context.AgeRates.Any()) ? DBState.Fetched : DBState.Unfetched;
            }
            catch
            {
                return DBState.Unmigrated;
            }
        }

        public static void FetchDataBase(this TheLenderRD_DBContext context)
        {
            try
            {
                if (IsDataFetched(context) == DBState.Unmigrated)
                {
                    context.Database.Migrate();
                }

                if (!context.AgeRates.Any())
                {
                    context.AgeRates.AddRange(new AgeRate[]
                    {
                    new AgeRate
                    {
                        Age = 18,
                        Rate = 1.20M,
                    },
                    new AgeRate
                    {
                        Age = 19,
                        Rate = 1.18M
                    },
                    new AgeRate
                    {
                        Age = 20,
                        Rate = 1.16M

                    },
                    new AgeRate
                    {
                        Age = 21,
                        Rate = 1.14M
                    },
                    new AgeRate
                    {
                        Age = 22,
                        Rate = 1.12M
                    },
                    new AgeRate
                    {
                        Age = 23,
                        Rate = 1.10M
                    },
                    new AgeRate
                    {
                        Age = 24,
                        Rate = 1.08M
                    },
                    new AgeRate
                    {
                        Age = 25,
                        Rate = 1.08M
                    }
                    });

                    context.SaveChanges();
                }

                if (!context.Months.Any())
                {
                    context.Months.AddRange(new Month[]
                    {
                    new Month
                    {
                        Description = "3 Meses",
                        Value = 3
                    },
                    new Month
                    {
                        Description = "6 Meses",
                        Value = 6
                    },
                    new Month
                    {
                        Description = "9 Meses",
                        Value = 9
                    },
                    new Month
                    {
                        Description = "12 Meses",
                        Value = 12
                    },

                    });
                    context.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}
