using HelperServiceModels.Models;
using System;
using System.Collections.Generic;

namespace InterviewTask.Helpers
{
    public static class ServiceHelpers
    {
        //TODO: Unit tests

        public static string OpeningHours(HelperServiceModel service)
        {
            if(service == null)
            {
                return string.Empty;
            }

            int currentHour = DateTime.Now.Hour;

            List<int> openingHours = TodaysHours(service);
            if(openingHours == null)
            {
                return string.Empty;
            }

            if (!IsOpen(openingHours, currentHour))
            {

                if (openingHours[0] > currentHour)
                {
                    return $"Opens at {FormatTime(openingHours[0])}";
                }

                return NextOpenDay(service);

            }
            else
            {
                return $"Open until {FormatTime(openingHours[1])}";
            }

        }

        public static List<int> TodaysHours(HelperServiceModel service)
        {
            DateTime now = DateTime.Now;

            switch (now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return service.MondayOpeningHours;
                case DayOfWeek.Tuesday:
                    return service.TuesdayOpeningHours;
                case DayOfWeek.Wednesday:
                    return service.WednesdayOpeningHours;
                case DayOfWeek.Thursday:
                    return service.ThursdayOpeningHours;
                case DayOfWeek.Friday:
                    return service.FridayOpeningHours;
                case DayOfWeek.Saturday:
                    return service.SaturdayOpeningHours;
                case DayOfWeek.Sunday:
                default:
                    return service.SundayOpeningHours;
            }
        }

        private static string NextOpenDay(HelperServiceModel service)
        {
            try
            {
                DayOfWeek dayToCheck = DateTime.Now.DayOfWeek;

                for (var daysTried = 0; daysTried < 7; daysTried++)
                {
                    if (dayToCheck > DayOfWeek.Saturday)
                    {
                        dayToCheck = DayOfWeek.Sunday;
                    }
                    else
                    {
                        dayToCheck = ++dayToCheck;
                    }

                    switch (dayToCheck)
                    {
                        case DayOfWeek.Monday:
                            if (service.MondayOpeningHours[0] > 0)
                            {
                                return $"Reopens Monday {FormatTime(service.MondayOpeningHours[0])}";
                            }
                            break;
                        case DayOfWeek.Tuesday:
                            if (service.TuesdayOpeningHours[0] > 0)
                            {
                                return $"Reopens Tuesday {FormatTime(service.TuesdayOpeningHours[0])}";
                            }
                            break;
                        case DayOfWeek.Wednesday:
                            if (service.WednesdayOpeningHours[0] > 0)
                            {
                                return $"Reopens Wednesday {FormatTime(service.WednesdayOpeningHours[0])}";
                            }
                            break;
                        case DayOfWeek.Thursday:
                            if (service.ThursdayOpeningHours[0] > 0)
                            {
                                return $"Reopens Thursday {FormatTime(service.ThursdayOpeningHours[0])}";
                            }
                            break;
                        case DayOfWeek.Friday:
                            if (service.FridayOpeningHours[0] > 0)
                            {
                                return $"Reopens Friday {FormatTime(service.FridayOpeningHours[0])}";
                            }
                            break;
                        case DayOfWeek.Saturday:
                            if (service.SaturdayOpeningHours[0] > 0)
                            {
                                return $"Reopens Saturday {FormatTime(service.SaturdayOpeningHours[0])}";
                            }
                            break;
                        case DayOfWeek.Sunday:
                            if (service.SaturdayOpeningHours[0] > 0)
                            {
                                return $"Reopens Saturday {FormatTime(service.SaturdayOpeningHours[0])}";
                            }
                            break;

                    }

                }
            }
            catch (InvalidOperationException ex)
            {
            }

            return string.Empty;

        }

        private static string FormatTime(int evaluateTime)
        {
            if(evaluateTime == 0)
            {
                return "Midnight";
            }

            int displayTwelveHour = evaluateTime > 12 ? evaluateTime - 12 : evaluateTime;

            return evaluateTime >= 12 ? $"{displayTwelveHour}PM" : $"{displayTwelveHour}AM";

        }

        public static bool IsOpen(List<int> openingHours, int currentHour)
        {
            if (openingHours == null || openingHours[0] == 0)
            {
                return false;
            }

            if (currentHour >= openingHours[0] && currentHour < openingHours[1])
            {
                return true;
            }

            return false;
        }
    }
}