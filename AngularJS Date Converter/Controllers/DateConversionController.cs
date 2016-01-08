using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Globalization;

namespace AngularJS_Date_Converter.Controllers
{
    public class DateConversionController : ApiController
    {

        // POST api/dateconversion
        //Handle post event from AngularJS, pass date entered using value parameter
        public IHttpActionResult Post([FromBody]string value)
        {
            //msg and errMsg must be set to null to match conditions in the AngularJS
            //controller
            string msg = null;
            string errMsg = null;
            //Setup variables to convert user input and then convert to UTC
            DateTime retDate;
            DateTime enteredDate;
            //Parse user input, TryParse is the most lenient, supports short dates
            //as well as long dates and dates with times
            if (!DateTime.TryParse(value, out enteredDate))
            {
                //Conversion failed, let the user know with errMsg
                errMsg = "Invalid date";
            }
            else
            {
                //Conversion was successful, now convert result to UTC time
                retDate = TimeZoneInfo.ConvertTimeToUtc(enteredDate);
                //Return msg with parsed user input and UTC time to display to the user
                msg = enteredDate.ToString() + " local is " + retDate.ToString() + " UTC";
            }
            //return results to AngularJS controller in Json format
            return Json(new { Message = msg, ErrorMessage = errMsg });
        }
        
    }
}
