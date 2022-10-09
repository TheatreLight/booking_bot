namespace ApiDB.dal.Interface
{
    public class NewBookingVM
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int UserId { get; set; }
        public string Date { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
    }
}
