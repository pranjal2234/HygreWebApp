namespace HygreWebApp.Models
{
    public class TrainingBooking
    {
        public int Id { get; set; }

        public string StudentName { get; set; }
        public string Email { get; set; }

        public int TrainingWeekId { get; set; }
        public TrainingWeek TrainingWeek { get; set; }

        public DateTime BookedOn { get; set; } = DateTime.Now;
    }

}
