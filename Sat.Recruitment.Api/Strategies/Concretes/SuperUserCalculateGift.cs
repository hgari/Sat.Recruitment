using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Strategies.Concretes
{
    public class SuperUserCalculateGift : ICalculateGift
    {
        public decimal Calculate(decimal money)
        {
            if (money > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gif = money * percentage;
                money += gif;
            }
            return money;
        }
    }
}
