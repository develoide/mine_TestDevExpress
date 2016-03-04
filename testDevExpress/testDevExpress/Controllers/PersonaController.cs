
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using testDevExpress.Models;


namespace testDevExpress.Controllers
{
    public class PersonaController : Controller
    {
        //
        // GET: /Persona/
        clsPersona objPersona = new clsPersona();
        
        public ActionResult Index()
        {   
            return View(objPersona.GetItems());
        }
        public ActionResult PersonaPartial()
        {
            return PartialView("PersonaPartial", objPersona.GetItems());
        }

        public ActionResult NewPartial([ModelBinder(typeof(DevExpressEditorsBinder))] Persona obj)
        {
            if (ModelState.IsValid)
                objPersona.AddItem(obj);
            return PartialView("PersonaPartial", objPersona.GetItems());
        }
        public ActionResult UpdatePartial([ModelBinder(typeof(DevExpressEditorsBinder))] Persona obj)
        {
            if (ModelState.IsValid)
                objPersona.UpdateItem(obj);
            return PartialView("PersonaPartial", objPersona.GetItems());
        }
        public ActionResult DeletePartial(int PersonaID)
        {
            if (PersonaID >= 0)
            {
                try
                {
                    objPersona.DeleteItem(PersonaID);
                }
                catch (System.Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("PersonaPartial", objPersona.GetItems());
        }

        public ActionResult DetailPartial(int PersonaID)
        {
            
            ViewData["PersonaID"] = PersonaID;
            return PartialView("DetailPartial", clsTelefono.GetTelefonosPorPersona(PersonaID));
        }

        public ActionResult DetailAddNewPartial( clsTelefono item, int PersonaID)
        {
            
            if (ModelState.IsValid) clsTelefono.AddItem(item);

            return PartialView("DetailPartial", clsTelefono.GetTelefonosPorPersona(PersonaID));
        }

        public ActionResult DetailUpdatePartial(clsTelefono item, int PersonaID)
        {
           
            if (ModelState.IsValid)
            {
                clsTelefono.UpdateItem(item);
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("DetailPartial", clsTelefono.GetTelefonosPorPersona(PersonaID));
        }

        public ActionResult DetailDeletePartial( clsTelefono item, int PersonaID)
        {
            
            if (ModelState.IsValid)
            {
                    clsTelefono.DeleteItem(item.TelefonoID);
               
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("DetailPartial", clsTelefono.GetTelefonosPorPersona(PersonaID));
        }

        public ActionResult BinaryImageColumnPhotoUpdate()
        {
            return BinaryImageEditExtension.GetCallbackResult();
        }

    }
}
