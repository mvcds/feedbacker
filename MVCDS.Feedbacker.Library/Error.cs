﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDS.Feedbacker.Library
{
    public class Error : IResult
    {
        internal Error(string message)
        {
            MessageValidator.Assert(message);

            Information = new Exception(message.Trim());
            Date = DateTime.Now;
        }

        internal Error(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException("Exception cannot be null");

            MessageValidator.Assert(exception.Message);

            Information = exception;
            Date = DateTime.Now;
        }

        public DateTime Date { get; private set; }
        
        public string Message
        {
            get
            {
                return Read(Information, 0);
            }
        }

        private string Read(Exception information, int n)
        {
            if (information.InnerException == null)
                return information.Message;

            return information.Message 
                + Environment.NewLine 
                + new string('-', n + 1) 
                + Read(information.InnerException, n + 1);
        }

        public bool TriggersFailure
        {
            get
            {
                return true;
            }
        }

        public Exception Information { get; private set; }
    }
}