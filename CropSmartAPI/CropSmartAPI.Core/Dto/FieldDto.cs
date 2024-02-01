using CropSmartAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Dto;

public class FieldDto
{
    public int Id { get; set; }
    public string CadastralNumber { get; set; }
    public string Name { get; set; }
    public double Area { get; set; }
    public PropertyRight PropertyRight { get; set; }
    public SoilType SoilType { get; set; }
    public double CoordinateX { get; set; }
    public double CoordinateY { get; set; }
    public int Userid { get; set; }
}
