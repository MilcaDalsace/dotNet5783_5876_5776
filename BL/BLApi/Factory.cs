﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public class Factory
    {
        public static IBl Get()
        {
            return new BlImplementation.Bl();
        }
    }
}
