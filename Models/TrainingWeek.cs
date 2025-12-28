namespace HygreWebApp.Models
{
    public class TrainingWeek
    {
        public int Id { get; set; }

        public DateTime WeekStartDate { get; set; }  // Monday
        public int MaxSeats { get; set; } = 20;

        public ICollection<TrainingBooking> Bookings { get; set; }
    }

}
