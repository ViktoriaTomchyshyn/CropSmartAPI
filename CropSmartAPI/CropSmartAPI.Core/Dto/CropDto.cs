using CropSmartAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Dto;

public class CropDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime SowingDate { get; set; }
    public DateTime HarvestDate { get; set; }
    public double? Fertility { get; set; }
    public string? Notes { get; set; }
    public int FieldId { get; set; }
}
