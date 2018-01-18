using SQLite;
using System.Collections.Generic;

namespace ESPmote.Model
{
    public class Fernbedienung
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
