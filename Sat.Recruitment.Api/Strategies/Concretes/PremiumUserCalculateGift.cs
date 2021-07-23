using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Strategies.Concretes
{
    public class PremiumUserCalculateGift : ICalculateGift
    {
        public decimal Calculate(decimal money)
        {
            if (money > 100)
            {
                var gif = money * 2;
                money += gif;
            }
            return money;
        }
    }
}
