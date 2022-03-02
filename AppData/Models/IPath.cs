using System;
using System.Collections.Generic;
using System.Text;

namespace Real2App.AppData.Models
{
    public interface IPath
    {
        string GetDatabasePath(string filename);
    }
}
