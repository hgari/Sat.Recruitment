using Sat.Recruitment.Api.Strategies;
using Sat.Recruitment.Api.Strategies.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Sat.Recruitment.Api.Data
{
    public class User
    {

        readonly Dictionary<string, Func<ICalculateGift>> strategies = new Dictionary<string, Func<ICalculateGift>>() {
            { "Normal", () => new NormalUserCalculateGift() },
            { "Premium", () => new PremiumUserCalculateGift() },
            { "SuperUser", () => new SuperUserCalculateGift() }
    
        };

        
        
        public void Initialize()
        {
            ICalculateGift selectedStrategy = strategies[this.UserType]();
            this.Money = selectedStrategy.Calculate(this.Money);

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
