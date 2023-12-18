using CropSmartAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Dto;

public class FertilizerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public long Quantity { get; set; }
    public int CropId { get; set; }
}
