﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_custom_project
{
    public interface IHaveInventory
    {
        GameObject Locate(string id);
        string Name { get; }
    }
}
