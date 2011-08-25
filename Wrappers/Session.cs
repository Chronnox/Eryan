﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;

using Eryan.Responses;
using Eryan.Factories;
using Eryan.IPC;
using Eryan.Input;
using Eryan.UI;

namespace Eryan.Wrappers
{
    /// <summary>
    /// Hold eve session information
    /// </summary>
    public class Session
    {
        Communicator com;
        KeyBoard kb;
        Random ran = new Random();
        Mouse m;
        PreciseMouse pm;

        /// <summary>
        /// Builds the session object with the given communicator
        /// </summary>
        /// <param name="com">The reference to the bot's communicator</param>
        public Session(WindowHandler wh)
        {
            this.com = wh.COMMUNICATOR;
            this.kb = wh.KEYBOARD;
            m = wh.MOUSE;
            pm = wh.PMOUSE;
        }


        /// <summary>
        /// Check if we are loading something
        /// </summary>
        /// <returns>True if there is a progress dialog open, false otherwise</returns>
        public bool isLoading()
        {
            BooleanResponse bresp = (BooleanResponse)com.sendCall(FunctionCallFactory.CALLS.ISLOADING, Response.RESPONSES.BOOLEANRESPONSE);
            if (bresp == null)
                return false;
            return (Boolean)bresp.Data;
        }


        /// <summary>
        /// Check if the system menu is open
        /// </summary>
        /// <returns>True if it is, false otherwise</returns>
        public bool isSystemMenuOpen()
        {
            BooleanResponse bresp = (BooleanResponse)com.sendCall(FunctionCallFactory.CALLS.ISSYSTEMMENUOPEN, Response.RESPONSES.BOOLEANRESPONSE);
            if (bresp == null)
                return false;
            return (Boolean)bresp.Data;
        }

        /// <summary>
        /// Get the No button of an interface if it exists
        /// </summary>
        /// <returns>The No button or null on failure</returns>

        public Button getNoButton()
        {
            InterfaceResponse iresp = (InterfaceResponse)com.sendCall(FunctionCallFactory.CALLS.GETMODALYESBUTTON, Response.RESPONSES.INTERFACERESPONSE);
            if (iresp == null)
            {
                return null;
            }

            return new Button("NO", iresp.X, iresp.Y, iresp.Height, iresp.Width);
        }

        /// <summary>
        /// Check if we are fleeted
        /// </summary>
        /// <returns>True if we are fleeted, false otherwise</returns>
        public bool amIFleeted()
        {
            BooleanResponse bresp = (BooleanResponse)com.sendCall(FunctionCallFactory.CALLS.ISFLEETED, Response.RESPONSES.BOOLEANRESPONSE);
            if (bresp == null)
                return false;

            return (Boolean)bresp.Data;
        }

        /// <summary>
        /// Logout
        /// </summary>
        public void logout()
        {
            if (!isSystemMenuOpen())
                openSystemMenu();

            Thread.Sleep(600);

            Button logoff = getLogoutButton();
            if (logoff != null)
            {   
                m.move(new Point(ran.Next(logoff.X + 5, logoff.X + logoff.Width - 5), ran.Next(logoff.Y + 5, logoff.Y + logoff.Height - 5)));
                Thread.Sleep(500);
                m.click(true);
                pm.synchronize(m);
                Thread.Sleep(600);
                Button yes = getYesButton();
                if (yes != null)
                {
                    m.move(new Point(ran.Next(yes.X + 5, yes.X + yes.Width - 5), ran.Next(yes.Y + 5, yes.Y + yes.Height - 5)));
                    Thread.Sleep(600);
                    m.click(true);
                    pm.synchronize(m);
                }
            }



        }

        /// <summary>
        /// Open the system menu
        /// </summary>
        public void openSystemMenu()
        {
           kb.sendChar((char)KeyBoard.VKeys.VK_ESCAPE);
        }

