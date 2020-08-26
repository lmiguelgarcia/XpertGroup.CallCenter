using System;
using System.Collections.Generic;
using System.Text;

namespace XpertGroup.Dominio.Excepcion
{
    public class CallCenterExcepcion: Exception
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }

        public CallCenterExcepcion(string errorCode, string errorDescription) : base(errorDescription)
        {
            this.ErrorCode = errorCode;
            this.ErrorDescription = errorDescription;
        }

        public CallCenterExcepcion(string message)
            : base(message)
        {
        }

        public CallCenterExcepcion(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
