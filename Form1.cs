using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Zebra.Sdk.Card.Containers;
using Zebra.Sdk.Card.Enumerations;
using Zebra.Sdk.Card.Graphics;
using Zebra.Sdk.Card.Graphics.Enumerations;
using Zebra.Sdk.Card.Job;
using Zebra.Sdk.Card.Printer;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer.Discovery;
using Zebra.Sdk.Card.Printer.Discovery;

namespace ZebraCardPrint
{
   public partial class FrmCardPrint : Form
   {
      private const int CARD_FEED_TIMEOUT = 30000;
      private string Printer = null;
      DataTable dataTable = new  DataTable();
      
      public FrmCardPrint()
      {
         InitializeComponent();
      }

      #region Graphics
      // <exception cref="ConnectionException"></exception>
      // <exception cref="IOException"></exception>
      // <exception cref="NotSupportedException"></exception>
      // <exception cref="System.Security.SecurityException"></exception>
      // <exception cref="UnauthorizedAccessException"></exception>
      // <exception cref="Zebra.Sdk.Card.Exceptions.ZebraCardException"></exception>
      // <exception cref="Zebra.Sdk.Device.ZebraIllegalArgumentException"></exception>

      private static ZebraCardImageI DrawCarnet(ZebraGraphics graphics, PrintType printType, DataTable data)
      {
         Font font = new Font("Arial", 9, FontStyle.Bold );
         Font fontlabel = new Font("Arial", 7, FontStyle.Bold);

         graphics.Initialize(0, 0, OrientationType.Landscape, printType, Color.White);

         string colorImagePath = @"C:\logos\" + data.Rows[0]["Empresa"].ToString() + ".jpg";
         byte[] imageData = File.ReadAllBytes(colorImagePath);

         string Nombre = data.Rows[0]["Nombres"].ToString();
         string Apellidos = data.Rows[0]["Apellidos"].ToString();

         graphics.DrawImage(imageData,15, 15, 450, 150, RotationType.RotateNoneFlipNone);
         graphics.DrawText("Nombres:", fontlabel, Color.Black, 40, 165, 0);
         graphics.DrawText(Nombre,font , Color.Black,40,195, 0);
         graphics.DrawText("Apellidos:", fontlabel, Color.Black, 40, 250, 0);
         graphics.DrawText(Apellidos, font, Color.Black, 40, 280, 0);
         return graphics.CreateImage();
      }
      private static  List<GraphicsInfo> DrawGraphics(ZebraCardPrinter zebraCardPrinter, DataTable data)
      {
         // Generate image data
         ZebraCardImageI zebraCardImage = null;
         
         //byte[] fontFields = System.Text.Encoding.ASCII.GetBytes("Arial");
         

         List<GraphicsInfo> graphicsData = new List<GraphicsInfo>();
         using (ZebraGraphics graphics = new ZebraCardGraphics(zebraCardPrinter))
         {
            // Front side color
           

            zebraCardImage = DrawCarnet(graphics, PrintType.Color, data);
            //zebraCardImage = DrawImage(graphics, PrintType.Color, imageData, 0, 0, 450, 150);
            
            //graphics.DrawImage(imageData, 0, 450, 150, 0, RotationType.RotateNoneFlipXY);
           

            //zebraCardImage = graphics.CreateImage();

            graphicsData.Add(AddImage(CardSide.Front, PrintType.Color, 0, 0, -1,zebraCardImage));            
            graphics.Clear();

            //// Front side full overlay
            //graphicsData.Add(AddImage(CardSide.Front, PrintType.Overlay, 0, 0, 1, null));

            //// Back side mono
            //string monoImagePath = @"C:\logos\06.jpg";
            //imageData = File.ReadAllBytes(monoImagePath);

            //zebraCardImage = DrawImage(graphics, PrintType.Color, imageData, 0, 0, 300, 100);
            //graphicsData.Add(AddImage(CardSide.Back, PrintType.Color, 0, 0, -1, zebraCardImage));
         }
         return graphicsData;
      }

      /// <exception cref="Zebra.Sdk.Card.Exceptions.ZebraCardException"></exception>
      /// <exception cref="Zebra.Sdk.Device.ZebraIllegalArgumentException"></exception>
      private static ZebraCardImageI DrawImage(ZebraGraphics graphics, PrintType printType, byte[] imageData, int xOffset, int yOffset, int width, int height)
      {
         graphics.Initialize(0, 0, OrientationType.Landscape, printType, Color.White);
         graphics.DrawImage(imageData, xOffset, yOffset, width, height, RotationType.RotateNoneFlipNone);
         return graphics.CreateImage();
      }

   

      private static GraphicsInfo AddImage(CardSide side, PrintType printType, int xOffset, int yOffset, int fillColor, ZebraCardImageI zebraCardImage)
      {
         return new GraphicsInfo
         {
            Side = side,
            PrintType = printType,
            GraphicType = zebraCardImage != null ? GraphicType.BMP : GraphicType.NA,
            XOffset = xOffset,
            YOffset = yOffset,
            FillColor = fillColor,
            Opacity = 0,
            Overprint = false,
            GraphicData = zebraCardImage ?? null
         };
      }
     #endregion Graphics

