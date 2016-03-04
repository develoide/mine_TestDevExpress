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
    public class clsPersona
    {
        public clsPersona()
        {
        }
        public Int32 PersonaID { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public byte[] Fotografia { get; set; }
        public bool Habilitado { get; set; }
        testDevExpress.Local_DB db = new testDevExpress.Local_DB();
        public void AddItem(testDevExpress.Persona objPersona)
        {
            var Item = new testDevExpress.Persona();

            
                Item.Nombre = objPersona.Nombre;
                Item.FechaNacimiento = objPersona.FechaNacimiento;
                Item.Fotografia = objPersona.Fotografia;
                Item.Habilitado = true;
                db.Persona.Add(Item);
                db.SaveChanges();
            
        }

        public void UpdateItem(testDevExpress.Persona objPersona)
        {
            var modelItem = db.Persona.FirstOrDefault(it => it.PersonaID == objPersona.PersonaID);

            if (modelItem != null)
            {
                modelItem.Nombre = objPersona.Nombre;
                modelItem.FechaNacimiento = objPersona.FechaNacimiento;
                modelItem.Fotografia = objPersona.Fotografia;                
                db.SaveChanges();
            }
        }

        public void DeleteItem(Int32 PersonaID)
        {
            var modelItem = db.Persona.FirstOrDefault(it => it.PersonaID == PersonaID);
            if (modelItem != null)
            {                
                modelItem.Habilitado = false;
                db.SaveChanges();
            }
        }

        public IList<clsPersona> GetItems()
        {   
            //return db.Persona.ToList();
            var items = from i in db.Persona
                        where i.Habilitado == true                        
                        select
                            new clsPersona { PersonaID = i.PersonaID, Nombre = i.Nombre, FechaNacimiento = i.FechaNacimiento, Habilitado = i.Habilitado };
            //items = items.OrderBy(x => x.Number);
            return items.ToList();
        }

      
    }
   
}