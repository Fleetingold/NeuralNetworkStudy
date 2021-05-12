using System;
using System.Collections.Generic;
using System.Text;

namespace AutoEncoder
{
    public interface IErrorObserver
    {
        void CalculateError(double PError);
    }
}
