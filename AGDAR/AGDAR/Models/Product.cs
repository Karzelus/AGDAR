﻿namespace AGDAR.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public double Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int StateId { get; set; }
        public string Img { get; set; }
    }
}