using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class Activity
    {
        public int ActivityId {get;set;}
        [Required]
        [Display(Name = "Name")] 
        public string Name {get;set;}

       
        public DateTime Date {get;set;}
     
        public TimeSpan Time {get;set;}
    
        public int DurationTime {get;set;}
        
        public string DurationString {get;set;}
        [Required]
        [Display(Name = "Description:")] 
        public string Description {get;set;}

        public int UserId {get;set;}

        public User User {get; set;}

        public virtual List<RSVP> RSVPs {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}