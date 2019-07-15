using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AquaMarket.Models
{
    public class File
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int PlantId { get; set; }
    }
}