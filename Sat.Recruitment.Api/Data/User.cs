using Sat.Recruitment.Api.Strategies;
using Sat.Recruitment.Api.Strategies.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Data
{
    public class User
    {
        /*
        Dictionary<string, ICalculateGift> strategies = new Dictionary<string, ICalculateGift>() {
            { "Normal", new NormalUserCalculateGift() },
            { "Premium", new PremiumUserCalculateGift() },
            { "SuperUser", new SuperUserCalculateGift() }
        };
        ICalculateGift selectedStrategy;
        */
        readonly Dictionary<string, Func<ICalculateGift>> strategies = new Dictionary<string, Func<ICalculateGift>>() {
            { "Normal", () => new NormalUserCalculateGift() },
            { "Premium", () => new PremiumUserCalculateGift() },
            { "SuperUser", () => new SuperUserCalculateGift() }
    
        };

        
        
        public void Initialize()
        {
            ICalculateGift selectedStrategy = strategies[this.UserType]();
            this.Money = selectedStrategy.Calculate(this.Money);
            /*
            if (UserType == "Normal")
            {
                if (Money> 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = Money * percentage;
                    Money +=  gif;
                }
                if (Money < 100)
                {
                    if (Money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = Money * percentage;
                        Money+= gif;
                    }
                }
            }
            if (UserType == "SuperUser")
            {
                if (Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = Money * percentage;
                    Money +=  gif;
                }
            }
            if (UserType == "Premium")
            {
                if (Money > 100)
                {
                    var gif = Money * 2;
                    Money +=  gif;
                }
            }
            */
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }

    }
}