        /// <summary>
        /// Get the logoff button from the system menu
        /// </summary>
        /// <returns>The logout button or No on failure</returns>
        public Button getLogoutButton()
        {
            InterfaceResponse iresp = (InterfaceResponse)com.sendCall(FunctionCallFactory.CALLS.GETLOGOFFBUTTON, Response.RESPONSES.INTERFACERESPONSE);
            if (iresp == null)
            {
                return null;
            }

            return new Button("LOGOFF", iresp.X, iresp.Y, iresp.Height, iresp.Width);
        }


        /// <summary>
        /// Get the Yes button of an interface if it exists
        /// </summary>
        /// <returns>The Yes Button, or null on failure</returns>
        public Button getYesButton()
        {
            InterfaceResponse iresp = (InterfaceResponse)com.sendCall(FunctionCallFactory.CALLS.GETMODALYESBUTTON, Response.RESPONSES.INTERFACERESPONSE);
            if (iresp == null)
            {
                return null;
            }

            return new Button("YES", iresp.X, iresp.Y, iresp.Height, iresp.Width);
        }

        /// <summary>
        /// Get the OK button of an interface if it exists
        /// </summary>
        /// <returns>The ok button of the interface, null if it doesn't exist</returns>
        public Button getOkButton()
        {
            InterfaceResponse iresp = (InterfaceResponse)com.sendCall(FunctionCallFactory.CALLS.GETMODALOKBUTTON, Response.RESPONSES.INTERFACERESPONSE);
            if (iresp == null)
            {
                return null;
            }

            return new Button("OK", iresp.X, iresp.Y, iresp.Height, iresp.Width);
        }

        /// <summary>
        /// Get the cancel button of an interface if it exists
        /// </summary>
        /// <returns>The cancel button of the interface, null if it doesn't exist</returns>
        public Button getCancelButton()
        {
            InterfaceResponse iresp = (InterfaceResponse)com.sendCall(FunctionCallFactory.CALLS.GETMODALCANCELBUTTON, Response.RESPONSES.INTERFACERESPONSE);
            if (iresp == null)
            {
                return null;
            }

            return new Button("CANCEL", iresp.X, iresp.Y, iresp.Height, iresp.Width);
        }

        /// <summary>
        /// Get the last server message
        /// </summary>
        /// <returns>The message or null if none exists</returns>
        public string getServerMessage()
        {

            StringResponse sresp = (StringResponse)com.sendCall(FunctionCallFactory.CALLS.GETSERVERMESSAGE, Response.RESPONSES.STRINGRESPONSE);
            if (sresp == null)
            {
                return null;
            }


            Regex reg = new Regex("<center>");
            string[] split = reg.Split((string)sresp.Data);
            if (split.Count() > 1)
            {
                return split[1];
            }
            return null;

        }

        /// <summary>
        /// Check if current system is undergoing an incursion
        /// </summary>
        /// <returns>Returns true if there is an incursion, false otherwise</returns>
        public Boolean isIncursionOngoing()
        {
            BooleanResponse bresp = (BooleanResponse)com.sendCall(FunctionCallFactory.CALLS.ISINCURSIONONGOING, Response.RESPONSES.BOOLEANRESPONSE);
            if (bresp == null)
            {
                return false;
            }

            return (Boolean)bresp.Data;
        }

        /// <summary>
        /// Check if there's hostiles in local
        /// </summary>
        /// <returns>Returns true if there is hostiles in local, false otherwise</returns>
        public Boolean isLocalHostile()
        {
            BooleanResponse tresp = (BooleanResponse)com.sendCall(FunctionCallFactory.CALLS.CHECKLOCAL, Response.RESPONSES.BOOLEANRESPONSE);
            if (tresp == null)
            {
                Console.WriteLine("Couldn't retrieve local");
                return true;
            }
            return ((Boolean)tresp.Data);
        }

        /// <summary>
        /// Get the current solar system
        /// </summary>
        /// <returns>Returns a solarsystem object on success, null on failure</returns>
        public SolarSystem getSolarSystem()
        {
            SystemResponse sresp = (SystemResponse)com.sendCall(FunctionCallFactory.CALLS.GETSYSTEMINFORMATION, Response.RESPONSES.SOLARYSYSTEMRESPONSE);
            if (sresp == null)
            {
                return null;
            }

            return new SolarSystem(sresp.Name, sresp.Info);
           
        }

    }
}
