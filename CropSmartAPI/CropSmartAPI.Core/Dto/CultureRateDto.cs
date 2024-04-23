using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Dto;

public class CultureRateDto
{
    public int Id { get; set; }
    public string Culture { get; set; }
    public string Predecessor { get; set; }
    public int Rate { get; set; }
}