   #region JobStatus
     /// <exception cref="ArgumentException"></exception>
     /// <exception cref="ConnectionException"></exception>
     /// <exception cref="IOException"></exception>
     /// <exception cref="OverflowException"></exception>
     /// <exception cref="Zebra.Sdk.Settings.SettingsException"></exception>
     /// <exception cref="Zebra.Sdk.Card.Exceptions.ZebraCardException"></exception>
     private static JobStatusInfo PollJobStatus(int jobId, ZebraCardPrinter zebraCardPrinter)
      {
         JobStatusInfo jobStatusInfo = new JobStatusInfo();
         bool isFeeding = false;

         long start = Math.Abs(Environment.TickCount);
         while (true)
         {
            jobStatusInfo = zebraCardPrinter.GetJobStatus(jobId);

            if (!isFeeding)
            {
               start = Math.Abs(Environment.TickCount);
            }

            isFeeding = jobStatusInfo.CardPosition.Contains("feeding");

            string alarmDesc = jobStatusInfo.AlarmInfo.Value > 0 ? $" ({jobStatusInfo.AlarmInfo.Description})" : "";
            string errorDesc = jobStatusInfo.ErrorInfo.Value > 0 ? $" ({jobStatusInfo.ErrorInfo.Description})" : "";

            Console.WriteLine($"Job {jobId}: status:{jobStatusInfo.PrintStatus}, position:{jobStatusInfo.CardPosition}, alarm:{jobStatusInfo.AlarmInfo.Value}{alarmDesc}, error:{jobStatusInfo.ErrorInfo.Value}{errorDesc}");

            if (jobStatusInfo.PrintStatus.Contains("done_ok"))
            {
               break;
            }
            else if (jobStatusInfo.PrintStatus.Contains("error") || jobStatusInfo.PrintStatus.Contains("cancelled"))
            {
               Console.WriteLine($"The job encountered an error [{jobStatusInfo.ErrorInfo.Description}] and was cancelled.");
               break;
            }
            else if (jobStatusInfo.ErrorInfo.Value > 0)
            {
               Console.WriteLine($"The job encountered an error [{jobStatusInfo.ErrorInfo.Description}] and was cancelled.");
               zebraCardPrinter.Cancel(jobId);
            }
            else if (jobStatusInfo.PrintStatus.Contains("in_progress") && isFeeding)
            {
               if (Math.Abs(Environment.TickCount) > start + CARD_FEED_TIMEOUT)
               {
                  Console.WriteLine("The job timed out waiting for a card and was cancelled.");
                  zebraCardPrinter.Cancel(jobId);
               }
            }

            Thread.Sleep(1000);
         }
         return jobStatusInfo;
      }
     #endregion JobStatus

   #region CleanUp
     private static void CloseQuietly(Connection connection, ZebraCardPrinter zebraCardPrinter)
      {
         try
         {
            if (zebraCardPrinter != null)
            {
               zebraCardPrinter.Destroy();
            }
         }
         catch { }

         try
         {
            if (connection != null)
            {
               connection.Close();
            }
         }
         catch { }
      }
      #endregion CleanUp


      public void   Print(string Printer)
      {
         Connection connection = null;
         ZebraCardPrinter zebraCardPrinter = null;
       
         try
         {

           
            //connection = new TcpConnection("1.2.3.4", 9100);
            connection = new UsbConnection(Printer);
            connection.Open();

            zebraCardPrinter = ZebraCardPrinterFactory.GetInstance(connection);

            List<GraphicsInfo> graphicsData = DrawGraphics (zebraCardPrinter, dataTable);
            

            // Set the card source
            zebraCardPrinter.SetJobSetting(ZebraCardJobSettingNames.CARD_SOURCE, "Feeder"); // Feeder=default

            // Set the card destination - If the destination value is not specifically set, it will be auto set to the most appropriate value
            if (zebraCardPrinter.HasLaminator())
            {
               zebraCardPrinter.SetJobSetting(ZebraCardJobSettingNames.CARD_DESTINATION, "LaminatorAny");
            }
            else
            {
               zebraCardPrinter.SetJobSetting(ZebraCardJobSettingNames.CARD_DESTINATION, "Eject");
            }

            // Send job
            //int jobId = zebraCardPrinter.Print(1, graphicsData);

            //// Poll job status
            //JobStatusInfo jobStatus = PollJobStatus(jobId, zebraCardPrinter);
            //Console.WriteLine($"Job {jobId} completed with status '{jobStatus.PrintStatus}'.");
         }
         catch (Exception e)
         {
            Console.WriteLine($"Error printing image: {e.Message}");
         }
         finally
         {
            CloseQuietly(connection, zebraCardPrinter);
         }


      }
      
       
         private void Form1_Load(object sender, EventArgs e)
      {
         try
         {
            foreach (DiscoveredUsbPrinter usbPrinter in UsbDiscoverer.GetZebraUsbPrinters(new ZebraCardPrinterFilter()))
            {
               Printer= usbPrinter.ToString();
            }
         }
         catch (Exception ex)
         { 
         
         }
         //
     
      }

      private void btnImprimir_Click(object sender, EventArgs e)
      {
                 
         if (Printer == null)
         {
            MessageBox.Show("La impresora no est activa", "Mensaje del Sistema");
         }
         else
         {
            ZebraCardPrint.DLL.DatosCarnet datosCarnet = new ZebraCardPrint.DLL.DatosCarnet();
            datosCarnet.DeleteDatosCarnet();
            datosCarnet.SPDatosCarnet(txtNumeroDeEmpleado.Text);
            dataTable = datosCarnet.LoadCarnet();
            Print(Printer);
         }
         

      }
   }
}

