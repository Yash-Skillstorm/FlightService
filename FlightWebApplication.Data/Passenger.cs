using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public class Passenger
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Passenger_Name { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int Passenger_Age { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Passenger_Email { get; set; }
        public Passenger()
        {

        }
        public Passenger(string Pass_Name, int Pass_Age, string Pass_Email)
        {
            this.Passenger_Name = Pass_Name;
            this.Passenger_Age = Pass_Age;
            this.Passenger_Email = Pass_Email;
        }
    }
}
