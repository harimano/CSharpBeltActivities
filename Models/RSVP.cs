using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class RSVP
    {
        public int RSVPId {get;set;}

        public int UserId {get;set;}

        public int ActivityId {get;set;}

        public User User {get;set;}

        public Activity Activity {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}