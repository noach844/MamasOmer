﻿using System;
using System.Configuration;
using MamasOmer.Classes;
using MamasOmer.Classes.DB;
using MamasOmer.Classes.Exceptions;

namespace MamasOmer
{
    class Program
    {        
        static void Main(string[] args)
        {
            EmployeesManager manager = new EmployeesManager();
            manager.SearchEmployees("Ing");
        }
    }
}
