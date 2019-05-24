using System;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ChefsNDishes.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<Chef> Chefs {get;set;}
        public DbSet<Dish> Dishes {get;set;}
    }
    
    public class Chef
    {
       [Key]
       public int ChefId {get;set;}

       [Required]
       [Display(Name = "First Name")]
       public string FirstName {get;set;}

       [Required]
       [Display(Name = "Last Name")]
       public string LastName {get;set;}

       [Required]
       [CheckChefAge]
       [Display(Name = "Date of Birth")]
       public DateTime DOB {get;set;}
       
       public List<Dish> CreatedDishes {get;set;}
       public DateTime CreatedAt {get;set;} = DateTime.Now;
       public DateTime UpdateAt {get;set;} = DateTime.Now;

        public class CheckChefAge : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                int age = CalculateAge((DateTime)value);
                if (age < 18)
                {
                    return new ValidationResult("Chefs must be at least 18 years old!");
                }
                return ValidationResult.Success;
            }
        }

        public static int CalculateAge(DateTime dob)
        {
            int age = 0;
            age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
            {
                age--;
            }
            return age;
        }
    }

    public class Dish
    {
        [Key]
        public int DishId {get;set;}

        [Required]
        [Display(Name = "Name of Dish")]
        public string Name {get;set;}

        [Required]
        [Display(Name = "# of Calories")]
        public int Calories {get;set;}

        [Required]
        public int Tastiness {get;set;}

        [Required]
        public string Description {get;set;}

        [Display(Name = "Chef")]
        public int ChefId {get;set;}
        public Chef Creator {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdateAt {get;set;} = DateTime.Now;
    }
}