using ContactManagement.Entity.Entity;
using ContactManagement.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Http.Description;
using ContactManagement.Entity.Model;
using System.Security.Policy;

namespace ContactManagement.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/contact")]
    public class ContactController : ApiController
    {
        private UnitOfWork<AppDbContext> unitOfWork = new UnitOfWork<AppDbContext>();
        private GenericRepository<Contact> repository;
        private IContactRepository contact_repository;
        public ContactController()
        {
            repository = new GenericRepository<Contact>(unitOfWork);
            this.contact_repository = new ContactRepository(unitOfWork);
        }
        public ContactController(ContactRepository repository)
        {
            this.contact_repository = repository;
        }

        [HttpPost]
        [Route("All")]
        [ResponseType(typeof(ResultData<List<Contact>>))]
        public IHttpActionResult GetAll(DataTablesHelper dataTables)
        {            
            try
            {
                var contacts = repository.GetAll(dataTables);                
                ResultData<List<Contact>> result = new ResultData<List<Contact>>();
                result.IsSuccess = true;
                result.Data = contacts.ToList();
                result.HttpCode = HttpStatusCode.OK;
                result.Message = string.Format("{0} total contacts", repository.Count());
                return Ok(result);
            }
            catch (Exception ex)
            {
                ResultData<string> result = new ResultData<string>();
                result.IsSuccess = false;
                result.Data = ex.InnerException != null ? ex.Message + " Additional Information: " + ex.InnerException.Message : ex.Message;
                result.HttpCode = HttpStatusCode.InternalServerError;
                result.Message = "Error occurred while getting all the contacts. Error Information: " + ex.ToString();
                return Ok(result);
                //return BadRequest("Error Occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("active")]
        [ResponseType(typeof(ResultData<List<Contact>>))]
        public IHttpActionResult GetActiveContacts()
        {
            try
            {
                var contacts = contact_repository.GetActiveContacts();
                ResultData<List<Contact>> result = new ResultData<List<Contact>>();
                result.IsSuccess = true;
                result.Data = contacts.ToList();
                result.HttpCode = HttpStatusCode.OK;
                result.Message = string.Format("{0} active contacts found.", contacts.Count());
                return Ok(result);
            }
            catch (Exception ex)
            {
                ResultData<string> result = new ResultData<string>();
                result.IsSuccess = false;
                result.Data = ex.InnerException != null ? ex.Message + " Additional Information: " + ex.InnerException.Message : ex.Message;
                result.HttpCode = HttpStatusCode.InternalServerError;
                result.Message = "Error occurred while getting active contacts. Error Information: " + ex.ToString();
                return Ok(result);
            }
        }

        [HttpGet]
        [Route("inactive")]
        [ResponseType(typeof(ResultData<List<Contact>>))]
        public IHttpActionResult GetInActiveContacts()
        {
            try
            {
                var contacts = contact_repository.GetInActiveContacts();
                ResultData<List<Contact>> result = new ResultData<List<Contact>>();
                result.IsSuccess = true;
                result.Data = contacts.ToList();
                result.HttpCode = HttpStatusCode.OK;
                result.Message = string.Format("{0} inactive contacts found.", contacts.Count());
                return Ok(result);
            }
            catch (Exception ex)
            {
                ResultData<string> result = new ResultData<string>();
                result.IsSuccess = false;
                result.Data = ex.InnerException != null ? ex.Message + " Additional Information: " + ex.InnerException.Message : ex.Message;
                result.HttpCode = HttpStatusCode.InternalServerError;
                result.Message = "Error occurred while getting inactive contacts. Error Information: " + ex.ToString();
                return Ok(result);
            }
        }

        [HttpGet]
        [Route("Get")]
        [ResponseType(typeof(ResultData<Contact>))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var contact = repository.GetById(id); 
                ResultData<Contact> result = new ResultData<Contact>();
                result.IsSuccess = true;
                result.Data = contact;
                result.HttpCode = HttpStatusCode.OK;
                result.Message = "contact found";
                return Ok(result);
            }
            catch (Exception ex)
            {
                ResultData<string> result = new ResultData<string>();
                result.IsSuccess = false;
                result.Data = ex.InnerException != null ? ex.Message + " Additional Information: " + ex.InnerException.Message : ex.Message;
                result.HttpCode = HttpStatusCode.InternalServerError;
                result.Message = "Error occurred while getting the contact information. Error Information: " + ex.ToString();
                return Ok(result);
            }
        }

        [HttpPost]
        [Route("save")]
        [ResponseType(typeof(ResultData<string>))]
        public IHttpActionResult Save(Contact contact)
        {
            try
            {
                repository.Insert(contact);
                unitOfWork.Save();
                int id = contact.ID;
                ResultData<string> result = new ResultData<string>();
                result.IsSuccess = true;
                result.Data = id.ToString();
                result.HttpCode = HttpStatusCode.OK;
                result.Message = "contact saved";
                return Ok(result);
            }
            catch (Exception ex)
            {
                ResultData<string> result = new ResultData<string>();
                result.IsSuccess = false;
                result.Data = ex.InnerException != null ? ex.Message + " Additional Information: " + ex.InnerException.Message : ex.Message;
                result.HttpCode = HttpStatusCode.InternalServerError;
                result.Message = "Error occurred while saving the contact. Error Information: " + ex.ToString();
                return Ok(result);
            }
        }

        [HttpPost]
        [Route("update")]
        [ResponseType(typeof(ResultData<string>))]
        public IHttpActionResult Update(Contact contact)
        {
            try
            {
                repository.Update(contact);
                unitOfWork.Save();
                int id = contact.ID;
                ResultData<string> result = new ResultData<string>();
                result.IsSuccess = true;
                result.Data = "Contact updated successfully.";
                result.HttpCode = HttpStatusCode.OK;
                result.Message = "contact updated";
                return Ok(result);
            }
            catch (Exception ex)
            {
                ResultData<string> result = new ResultData<string>();
                result.IsSuccess = false;
                result.Data = ex.InnerException != null ? ex.Message + " Additional Information: " + ex.InnerException.Message : ex.Message;
                result.HttpCode = HttpStatusCode.InternalServerError;
                result.Message = "Error occurred while updating the contact. Error Information: " + ex.ToString();
                return Ok(result);                
            }
        }

        [HttpDelete]
        [Route("delete")]
        [ResponseType(typeof(ResultData<string>))]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var contact = repository.GetById(id);
                if (contact != null)
                {
                    repository.Delete(contact);
                    unitOfWork.Save();
                    //int id = contact.ID;
                    ResultData<string> result = new ResultData<string>();
                    result.IsSuccess = true;
                    result.Data = "Contact deleted successfully.";
                    result.HttpCode = HttpStatusCode.OK;
                    result.Message = "contact deleted";
                    return Ok(result);
                }
                else
                {
                    ResultData<string> result = new ResultData<string>();
                    result.IsSuccess = false;
                    result.Data = "Contact not found.";
                    result.HttpCode = HttpStatusCode.InternalServerError;
                    result.Message = "Contact not found";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                ResultData<string> result = new ResultData<string>();
                result.IsSuccess = false;
                result.Data = ex.InnerException != null ? ex.Message + " Additional Information: " + ex.InnerException.Message : ex.Message;
                result.HttpCode = HttpStatusCode.InternalServerError;
                result.Message = "Error occurred while deleting the contact. Error Information: " + ex.ToString();
                return Ok(result);
            }
        }
    }
}
