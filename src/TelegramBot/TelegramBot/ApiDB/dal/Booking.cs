﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using ApiDB.dal.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDB
{
    public partial class Booking 
    {
        [Key]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int UserId { get; set; }
        public string Date { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public Subject Subject { get; set; }
        public User User { get; set; }


    }
}