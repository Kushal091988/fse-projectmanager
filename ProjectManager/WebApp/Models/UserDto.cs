using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        public float Value { get; set; }

        public string Description { get; set; }

        public DateTime MeasuredAt { get; set; }
    }
}