using SQLite;

namespace ESPmote.Model
{
    public class FernbedienungButton
    {
        public FernbedienungButton()
        {
            Beschriftung = "";
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int Fernbedienung { get; set; }

        public string Beschriftung { get; set; }

        public int Zeile { get; set; }

        public int Spalte { get; set; }

        public string IRCode { get; set; }
    }
}
