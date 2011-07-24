﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

using Eryan.Wrappers;
using Eryan.Input;
using Eryan.IPC;
using Eryan.Responses;
using Eryan.Factories;

namespace Eryan.InputHandler
{
    /// <summary>
    /// Handles the interaction with the overview
    /// </summary>
    public class OverviewHandler : InputHandler
    {
        List<OverViewEntry> entries;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="m">Reference to the bot's mouse</param>
        /// <param name="pm">Reference to the bot's Precise mouse</param>
        /// <param name="com">Reference to the bot's communicator</param>
        public OverviewHandler(Mouse m, PreciseMouse pm, Communicator com)
        {
            this.m = m;
            this.pm = pm;
            this.comm = com;
        }

        /// <summary>
        /// Populate this overview wrapper with the client's current overview
        /// </summary>
        /// <returns>True if success, false otherwise</returns>
        public bool readOverView()
        {
            OverViewResponse resp = (OverViewResponse)comm.sendCall(FunctionCallFactory.CALLS.GETOVERVIEWITEMS, Response.RESPONSES.OVERVIEWRESPONSE);
            if (resp == null)
            {
                return false;
            }

            entries = (List<OverViewEntry>)resp.Data;
            return true;
        }

        /// <summary>
        /// Open a menu on this row
        /// </summary>
        /// <param name="rowNum">The number of the overview entry</param>
        /// <returns>True if success, false otherwise</returns>
        public bool interactRow(int rowNum)
        {
            if (entries.Count > rowNum)
            {
                m.moveMouse(new Point(entries[rowNum].X, entries[rowNum].Y));
                Thread.Sleep(200);
                synchronizeMouse(m);
                m.click(false);
                Thread.Sleep(200);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Read an overview row
        /// </summary>
        /// <param name="rowNum">The row number in the overview</param>
        /// <returns>The contents of the overview row</returns>
        public string readRow(int rowNum)
        {
            if (entries.Count < rowNum)
                return entries[rowNum].ToString();
            return null;
        }

        /// <summary>
        /// Interact with the first overview row that contains "content"
        /// </summary>
        /// <param name="content">The string to look for in the overview</param>
        /// <returns>True if interacted, false otherwise</returns>
        public bool interactRow(string content)
        {
            int i = 0;
            foreach (OverViewEntry entry in entries)
            {
                if (entry.ToString().Contains(content))
                {
                    interactRow(i);
                    return true;
                }
                i++;
            }
            return false;
        }

        /// <summary>
        /// Returns the overview items
        /// </summary>
        public List<OverViewEntry> Items
        {
            get
            {
                return entries;
            }
        }

    }
}
