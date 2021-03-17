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
using Zebra.Sdk.Card.Graphics.Barcode;

namespace ZebraCardPrint
{
   public partial class FrmCardPrint : Form
   {
      private const int CARD_FEED_TIMEOUT = 30000;
      private string Printer = null;
      DataTable dataTable = new  DataTable();
      int xposition = 10;
      


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

      private ZebraCardImageI DrawCarnetBack(ZebraGraphics graphics, PrintType printType, DataTable data)
      {
         int positionx = 775;
         int positiony = 380;

         Font font = new Font("Arial", 7, FontStyle.Bold);
         Font fontIGSS = new Font("Arial", 9, FontStyle.Bold);
         Font fontFechaEmision = new Font("Arial", 5, FontStyle.Regular);
         string Recomendaciones = "La  empresa no se hace responsable por el mal uso de este carné, " +
         "el cual es válido únicamente dentro de nuestras instalaciones y durante el periodo de relación laboral."+
         " En caso  de emergencia  comunicarse con Recursos Humanos   al 7820-1700.";
         string NumeroDeEmpleado = data.Rows[0]["NumeroDeEmpleado"].ToString();
         string ImpresionCarnet = data.Rows[0]["ImpresionCarnet"].ToString();
         string AfiliacionIGSS = data.Rows[0]["AfiliacionIGSS"].ToString();

         graphics.Initialize(0, 0, OrientationType.Landscape, printType, Color.White);

         graphics.DrawText(Recomendaciones, font, Color.Black, 10, 10, 1000, 200, 0, TextAlignment.Left, TextAlignment.Center);
         

         using (Code39Util code39 = ZebraBarcodeFactory.GetCode39(graphics))
         {
            code39.ValueToEncode = NumeroDeEmpleado + "-" + ImpresionCarnet;
            code39.QuietZoneWidth = 0;
            code39.DrawBarcode(100, 190, 800, 180);
         }

         if (File.Exists(@"\\nastrfca1\ArchivosAdjuntosSistemas\Carnet\" + NumeroDeEmpleado + "VCARDV3.png"))
         {
            string QRVCardImagePath = @"\\nastrfca1\ArchivosAdjuntosSistemas\Carnet\" + NumeroDeEmpleado + "VCARDV3.png";
            byte[] QRVCardimageData = File.ReadAllBytes(QRVCardImagePath);
            graphics.DrawImage(QRVCardimageData, 10, 380, 270, 270, RotationType.RotateNoneFlipNone);
         }
         else
         {
             positionx = 10;
             positiony = 380;
         }


         graphics.DrawText("Afiliación IGSS: ", font, Color.Black, 280, 405, 600, 45, 0, TextAlignment.Left, TextAlignment.Center);
         if ( !string.IsNullOrEmpty( AfiliacionIGSS))
         { 
            graphics.DrawText(AfiliacionIGSS, fontIGSS, Color.Black, 280, 435, 600, 50, 0, TextAlignment.Left, TextAlignment.Center);
         }

         graphics.DrawText("Teléfono Clínica Médica:",font, Color.Black, 280, 480, 600, 45, 0, TextAlignment.Left, TextAlignment.Center);
         graphics.DrawText("7820 1746", fontIGSS, Color.Black, 280, 510, 600, 45, 0, TextAlignment.Left, TextAlignment.Center);
         graphics.DrawText("Fecha de Emisión:"   , fontFechaEmision, Color.Black, 280, 540, 600, 45, 0, TextAlignment.Left, TextAlignment.Center);
         graphics.DrawText(DateTime.Now.ToString("dd/MM/yyyy"), fontFechaEmision, Color.Black, 280, 570, 600, 45, 0, TextAlignment.Left, TextAlignment.Center);



         using (QRCodeUtil qrCode = ZebraBarcodeFactory.GetQRCode(graphics))
         {
            qrCode.ValueToEncode = NumeroDeEmpleado;
            qrCode.DrawBarcode(positionx, positiony, 180, 180);
         }

         return graphics.CreateImage();

      }
         private  ZebraCardImageI DrawCarnetFront(ZebraGraphics graphics, PrintType printType, DataTable data)
      {
         Font font = new Font("Arial", 9, FontStyle.Bold );
         Font fontlabel = new Font("Arial", 6, FontStyle.Bold);
         Font fontlabelNumeroEmpleado = new Font("Arial", 6, FontStyle.Bold);
         char[] spacewhite = new char[] { ' ' };
         string NombreDepartamentoEmpresa = null;
         string NombreDepartamentoEmpresa2 = null;
         string NombrePuestoEmpresa = null;
         string NombrePuestoEmpresa2 = null;
         int puestosplit = 1;
         int departamentosplit = 1;
         graphics.Initialize(0, 0, OrientationType.Landscape, printType, Color.White);

         string colorImagePath = @"C:\logos\" + data.Rows[0]["Empresa"].ToString() + ".jpg";
         byte[] imageData = File.ReadAllBytes(colorImagePath);
         string RutaFoto =   data.Rows[0]["RutaFoto"].ToString().Replace("http://intsdfca1:83/Adjuntos/Carnet/", @"\\nastrfca1\ArchivosAdjuntosSistemas\Carnet\");
         byte[] imagePhoto = File.ReadAllBytes(RutaFoto);

         string Nombre = data.Rows[0]["Nombres"].ToString();
         string Apellidos = data.Rows[0]["Apellidos"].ToString();
         string Departamento = data.Rows[0]["NombreDepartamento"].ToString();
         string Puesto = data.Rows[0]["NombrePuesto"].ToString();
         string NumeroDeEmpleado = data.Rows[0]["NumeroDeEmpleado"].ToString();
         string DPI = data.Rows[0]["DPI"].ToString();


         graphics.DrawImage(imageData,10, 10, 450, 150, RotationType.RotateNoneFlipNone);
         graphics.DrawImage(imagePhoto, 560, 0, 445, 460, RotationType.RotateNoneFlipNone);

         graphics.DrawText("Puesto:", fontlabel, Color.FromArgb(2, 115, 199), xposition, 160, 0);

         if (Puesto.Length >= 35)
         {
           if( Puesto.Split(spacewhite).Length >=2)
            {
               puestosplit = 2;
            }
         }

         if (Puesto.Split(spacewhite).Length >= 4)
         {
            puestosplit = 2;
         }

         if (Puesto.Split(spacewhite).Length>=5)
         {
            puestosplit = 3;
         }

         if (Puesto.Split(spacewhite).Length >= 7)
         {
            puestosplit = 4;
         }

         if (Puesto.Length >= 30)
         {
            for (int i = 0; i < Puesto.Split(spacewhite).Length; i++)
            {
               if (i < Puesto.Split(spacewhite).Length - puestosplit)
               {
                  NombrePuestoEmpresa += Puesto.Split(spacewhite)[i].ToString() + " ";

               }
               else
               {
                  NombrePuestoEmpresa2 += Puesto.Split(spacewhite)[i].ToString() + " ";

               }

            }
            if (!string.IsNullOrEmpty(NombrePuestoEmpresa))
            {
               graphics.DrawText(NombrePuestoEmpresa, font, Color.Black, xposition, 190, 540, 45, 0, TextAlignment.Left, TextAlignment.Top, true);
               graphics.DrawText(NombrePuestoEmpresa2, font, Color.Black, xposition, 225, 540, 45, 0, TextAlignment.Left, TextAlignment.Top, true);
            }
            else
            {
               graphics.DrawText(NombrePuestoEmpresa2, font, Color.Black, xposition, 190, 540, 45, 0, TextAlignment.Left, TextAlignment.Top, true);
            }

         }
      
         else
         {
            graphics.DrawText(Puesto, font, Color.Black, xposition, 190, 540, 50, 0, TextAlignment.Left, TextAlignment.Top, true);
         }





   graphics.DrawText("Departamento:", fontlabel, Color.FromArgb(2, 115, 199), xposition, 290, 0);

         if (Departamento.Length >= 35)
         {
            if (Departamento.Split(spacewhite).Length >= 2)
            {
               departamentosplit = 2;
            }
         }

         if (Departamento.Split(spacewhite).Length >= 2)
         {
            departamentosplit = 3;
         }

         if (Departamento.Split(spacewhite).Length>=5)
         {
            departamentosplit = 4;
         }

         if (Departamento.Split(spacewhite).Length >= 7)
         {
            departamentosplit = 5;
         }

         if (Departamento.Length >= 30)
         {
            for (int i = 0; i < Departamento.Split(spacewhite).Length; i++)
            {
               if (i < Departamento.Split(spacewhite).Length - departamentosplit)
               {
                  NombreDepartamentoEmpresa += Departamento.Split(spacewhite)[i].ToString() + " ";
                  
               }
               else
               {

                  NombreDepartamentoEmpresa2 += Departamento.Split(spacewhite)[i].ToString() + " ";
                  
               }

            }
            if (!string.IsNullOrEmpty(NombreDepartamentoEmpresa))
            {
               graphics.DrawText(NombreDepartamentoEmpresa, font, Color.Black, xposition, 320, 540, 45, 0, TextAlignment.Left, TextAlignment.Top, true);
               graphics.DrawText(NombreDepartamentoEmpresa2, font, Color.Black, xposition, 355, 540, 45, 0, TextAlignment.Left, TextAlignment.Bottom, true);
            }
            else
            
            {
               graphics.DrawText(NombreDepartamentoEmpresa2, font, Color.Black, xposition, 320, 540, 45, 0, TextAlignment.Left, TextAlignment.Top, true);
            }
         }
         else
         {
            graphics.DrawText(Departamento, font, Color.Black, xposition, 320, 540, 50, 0, TextAlignment.Left, TextAlignment.Top,true);
         }
         
         //}




         //graphics.DrawText("DPI:", fontlabel, Color.FromArgb(2, 115, 199), 40, 355, 0);
         //graphics.DrawText(DPI, font, Color.Black, 40, 380, 0);



         //graphics.DrawText(Apellidos, font, Color.Black, 40, 220, 0);








         graphics.DrawLine(new PointF(0,540), new PointF(1100,540), 150, Color.FromArgb(2, 115, 199));

         graphics.DrawText(Nombre + " " + Apellidos, font, Color.White,0, 440, 1000,100,0, TextAlignment.Center, TextAlignment.Center);
         graphics.DrawText("-DPI-", fontlabelNumeroEmpleado, Color.White, 0, 480, 1000, 100, 0, TextAlignment.Center, TextAlignment.Center);
         graphics.DrawText(DPI, font, Color.White, 0, 515, 1000, 100, 0, TextAlignment.Center, TextAlignment.Center);

         



         return graphics.CreateImage();
      }

      public static Bitmap ByteToImage(byte[] blob)
      {
         MemoryStream mStream = new MemoryStream();
         byte[] pData = blob;
         mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
         Bitmap bm = new Bitmap(mStream, false);
         mStream.Dispose();
         return bm;
      }

      private   List<GraphicsInfo> DrawGraphics(ZebraCardPrinter zebraCardPrinter, DataTable data)
      {
         // Generate image data
         ZebraCardImageI zebraCardImage = null;
         
         //byte[] fontFields = System.Text.Encoding.ASCII.GetBytes("Arial");
         
         List<GraphicsInfo> graphicsData = new List<GraphicsInfo>();
         using (ZebraGraphics graphics = new ZebraCardGraphics(zebraCardPrinter))
         {

            if (rdoAmbasCaras.Checked) 
            { 
               // Front side color     
               zebraCardImage = DrawCarnetFront(graphics, PrintType.Color, data);
               pictureBox1.Image = ByteToImage(zebraCardImage.ImageData);

               //zebraCardImage = DrawImage(graphics, PrintType.Color, imageData, 0, 0, 450, 150);            
               //graphics.DrawImage(imageData, 0, 450, 150, 0, RotationType.RotateNoneFlipXY);           
               //zebraCardImage = graphics.CreateImage();
               graphicsData.Add(AddImage(CardSide.Front, PrintType.Color, 0, 0, -1, zebraCardImage));
               graphics.Clear();
               //// Front side full overlay
               graphicsData.Add(AddImage(CardSide.Front, PrintType.Overlay, 0, 0, 1, null));

               // Back side mono
               zebraCardImage = null;
               zebraCardImage = DrawCarnetBack(graphics, PrintType.MonoK, data);
               pictureBox2.Image = ByteToImage(zebraCardImage.ImageData);
               //zebraCardImage = DrawImage(graphics, PrintType.Color, imageData, 0, 0, 450, 150);            
               //graphics.DrawImage(imageData, 0, 450, 150, 0, RotationType.RotateNoneFlipXY);           
               //zebraCardImage = graphics.CreateImage();
               graphicsData.Add(AddImage(CardSide.Back, PrintType.MonoK, 0, 0, -1, zebraCardImage));
               graphics.Clear();
               //// Back side full overlay
               graphicsData.Add(AddImage(CardSide.Back, PrintType.Overlay, 0, 0, 1, null));
            }

            if (rdoFrontal.Checked)
            {
               // Front side color     
               zebraCardImage = DrawCarnetFront(graphics, PrintType.Color, data);
               pictureBox1.Image = ByteToImage(zebraCardImage.ImageData);

               //zebraCardImage = DrawImage(graphics, PrintType.Color, imageData, 0, 0, 450, 150);            
               //graphics.DrawImage(imageData, 0, 450, 150, 0, RotationType.RotateNoneFlipXY);           
               //zebraCardImage = graphics.CreateImage();
               graphicsData.Add(AddImage(CardSide.Front, PrintType.Color, 0, 0, -1, zebraCardImage));
               graphics.Clear();
               //// Front side full overlay
               graphicsData.Add(AddImage(CardSide.Front, PrintType.Overlay, 0, 0, 1, null));
            }

            if (rdoReverso.Checked)
            {
               // Back side mono
               zebraCardImage = null;
               zebraCardImage = DrawCarnetBack(graphics, PrintType.MonoK, data);
               pictureBox2.Image = ByteToImage(zebraCardImage.ImageData);
               //zebraCardImage = DrawImage(graphics, PrintType.Color, imageData, 0, 0, 450, 150);            
               //graphics.DrawImage(imageData, 0, 450, 150, 0, RotationType.RotateNoneFlipXY);           
               //zebraCardImage = graphics.CreateImage();
               graphicsData.Add(AddImage(CardSide.Back, PrintType.MonoK, 0, 0, -1, zebraCardImage));
               graphics.Clear();
               //// Back side full overlay
               graphicsData.Add(AddImage(CardSide.Back, PrintType.Overlay, 0, 0, 1, null));

            }


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
         ZebraCardPrint.DLL.DatosCarnet datosCarnet = new ZebraCardPrint.DLL.DatosCarnet();

         try
         {
            
           
            //connection = new TcpConnection("1.2.3.4", 9100);
            connection = new UsbConnection(Printer);
            
            connection.Open();

            zebraCardPrinter = ZebraCardPrinterFactory.GetInstance(connection);

            List<GraphicsInfo> graphicsData = DrawGraphics (zebraCardPrinter, dataTable);


            // Set the card source

            //Descomentar
            zebraCardPrinter.SetJobSetting(ZebraCardJobSettingNames.CARD_SOURCE, "Feeder"); // Feeder=default

            // Set the card destination - If the destination value is not specifically set, it will be auto set to the most appropriate value

            if (checkBox1.CheckState==CheckState.Unchecked)
            {
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
               int jobId = zebraCardPrinter.Print(1, graphicsData);

               // Poll job status
               JobStatusInfo jobStatus = PollJobStatus(jobId, zebraCardPrinter);
               MessageBox.Show($"Impresion Id: {jobId} completada con estado: '{jobStatus.PrintStatus}'.");
               if (jobStatus.PrintStatus.ToString().ToUpper()== "DONE_OK")
               {
                  datosCarnet.SDInsertaImpresionCarnet(txtNumeroDeEmpleado.Text);
                  rdoAmbasCaras.Checked = true;
                  
               }
               

            }
         }
         catch (Exception e)
         {
            MessageBox.Show  ($"Error printing image: {e.Message}");
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
               Printer = usbPrinter.ToString();
            }

            if (Environment.UserName.ToString().ToUpper() == "DMARROQUIN")
            {
               Printer = @"\\?\usb#vid_0a5f&pid_0148#412509098#{28d78fad-5a12-11d1-ae5b-0000f803a8c2}";
            }

         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
         //
     
      }



      private void btnImprimir_Click(object sender, EventArgs e)
      {
         if (String.IsNullOrEmpty(txtNumeroDeEmpleado.Text))
         {
            MessageBox.Show("El número de empleado es requerido para este proceso.", "Mensaje del Sistema");
            ActiveControl = txtNumeroDeEmpleado;
            return;
         }
         pictureBox1.Image = null;
         pictureBox2.Image = null;
                 
         if (Printer == null)
         {
            MessageBox.Show("La impresora no est activa", "Mensaje del Sistema");
         }
         else
         {
            
            ZebraCardPrint.DLL.DatosCarnet datosCarnet = new ZebraCardPrint.DLL.DatosCarnet();
            if (datosCarnet.EmpleadoActivo(txtNumeroDeEmpleado.Text) > 0)
            {
               checkBox1.Checked = false;
              
               datosCarnet.DeleteDatosCarnet();
               datosCarnet.SPDatosCarnet(txtNumeroDeEmpleado.Text);
               dataTable = datosCarnet.LoadCarnet();
               //datosCarnet.SDInsertaImpresionCarnet(txtNumeroDeEmpleado.Text);
               Print(Printer);
              
               
            }
            else
            {
               MessageBox.Show("El número de empleado ingresado no pertenece a ningún colaborador activo.", "Mensaje del Sistema");
            }

            }
           
         }

      private void button1_Click(object sender, EventArgs e)
      {

         

         if (String.IsNullOrEmpty(txtNumeroDeEmpleado.Text))
         {
            MessageBox.Show("El número de empleado es requerido para este proceso.", "Mensaje del Sistema");
            ActiveControl = txtNumeroDeEmpleado;
            return;
         }
         pictureBox1.Image = null;
         pictureBox2.Image = null;

         if (Printer == null)
         {
            MessageBox.Show("La impresora no est activa", "Mensaje del Sistema");
         }
         else
         {

            ZebraCardPrint.DLL.DatosCarnet datosCarnet = new ZebraCardPrint.DLL.DatosCarnet();
            if (datosCarnet.EmpleadoActivo(txtNumeroDeEmpleado.Text) > 0)
            {
               checkBox1.Checked = true;
               datosCarnet.DeleteDatosCarnet();
               datosCarnet.SPDatosCarnet(txtNumeroDeEmpleado.Text);
               dataTable = datosCarnet.LoadCarnet();
               //datosCarnet.SDInsertaImpresionCarnet(txtNumeroDeEmpleado.Text);
               Print(Printer);

            }
            else
            {
               MessageBox.Show("El número de empleado ingresado no pertenece a ningún colaborador activo.", "Mensaje del Sistema");
            }

         }

      }
   }
   }

     
   


