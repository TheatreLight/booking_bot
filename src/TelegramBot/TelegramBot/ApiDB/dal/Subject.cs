// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using ApiDB.dal.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiDB
{
    public partial class Subject : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string NameSubject { get; set; }
        [StringLength(100)]
        public string Info { get; set; }
        public int? Level { get; set; }
        public int? NumberRoom { get; set; }
        public int Type { get; set; }
        public School21 School21 { get; set; }
        public int Campus { get; set; }
        public int securType { get; set; }
        [StringLength(4)]
        public string MinTime { get; set; }   
        public List<Booking> bookings { get; set; }
        public bool Block { get; set; }
    }
}