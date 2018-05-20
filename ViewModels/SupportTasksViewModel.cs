using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class SupportTasksViewModel
    {
            public int Id { get; set; }
            public string TaskName { get; set; }
            public string Comments { get; set; }
            public bool? Active { get; set; }
            public string UserID { get; set; }
    }
}
