using System;

namespace TheLenderRD.Domain.Services
{
    public static class Calculations
    {
        public static decimal QuotaCalculation(decimal amount, decimal rate, int months) => ((amount * rate) / months);

        public static int CalculateAge(DateTime DateOfBirth)
        {
            int suma = 0;

            if (DateTime.Now.Month == DateOfBirth.Month)
                if (DateOfBirth.Day >= DateTime.Now.Day)
                    suma = 1;

            if (DateOfBirth.Month > DateTime.Now.Month)
                suma = 1;

            return Convert.ToInt32(DateTime.Now.Year - (DateOfBirth.Year + suma));
        }
    }
}
