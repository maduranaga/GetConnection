using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Services
{
    public class GenrateOtp
    {
        public async Task<int> GenerateOtp()
        {
            try
            {
                int min = 1000;
                int max = 9999;
                int otp = 0;

                Random rdm = new Random();
                otp = rdm.Next(min, max);
                return await Task.FromResult(otp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
