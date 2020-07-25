using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using SharpArch.Core;
using SharpArch.Web.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.RepositoryInterfaces;
using YTech.IM.GSP.Data.Repository;
using YTech.IM.GSP.Enums;
using YTech.IM.GSP.Web.Controllers.ViewModel;

namespace YTech.IM.GSP.Web.Controllers.Master
{
    [HandleError]
    public class CustomerController : Controller
    {
        //public CustomerController() : this(new MSupplierRepository(), new RefAddressRepository())
        //{}

        private readonly IMCustomerRepository _mCustomerRepository;
        private readonly IRefPersonRepository _refPersonRepository;
        private readonly IRefAddressRepository _refAddressRepository;
        public CustomerController(IMCustomerRepository mCustomerRepository, IRefPersonRepository refPersonRepository, IRefAddressRepository refAddressRepository)
        {
            Check.Require(mCustomerRepository != null, "mCustomerRepository may not be null");
            Check.Require(refPersonRepository != null, "refPersonRepository may not be null");
            Check.Require(refAddressRepository != null, "refAddressRepository may not be null");

            this._mCustomerRepository = mCustomerRepository;
            this._refPersonRepository = refPersonRepository;
            this._refAddressRepository = refAddressRepository;
        }



        public ActionResult Search(bool? popUp = true)
        {
            if (popUp.HasValue)
            {
                if (!popUp.Value)
                    return View("Search", "MyMaster", null);
            }
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [Transaction]
        public virtual ActionResult ListSearch(string sidx, string sord, int page, int rows, string searchBy, string searchText)
        {
            int totalRecords = 0;
            var custs = _mCustomerRepository.GetPagedCustomerList(sidx, sord, page, rows, ref totalRecords, searchBy, searchText);
            int pageSize = rows;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from cust in custs
                    select new
                    {
                        i = cust.Id.ToString(),
                        cell = new string[] {
                            cust.Id,  
                          cust.PersonId != null?  cust.PersonId.PersonName : null,  
                          cust.PersonId != null?  cust.PersonId.PersonGender : null, 
                             cust.AddressId != null?  cust.AddressId.AddressLine1 : null, 
                          cust.AddressId != null?  cust.AddressId.AddressLine2 : null,
                          cust.AddressId != null?  cust.AddressId.AddressPhone : null,
                          cust.AddressId != null?  cust.AddressId.AddressCity : null,
                            cust.CustomerDesc,
                        }
                    }).ToArray()
            };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [Transaction]
        public virtual ActionResult List(string sidx, string sord, int page, int rows)
        {
            int totalRecords = 0;
            var custs = _mCustomerRepository.GetPagedCustomerList(sidx, sord, page, rows, ref totalRecords);
            int pageSize = rows;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from cust in custs
                    select new
                    {
                        i = cust.Id.ToString(),
                        cell = new string[] {
                            string.Empty,
                            cust.Id, 
                            cust.PersonId != null ?    cust.PersonId.PersonFirstName : null, 
                          cust.PersonId != null ?    cust.PersonId.PersonLastName : null, 
                          cust.PersonId != null ?    cust.PersonId.PersonName : null,  
                          cust.AddressId != null?  cust.AddressId.AddressLine1 : null,
                          cust.AddressId != null?  cust.AddressId.AddressLine2 : null,
                          cust.AddressId != null?  cust.AddressId.AddressLine3 : null,
                          cust.AddressId != null?  cust.AddressId.AddressPhone : null,
                          cust.AddressId != null?  cust.AddressId.AddressCity : null,
                            cust.CustomerDesc
                        }
                    }).ToArray()
            };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [Transaction]
        public ActionResult Insert(MCustomer viewModel, FormCollection formCollection)
        {
            RefAddress address = new RefAddress();
            TransferFormValuesTo(address, formCollection);
            address.SetAssignedIdTo(Guid.NewGuid().ToString());
            address.CreatedDate = DateTime.Now;
            address.CreatedBy = User.Identity.Name;
            address.DataStatus = EnumDataStatus.New.ToString();
            _refAddressRepository.Save(address);

            RefPerson person = new RefPerson();
            TransferFormValuesTo(person, formCollection);
            person.SetAssignedIdTo(Guid.NewGuid().ToString());
            person.CreatedDate = DateTime.Now;
            person.CreatedBy = User.Identity.Name;
            person.DataStatus = EnumDataStatus.New.ToString();
            _refPersonRepository.Save(person);

            MCustomer customer = new MCustomer();
            TransferFormValuesTo(customer, viewModel);
            customer.SetAssignedIdTo(viewModel.Id);
            customer.CreatedDate = DateTime.Now;
            customer.CreatedBy = User.Identity.Name;
            customer.DataStatus = EnumDataStatus.New.ToString();

            customer.AddressId = address;
            customer.PersonId = person;

            _mCustomerRepository.Save(customer);

            try
            {
                _mCustomerRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {

                _mCustomerRepository.DbContext.RollbackTransaction();

                //throw e.GetBaseException();
                return Content(e.GetBaseException().Message);
            }

            return Content("success");
        }

        [Transaction]
        public ActionResult Update(MCustomer viewModel, FormCollection formCollection)
        {
            MCustomer customer = _mCustomerRepository.Get(viewModel.Id);
            TransferFormValuesTo(customer, viewModel);
            customer.ModifiedDate = DateTime.Now;
            customer.ModifiedBy = User.Identity.Name;
            customer.DataStatus = EnumDataStatus.Updated.ToString();

            RefAddress address = customer.AddressId;
            TransferFormValuesTo(address, formCollection);
            customer.ModifiedDate = DateTime.Now;
            customer.ModifiedBy = User.Identity.Name;
            customer.DataStatus = EnumDataStatus.Updated.ToString();

            RefPerson person = customer.PersonId;
            TransferFormValuesTo(person, formCollection);
            customer.ModifiedDate = DateTime.Now;
            customer.ModifiedBy = User.Identity.Name;
            customer.DataStatus = EnumDataStatus.Updated.ToString();

            _mCustomerRepository.Update(customer);

            try
            {
                _mCustomerRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {

                _mCustomerRepository.DbContext.RollbackTransaction();

                //throw e.GetBaseException();
                return Content(e.GetBaseException().Message);
            }

            return Content("success");
        }

        [Transaction]
        public ActionResult Delete(MCustomer viewModel, FormCollection formCollection)
        {
            MCustomer customer = _mCustomerRepository.Get(viewModel.Id);

            if (customer != null)
            {
                _refAddressRepository.Delete(customer.AddressId);
                _refPersonRepository.Delete(customer.PersonId);
                _mCustomerRepository.Delete(customer);
            }

            try
            {
                _mCustomerRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {

                _mCustomerRepository.DbContext.RollbackTransaction();

                return Content(e.GetBaseException().Message);
            }

            return Content("success");
        }

        private void TransferFormValuesTo(RefAddress address, FormCollection formCollection)
        {
            address.AddressLine1 = formCollection["AddressLine1"];
            address.AddressLine2 = formCollection["AddressLine2"];
            address.AddressLine3 = formCollection["AddressLine3"];
            address.AddressPhone = formCollection["AddressPhone"];
            address.AddressCity = formCollection["AddressCity"];
            address.AddressRt = formCollection["AddressRt"];
            address.AddressRw = formCollection["AddressRw"];
            address.AddressPostCode = formCollection["AddressPostCode"];


            address.AddressIdCardLine1 = formCollection["AddressIdCardLine1"];
            address.AddressIdCardLine2 = formCollection["AddressIdCardLine2"];
            address.AddressIdCardLine3 = formCollection["AddressIdCardLine3"];
            address.AddressIdCardPhone = formCollection["AddressIdCardPhone"];
            address.AddressIdCardCity = formCollection["AddressIdCardCity"];
            address.AddressIdCardRt = formCollection["AddressIdCardRt"];
            address.AddressIdCardRw = formCollection["AddressIdCardRw"];
            address.AddressIdCardPostCode = formCollection["AddressIdCardPostCode"];

        }

        private static void TransferFormValuesTo(MCustomer customer, MCustomer customerForm)
        {
            // customer.CustomerDesc = customerForm.CustomerDesc; 
            customer = customerForm;
        }

        private void TransferFormValuesTo(RefPerson person, FormCollection formCollection)
        {
            person.PersonFirstName = formCollection["PersonFirstName"];
            person.PersonLastName = formCollection["PersonLastName"];
            person.PersonGender = formCollection["PersonGender"];
            if (!string.IsNullOrEmpty(formCollection["PersonDob"]))
                person.PersonDob = Convert.ToDateTime(formCollection["PersonDob"]);
            else
                person.PersonDob = null;
            person.PersonPob = formCollection["PersonPob"];
            person.PersonPhone = formCollection["PersonPhone"];
            person.PersonMobile = formCollection["PersonMobile"];
            person.PersonEmail = formCollection["PersonEmail"];
            //person.PersonRace = formCollection["PersonRace"];
            person.PersonIdCardType = formCollection["PersonIdCardType"];
            person.PersonIdCardNo = formCollection["PersonIdCardNo"];
            person.PersonOccupation = formCollection["PersonOccupation"];
            person.PersonNationality = formCollection["PersonNationality"];
            person.PersonMarriedStatus = formCollection["PersonMarriedStatus"];
            person.PersonReligion = formCollection["PersonReligion"];
            person.PersonOccupation = formCollection["PersonOccupation"];
            person.PersonOfficceName = formCollection["PersonOfficceName"];
            person.PersonOfficceAddress = formCollection["PersonOfficceAddress"];
            person.PersonOfficceCity = formCollection["PersonOfficceCity"];
            person.PersonOfficcePhone = formCollection["PersonOfficcePhone"];
            person.PersonOfficcePostCode = formCollection["PersonOfficcePostCode"];
            person.PersonOfficceFax = formCollection["PersonOfficceFax"];
            person.PersonLastEdu = formCollection["PersonLastEdu"];
            person.PersonBloodType = formCollection["PersonBloodType"];
            person.PersonHobby = formCollection["PersonHobby"];
        }

        [Transaction]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CheckCustomer(string customerId)
        {
            if (!string.IsNullOrEmpty(customerId))
            {
                MCustomer c = _mCustomerRepository.Get(customerId);
                if (c != null)
                    return Content(false.ToString().ToLower());
            }

            return Content(true.ToString().ToLower());
        }

        [Transaction]
        public ActionResult Edit(string customerId)
        {
            ViewData["CurrentItem"] = "Identitas Pasien";
            RegistrationFormViewModel viewModel =
                RegistrationFormViewModel.CreateRegistrationFormViewModel(_mCustomerRepository, customerId);
            return View(viewModel);
        }

        [Transaction]
        public ActionResult Registration()
        {
            ViewData["CurrentItem"] = "Identitas Pasien";
            RegistrationFormViewModel viewModel =
                RegistrationFormViewModel.CreateRegistrationFormViewModel(_mCustomerRepository, null);
            //if (usePopup.HasValue)
            //{
            //    if (usePopup.Value)
            //        return View("Registration", "MasterPopup", viewModel);
            //}
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]      // Helps avoid CSRF attacks
        [Transaction]                   // Wraps a transaction around the action
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Registration(MCustomer customer, FormCollection formCollection)
        {
            _mCustomerRepository.DbContext.BeginTransaction();
            RefAddress address = new RefAddress();
            RefPerson person = new RefPerson();
            TransferFormValuesTo(address, formCollection);
            address.SetAssignedIdTo(Guid.NewGuid().ToString());
            address.CreatedDate = DateTime.Now;
            address.CreatedBy = User.Identity.Name;
            address.DataStatus = EnumDataStatus.New.ToString();
            _refAddressRepository.Save(address);

            TransferFormValuesTo(person, formCollection);
            person.SetAssignedIdTo(Guid.NewGuid().ToString());
            person.CreatedDate = DateTime.Now;
            person.CreatedBy = User.Identity.Name;
            person.DataStatus = EnumDataStatus.New.ToString();
            _refPersonRepository.Save(person);

            //MCustomer customer = new MCustomer();
            //TransferFormValuesTo(customer, cust);
            if (customer == null)
            {
                customer = new MCustomer();
            }
            customer.SetAssignedIdTo(formCollection["Id"]);
            customer.CreatedDate = DateTime.Now;
            customer.CreatedBy = User.Identity.Name;
            customer.DataStatus = EnumDataStatus.New.ToString();

            customer.AddressId = address;
            customer.PersonId = person;

            _mCustomerRepository.Save(customer);

            try
            {
                _mCustomerRepository.DbContext.CommitChanges();
                TempData[EnumCommonViewData.SaveState.ToString()] = EnumSaveState.Success;
            }
            catch (Exception e)
            {
                _mCustomerRepository.DbContext.RollbackTransaction();
                TempData[EnumCommonViewData.SaveState.ToString()] = EnumSaveState.Failed;
                TempData[EnumCommonViewData.ErrorMessage.ToString()] = e.Message;

                //throw e.GetBaseException(); 
                var result = new
                {
                    Success = false,
                    Message = e.Message
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var resultx = new
            {
                Success = true,
                Message = //Helper.ViewHelper.RenderPartialToString("~/Views/Shared/Status.ascx", "", null, this.ControllerContext.RequestContext)
                @" <div class='ui-state-highlight ui-corner-all' style='padding: 5pt; margin-bottom: 5pt;'>
                            <p>
                                <span class='ui-icon ui-icon-info' style='float: left; margin-right: 0.3em;'></span>
                                Data berhasil disimpan.</p>
                        </div>"
            };
            return Json(resultx, JsonRequestBehavior.AllowGet);
            //return View("Status");
        }

        [ValidateAntiForgeryToken]      // Helps avoid CSRF attacks
        [Transaction]                   // Wraps a transaction around the action
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(MCustomer customer, FormCollection formCollection, string customerId)
        {
            _mCustomerRepository.DbContext.BeginTransaction();
            RefAddress address = new RefAddress();
            RefPerson person = new RefPerson();

            {
                customer = _mCustomerRepository.Get(customerId);
                //customer.CustomerFaceTreatment = formCollection["CustomerFaceTreatment"];
                //customer.CustomerAllergy = formCollection["CustomerAllergy"];
                //customer.CustomerSkinProblem = formCollection["CustomerSkinProblem"];
                //customer.CustomerPlanTreatment = formCollection["CustomerPlanTreatment"];
                //customer.CustomerPhoneJakarta = formCollection["CustomerPhoneJakarta"];
                //customer.CustomerLetter = formCollection["CustomerLetter"]; 
                customer.ModifiedDate = DateTime.Now;
                customer.ModifiedBy = User.Identity.Name;
                customer.DataStatus = EnumDataStatus.Updated.ToString();

                address = customer.AddressId;
                TransferFormValuesTo(address, formCollection);
                //address.AddressLine1 = formCollection["AddressLine1"];
                //address.AddressLine2 = formCollection["AddressLine2"];
                //address.AddressRt = formCollection["AddressRt"];
                //address.AddressRw = formCollection["AddressRw"];
                //address.AddressCity = formCollection["AddressCity"];
                //address.AddressPostCode = formCollection["AddressPostCode"];
                //address.AddressPhone = formCollection["AddressPhone"];
                //address.AddressFax = formCollection["AddressFax"];
                //address.AddressEmail = formCollection["AddressEmail"]; 
                address.ModifiedDate = DateTime.Now;
                address.ModifiedBy = User.Identity.Name;
                address.DataStatus = EnumDataStatus.Updated.ToString();

                person = customer.PersonId;
                TransferFormValuesTo(person, formCollection);
                //person.PersonFirstName = formCollection["PersonFirstName"];
                //person.PersonAnotherName = formCollection["PersonAnotherName"];
                //person.PersonPob = formCollection["PersonPob"];
                //if (!string.IsNullOrEmpty(formCollection["PersonDob"]))
                //    person.PersonDob = Convert.ToDateTime(formCollection["PersonDob"]);
                //else
                //    person.PersonDob = null;
                //person.PersonMobile = formCollection["PersonMobile"];
                //person.PersonOccupation = formCollection["PersonJob"];
                //person.PersonOfficceName = formCollection["PersonOfficceName"];
                //person.PersonOfficceAddress = formCollection["PersonOfficceAddress"];
                //person.PersonOfficceCity = formCollection["PersonOfficceCity"];
                //person.PersonOfficcePostCode = formCollection["PersonOfficcePostCode"];
                //person.PersonOfficcePhone = formCollection["PersonOfficcePhone"];
                //person.PersonOfficceFax = formCollection["PersonOfficceFax"];
                //person.PersonReligion = formCollection["PersonReligion"];
                //person.PersonLastEdu = formCollection["PersonLastEdu"];
                //person.PersonMarriedStatus = formCollection["PersonMarriedStatus"];
                //person.PersonHobby = formCollection["PersonHobby"];
                person.ModifiedDate = DateTime.Now;
                person.ModifiedBy = User.Identity.Name;
                person.DataStatus = EnumDataStatus.Updated.ToString();

                _mCustomerRepository.Update(customer);
            }

            try
            {
                _mCustomerRepository.DbContext.CommitChanges();
                TempData[EnumCommonViewData.SaveState.ToString()] = EnumSaveState.Success;

            }
            catch (Exception e)
            {
                _mCustomerRepository.DbContext.RollbackTransaction();
                TempData[EnumCommonViewData.SaveState.ToString()] = EnumSaveState.Failed;
                TempData[EnumCommonViewData.ErrorMessage.ToString()] = e.GetBaseException().Message;

                //throw e.GetBaseException();

                var result = new
               {
                   Success = false,
                   Message = e.Message
               };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var resultx = new
            {
                Success = true,
                Message = @" <div class='ui-state-highlight ui-corner-all' style='padding: 5pt; margin-bottom: 5pt;'>
                            <p>
                                <span class='ui-icon ui-icon-info' style='float: left; margin-right: 0.3em;'></span>
                                Data berhasil disimpan.</p>
                        </div>"
            };
            return Json(resultx, JsonRequestBehavior.AllowGet);
            //}

            //return View("Status");
        }
    }
}
