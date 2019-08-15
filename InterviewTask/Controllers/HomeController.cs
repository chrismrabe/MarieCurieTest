using HelperServiceModels.Models;
using InterviewTask.Helpers;
using HelperServices.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using log4net;
using System.Web.Script.Serialization;

namespace InterviewTask.Controllers
{
    public class HomeController : Controller
    {
        /*
         * Prepare your opening times here using the provided HelperServiceRepository class.       
         */

        private ILog logger = new ServicesLogger().Logger();

        public ActionResult Index()
        {
            try
            {
                IEnumerable<HelperServiceModel> services = new HelperServiceRepository().Get();
                ViewData["Services"] = services;
                return View("Index", services);
            } catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return View("Index");
        }
        
        public JsonResult WeatherData(Guid Id)
        {
            //Wouldn't this be better handled on client side? If we had the city or lat/long, we could pass that through direct to openW.
            logger.Info("Calling the weather service.");

            string responseData = string.Empty;

            HelperServiceModel thisService = new HelperServiceRepository().Get(Id); ;

            if(thisService == null)
            {
                string message = $"Service with ID: {Id} not found";
                logger.Error(message);
                responseData = new JavaScriptSerializer().Serialize(new { error = message });
                return Json(responseData, JsonRequestBehavior.AllowGet);
            }

            var request = (HttpWebRequest)WebRequest.Create($"http://api.openweathermap.org/data/2.5/weather?q={thisService.City}&APPID=e016391b92e54bfc19e468e0c7a5b8ec");
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    JsonSerializer serializer = new JsonSerializer();
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (var sreader = new StreamReader(dataStream))
                        {
                            responseData = sreader.ReadToEnd();
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    responseData = new JavaScriptSerializer().Serialize(new { error = ex.Message });
                }

            } else
            {
                logger.Error("Service not found");
                responseData = new JavaScriptSerializer().Serialize(new { error = "Trouble loading the weather" });
            }

            return Json(responseData, JsonRequestBehavior.AllowGet);
        }

    }
}