using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAppWebAPI.Models
{
    public class CodeDirection
    {
        /*
         * To create the models I did 
         * Scaffold-DbContext "Server=.;Database=CustomerDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
         * This allowed the system to create the models class and the CustomerDBContext
         * 
         * After securing the connection by changing it to default connection in the app settings
         * the above code will now be 
         * Scaffold-DbContext -Connection Name=DefaultConnection Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
         * 
         * the "-force" is to force the vs to recreate the models folders
         * 
         * to get pluralized names you can install bricelam (but at your own risk)
         */
    }
}
