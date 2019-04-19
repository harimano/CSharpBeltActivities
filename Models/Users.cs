using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "First Name:")] 
        [Required]
        [MinLength(2,ErrorMessage="First Name must be 3 characters or longer!")]
        public string FirstName {get;set;}

        [Display(Name = "Last Name:")] 
        [Required]
        [MinLength(2,ErrorMessage="Last Name must be 3 characters or longer!")]
        public string LastName {get;set;}

        [Display(Name = "Email:")] 
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email {get;set;}
        
        [Display(Name = "Password:")] 
        [Required]
        [DataType(DataType.Password)]
        [PasswordValidate]
        [MinLength(8,ErrorMessage="Password must be 8 characters or longer!")]
        public string Password {get;set;}

        public virtual List<Activity> Activities {get;set;}
        public  virtual List<RSVP> RSVPs {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}

    }
}