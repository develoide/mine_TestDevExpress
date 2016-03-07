
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using testDevExpress.Models;




namespace testDevExpress.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Directorio.Personas);
        }
        public ActionResult GridViewMasterPartial()
        {
            return PartialView("GridViewMasterPartial", Directorio.Personas);
        }
        public ActionResult GridViewMasterAddNewPartial([ModelBinder(typeof(DevExpressEditorsBinder))] EditablePersona persona)
        {
            if (ModelState.IsValid)
                Directorio.InsertPersona(persona);
            return PartialView("GridViewMasterPartial", Directorio.Personas);
        }
        public ActionResult GridViewMasterUpdatePartial([ModelBinder(typeof(DevExpressEditorsBinder))] EditablePersona persona)
        {
            if (ModelState.IsValid)
                Directorio.UpdatePersona(persona);
            return PartialView("GridViewMasterPartial", Directorio.Personas);
        }
        public ActionResult GridViewMasterDeletePartial(int PersonaID)
        {
            Directorio.RemovePersonaByID(PersonaID);
            return PartialView("GridViewMasterPartial", Directorio.Personas);
        }

        public ActionResult GridViewDetailPartial(int PersonaID)
        {
            ViewData["PersonaID"] = PersonaID;
            return PartialView("GridViewDetailPartial", Directorio.GetTelefonosByPersona(PersonaID));
        }
        public ActionResult GridViewDetailAddNewPartial([ModelBinder(typeof(DevExpressEditorsBinder))] EditableTelefono telefono, int PersonaID)
        {
            ViewData["PersonaID"] = PersonaID;
            telefono.PersonaID = PersonaID;
            if (ModelState.IsValid)
                Directorio.InsertTelefono(telefono);
            return PartialView("GridViewDetailPartial", Directorio.GetTelefonosByPersona(PersonaID));
        }
        public ActionResult GridViewDetailUpdatePartial([ModelBinder(typeof(DevExpressEditorsBinder))] EditableTelefono telefono, int PersonaID)
        {
            ViewData["PersonaID"] = PersonaID;
            if (ModelState.IsValid)
                Directorio.UpdateTelefono(telefono);
            return PartialView("GridViewDetailPartial", Directorio.GetTelefonosByPersona(PersonaID));
        }
        public ActionResult GridViewDetailDeletePartial(int TelefonoID, int PersonaID)
        {
            ViewData["PersonaID"] = PersonaID;
            Directorio.RemoveTelefonoByID(TelefonoID);
            return PartialView("GridViewDetailPartial", Directorio.GetTelefonosByPersona(PersonaID));
        }

        public ActionResult BinaryImageColumnPhotoUpdate()
        {
            return BinaryImageEditExtension.GetCallbackResult();
        }
    }
}
