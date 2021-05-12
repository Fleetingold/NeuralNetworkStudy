using System;
using System.Collections.Generic;
using System.Text;

namespace AutoEncoder
{
    public interface IWeightInitializer
    {
        double InitializeWeight();
        double InitializeBias();
    }
}
