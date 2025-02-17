﻿namespace ChefLocation.WebApi.Entities
{
    public class Testimonial : BaseTable
    {
        public int TestimonialId { get; set; }
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
    }
}
