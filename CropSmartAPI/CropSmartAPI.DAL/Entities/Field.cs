using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.DAL.Entities;

public class Field
{
    public int Id { get; set; }
    public string CadastralNumber { get; set; }
    public string Name { get; set; }
    public double Area { get; set; }
    public PropertyRight PropertyRight { get; set; }

    //maybe rent time 
    public int Userid { get; set; }
    public User User { get; set; }
    public ICollection<Crop> Crops { get; set; }
}
