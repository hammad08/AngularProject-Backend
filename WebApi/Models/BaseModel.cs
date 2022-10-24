using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public enum state
    {
        Unchanged = 0,
        Added=  1,
        Modified =2,
        Deleted=3
    };

    public class BaseModel
    {
        public state StateEnum { get; set; } 
    }
}
