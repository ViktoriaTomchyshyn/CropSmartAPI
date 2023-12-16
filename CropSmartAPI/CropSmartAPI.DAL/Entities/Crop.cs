using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.DAL.Entities;

public class Crop
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime SowingDate { get; set; }
    public DateTime HarverstDate { get; set; }
    public double Fertility { get; set; }
    public string Notes { get; set; }
    public int FieldId { get; set; }
    public Field Field { get; set; }
    public ICollection<Fertilizer> Fertilizers { get; set; }
}
