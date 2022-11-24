using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.WkTechnology.Tecnico.Domain.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
    }

    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
    }
}

