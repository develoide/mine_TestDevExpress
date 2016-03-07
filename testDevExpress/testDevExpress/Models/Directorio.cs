using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace testDevExpress.Models
{
    public class Directorio
    {
        public static IList<EditablePersona> Personas
        {
            get
            {

                HttpContext.Current.Session["Personas"] = GetAllPersonas();
                return (IList<EditablePersona>)HttpContext.Current.Session["Personas"];
            }
        }
        public static IList<EditableTelefono> Telefonos
        {
            get
            {
                HttpContext.Current.Session["Telefonos"] = GetAllTelefonos();
                return (IList<EditableTelefono>)HttpContext.Current.Session["Telefonos"];
            }
        }
        public static IEnumerable GetTelefonosByPersona(int PersonaID)
        {
            return from person in Telefonos where person.PersonaID == PersonaID select person;
        }
        public static EditableTelefono GetTelefonoByID(int id)
        {
            return (from person in Telefonos where person.TelefonoID == id select person).SingleOrDefault<EditableTelefono>();
        }
        public static EditablePersona GetPersonaByID(int id)
        {
            return (from room in Personas where room.PersonaID == id select room).SingleOrDefault<EditablePersona>();
        }


        public static void InsertTelefono(EditableTelefono objTelefono)
        {
            if (objTelefono != null)
            {
                using (Local_DB db = new Local_DB())
                {
                    var Item = new Telefono();
                    Item.Numero = objTelefono.Numero;
                    Item.PersonaID = objTelefono.PersonaID;
                    db.Telefono.Add(Item);
                    db.SaveChanges();
                }
            }
        }
        public static void UpdateTelefono(EditableTelefono objTelefono)
        {
            using (Local_DB db = new Local_DB())
            {
                var Item = db.Telefono.FirstOrDefault(it => it.TelefonoID == objTelefono.TelefonoID);
                if (Item != null)
                {
                    Item.Numero = objTelefono.Numero;
                    db.SaveChanges();
                }
            }
        }
        public static void RemoveTelefonoByID(int id)
        {
            using (Local_DB db = new Local_DB())
            {
                var Item = db.Telefono.FirstOrDefault(it => it.TelefonoID == id);

                if (Item != null)
                {

                    db.Telefono.Remove(Item);
                    db.SaveChanges();
                }
            }
        }

        public static void InsertPersona(EditablePersona objPersona)
        {
            if (objPersona != null)
            {
                using (Local_DB db = new Local_DB())
                {
                    var Item = new Persona();
                    Item.Nombre = objPersona.Nombre;
                    Item.FechaNacimiento = objPersona.FechaNacimiento;
                    Item.Fotografia = objPersona.Fotografia;
                    Item.Habilitado = true;
                    db.Persona.Add(Item);
                    db.SaveChanges();
                }
            }
        }
        public static void UpdatePersona(EditablePersona objPersona)
        {
            using (Local_DB db = new Local_DB())
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
        }
        public static void RemovePersonaByID(int id)
        {
            using (Local_DB db = new Local_DB())
            {
                var modelItem = db.Persona.FirstOrDefault(it => it.PersonaID == id);

                if (modelItem != null)
                {
                    modelItem.Habilitado = false;
                    db.SaveChanges();
                }
            }
        }

        static IList<EditableTelefono> GetAllTelefonos()
        {
            using (Local_DB db = new Local_DB())
            {
                var items = from i in db.Telefono
                            select
                                new EditableTelefono
                                {
                                    PersonaID = i.PersonaID,
                                    TelefonoID = i.TelefonoID,
                                    Numero = i.Numero
                                };
                return items.ToList();
            }
        }
        static IList<EditablePersona> GetAllPersonas()
        {

            using (Local_DB db = new Local_DB())
            {
                var items = from i in db.Persona
                            where i.Habilitado == true
                            select
                                new EditablePersona
                                {
                                    PersonaID = i.PersonaID,
                                    Nombre = i.Nombre,
                                    FechaNacimiento = i.FechaNacimiento,
                                    Habilitado = i.Habilitado
                                };
                return items.ToList();
            }

        }
    }

    public class EditablePersona
    {
        public int PersonaID { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public DateTime? FechaNacimiento { get; set; }
        public byte[] Fotografia { get; set; }
        public bool Habilitado { get; set; }
    }

    public class EditableTelefono
    {
        public int TelefonoID { get; set; }
        public int PersonaID { get; set; }
        [StringLength(10, ErrorMessage = "No debe de tener mas de 10 digitos")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Numero { get; set; }
    }
}