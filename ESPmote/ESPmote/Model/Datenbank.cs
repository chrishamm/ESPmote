using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ESPmote.Model
{
    public class Datenbank
    {
        private SQLiteConnection database;

        public Datenbank(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            database.CreateTable<Fernbedienung>();
            database.CreateTable<FernbedienungButton>();
        }

        public IEnumerable<Fernbedienung> GetFernbedienungen()
        {
            return database.Table<Fernbedienung>();
        }

        public void SaveFernbedienung(Fernbedienung f)
        {
            if (f.ID != 0)
            {
                database.Update(f);
            }
            else
            {
                database.Insert(f);
            }
        }

        public void DeleteFernbedienung(Fernbedienung f)
        {
            var buttons = GetFernbedienungButtons(f);
            foreach(FernbedienungButton btn in buttons)
            {
                DeleteFernbedienungsButton(btn);
            }
            database.Delete(f);
        }

        public IEnumerable<FernbedienungButton> GetFernbedienungButtons(Fernbedienung f)
        {
            return from fb in database.Table<FernbedienungButton>()
                   where fb.Fernbedienung == f.ID
                   select fb;
        }

        public void SaveFernbedienungsButton(FernbedienungButton fb)
        {
            if (fb.ID != 0)
            {
                database.Update(fb);
            }
            else
            {
                database.Insert(fb);
            }
        }

        public void DeleteFernbedienungsButton(FernbedienungButton fb)
        {
            database.Delete(fb);
        }

        public FernbedienungButton GetFernbedienungButton(Fernbedienung fernbedienung, int zeile, int spalte)
        {
            return (from fb in database.Table<FernbedienungButton>()
                    where (fb.Fernbedienung == fernbedienung.ID && fb.Zeile == zeile && fb.Spalte == spalte)
                    select fb).FirstOrDefault();
        }
    }
}
