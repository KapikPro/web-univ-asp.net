using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Hours
    {
        public double academicHours { get; set; }
        public double asronomicHours { get; set; }
        public Hours(double Achours, double Ashours) 
        {
            academicHours = Achours;
            asronomicHours = Ashours;
        }
    }
}
