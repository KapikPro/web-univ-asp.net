using System.ComponentModel.DataAnnotations;

namespace Kursach.Models
{
    public class Personn
    {
        public int Id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
