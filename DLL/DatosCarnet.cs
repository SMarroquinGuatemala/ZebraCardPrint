using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZebraCardPrint.DLL
{
   public class DatosCarnet
   {
      DatabaseProviderFactory factory = new DatabaseProviderFactory();
     

      public void DeleteDatosCarnet()
      {
         Database namedDB = factory.Create("Produccion");
         namedDB.ExecuteNonQuery(System.Data.CommandType.Text, "delete DbRecursosHumanos..TblWCarnet where Computadora = HOST_NAME()");

      }

      public void SPDatosCarnet(string NumeroDeEmpleado)
      {
         Database namedDB = factory.Create("Produccion");
         namedDB.ExecuteNonQuery("UpImprimirCarnet",Environment.MachineName, NumeroDeEmpleado);
      }

      public DataTable LoadCarnet()
      {
         Database namedDB = factory.Create("Produccion");
         return    namedDB.ExecuteDataSet(System.Data.CommandType.Text, "select  *  from DbRecursosHumanos..TblWCarnet where Computadora = HOST_NAME()").Tables[0];
      }
   }
}
