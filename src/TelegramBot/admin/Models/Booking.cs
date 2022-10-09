using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Vitalik.Models
{
    public class Booking
    {
        //public int Id { get; set; }
        //[Key, Column(Order = 0)]
        public int SubjectId { get; set; }
        //[Key, Column(Order = 1)]
        public int UserId { get; set; }
        public string? Date { get; set; }
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }

        public Booking()
        {
        }
    }
}
