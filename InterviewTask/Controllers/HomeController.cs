using HelperServiceModels.Models;
using InterviewTask.Helpers;
using InterviewTask.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace InterviewTask.Controllers
{
    public class HomeController : Controller
    {
        /*
         * Prepare your opening times here using the provided HelperServiceRepository class.       
         */

        public ActionResult Index()
        {
            try
            {
                HelperServiceRepository repo = new HelperServiceRepository();

                IEnumerable<HelperServiceModel> services = repo.Get();

                ViewData["Services"] = services;

                return View("Index", services);
            } catch (Exception ex)
            {
                ServicesLogger.Log(ex.Message);
                return View("Index");
            }

        }
        
        public JsonResult WeatherData(Guid Id)
        {
            //Wouldn't this be better handled purely on client side?

            ServicesLogger.Log("Calling Weather Service");

            HelperServiceRepository repo = new HelperServiceRepository();

            string responseData = "";

            try
            {
                //Ideally we would be retrieving only one entry based on ID.

                //Ideally all data access would be in separate project

                IEnumerable<HelperServiceModel> services = repo.Get();
                HelperServiceModel thisService = new HelperServiceModel();

                foreach (var service in services)
                {
                    if (service.Id == Id)
                    {
                        thisService = service;
                    }
                }

                var request = (HttpWebRequest)WebRequest.Create($"http://api.openweathermap.org/data/2.5/weather?q={thisService.City}&APPID=e016391b92e54bfc19e468e0c7a5b8ec");
                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();

                JsonSerializer serializer = new JsonSerializer();
                using (Stream dataStream = response.GetResponseStream())
                {
                    using (var sreader = new StreamReader(dataStream))
                    {
                        responseData = sreader.ReadToEnd();
                    }
                }
            } catch (Exception ex)
            {

                ServicesLogger.Log(ex.Message);

            }

            return Json(responseData, JsonRequestBehavior.AllowGet);
        }

    }
}