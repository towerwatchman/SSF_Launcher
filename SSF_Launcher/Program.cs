using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SSF_Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            //var _prgm = Properties.Settings.Default;
            string ssf = Properties.Settings.Default.SSF_LOCATION;
            string vcd = Properties.Settings.Default.VCD_LOCATION;
            string rom = Properties.Settings.Default.ROM_LOCATION;
            int ssf_wait = Properties.Settings.Default.SSF_WAIT_TIME;
            int cmd_wait = Properties.Settings.Default.CMD_WAIT_TIME;
            string mount_cmd = Properties.Settings.Default.MOUNT;
            string unmount_cmd = Properties.Settings.Default.UNMOUNT;
            bool enable_wait = Properties.Settings.Default.ENABLE_WAIT;

            if(args.Length > 0)
            {
               // Console.Out.WriteLine(args[0]);
            }
            try
            {
                Console.Out.WriteLine("Unmounting Image");
                Process.Start(vcd, unmount_cmd);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex);
            }  

            // Wait for any images to unmount. This will make sure that only one image is mounted. The timer can be increased for slower PC's
            System.Threading.Thread.Sleep(cmd_wait);

            try
            {
                Console.Out.WriteLine("Mounting Image: " + args[0]);
                Process.Start(vcd, mount_cmd + " \"" + args[0] + "\""); //Mount Image to first drive

                System.Threading.Thread.Sleep(ssf_wait); // Wait for image to mount before launching SSF
                Process.Start(ssf); //Launch SSF
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine(ex);
            }

            if(enable_wait == true)
            {                
                Console.Read();//Without this Emulationstation will launch SSF but switch bak to SSF
            }
        }
    }
}
