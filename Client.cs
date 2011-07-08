﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Eryan
{
    static class Eryan
    {

        static ClientWindow cWindow;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 



        public static void createWindow()
        {
            cWindow = new ClientWindow();
            Application.Run(cWindow);
        }

        public static void SpawnForm(object firm)
        {
            Application.Run(firm as Form);
        }

        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            
            Thread ClientThread = new Thread(new ThreadStart(createWindow));
            ClientThread.Start();


            

            //Thread ClientThread = new Thread(new ParameterizedThreadStart(SpawnForm));
            //ClientThread.Start(cWindow);

            Point location = new Point();

            while (cWindow == null)
            {
                Thread.Sleep(300);
            }

            cWindow.createBot();

            
            while(cWindow.getBots().Count < 1)
                Thread.Sleep(100);

            Bot bot1 = cWindow.getBots()[0];
            
            
            //WindowHandler BotHandle = bot1.getHandle();


            
            while (true)
            {
                
                if ( bot1 != null)
                {
                    bot1.update();
                }
            


                System.Threading.Thread.Sleep(1000);
            }
            

        }

    }
}
