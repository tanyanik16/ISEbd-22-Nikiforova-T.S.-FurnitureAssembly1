﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyFileImplement.Models
{
	public class Furniture
	{
		public int Id { get; set; }
		public string FurnitureName { get; set; }
		public decimal Price { get; set; }
		public Dictionary<int, int> FurnitureComponents { get; set; }
	}
}
