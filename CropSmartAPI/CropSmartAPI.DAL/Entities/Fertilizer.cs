using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.DAL.Entities;

public class Fertilizer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public long Quantity { get; set; }
    public int CropId { get; set; }
    public Crop Crop { get; set; }
}
