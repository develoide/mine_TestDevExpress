using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace testDevExpress.Models
{
    public class clsTelefono
    {
        public clsTelefono()
        {
        }
        

        public Int32 TelefonoID { get; set; }
        public Int32 PersonaID { get; set; }
        public string Numero { get; set; }


        public static IEnumerable GetTelefonosPorPersona(int PersonaID)
        {

            testDevExpress.Local_DB db = new testDevExpress.Local_DB();
            var query = from telefono in db.Telefono
                        where telefono.PersonaID==PersonaID
                        select
                            new clsTelefono { PersonaID = telefono.PersonaID, TelefonoID = telefono.TelefonoID, Numero = telefono.Numero };
           
                      
            return query.ToList();
        }


        public static void AddItem(clsTelefono objTelefono)
        {
            testDevExpress.Local_DB db = new testDevExpress.Local_DB();
            var Item = new testDevExpress.Telefono();
            Item.Numero = objTelefono.Numero;
            Item.PersonaID = objTelefono.PersonaID;
            db.Telefono.Add(Item);
            db.SaveChanges();
        }

        public static void UpdateItem(clsTelefono objTelefono)
        {
            testDevExpress.Local_DB db = new testDevExpress.Local_DB();
            var Item = db.Telefono.FirstOrDefault(it => it.TelefonoID == objTelefono.TelefonoID);

            if (Item != null)
            {
                Item.Numero = objTelefono.Numero;               
                db.SaveChanges();
            }
        }

        public static void DeleteItem(int TelefonoId)
        {
            testDevExpress.Local_DB db = new testDevExpress.Local_DB();
            var Item = db.Telefono.FirstOrDefault(it => it.TelefonoID == TelefonoId);

            if (Item != null)
            {
                db.Telefono.Attach(Item);
                db.Telefono.Remove(Item);
                db.SaveChanges();
            }
        }
    }
     
}