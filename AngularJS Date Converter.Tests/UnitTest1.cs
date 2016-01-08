using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngularJS_Date_Converter;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Threading;
using System.IO;
using System.Text;

namespace AngularJS_Date_Converter.Tests
{
    public class JsonData
    {
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestValidDate()
        {
            //Modify this string to "" to make the test fail
            string dateToTest = "01/07/2016";

            //Instantiate controller and setup Http configuration
            var tstController = new Controllers.DateConversionController();
            tstController.Request = new HttpRequestMessage();
            tstController.Configuration = new HttpConfiguration();

            //Cancellation token required to use ExecuteAsync
            CancellationToken cancelToken = new CancellationToken();

            //Obtain the response from the controller
            HttpResponseMessage response = tstController.Post(dateToTest).ExecuteAsync(cancelToken).Result;

            //Read back response.Content as stream to convert to string
            Stream receiveStream = response.Content.ReadAsStreamAsync().Result;
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string jsonResult = readStream.ReadToEnd();

            //Deserialize jsonResult from the controller
            var tmpData = JsonConvert.DeserializeObject<JsonData>(jsonResult);

            //If Message is not null then assert the test is true
            Assert.IsTrue(tmpData.Message != null);
        }

        [TestMethod]
        public void TestInvalidDate()
        {
            //Modify this string to a valid date string to make the test fail
            string dateToTest = "654987321";

            //Instantiate controller and setup Http configuration
            var tstController = new Controllers.DateConversionController();
            tstController.Request = new HttpRequestMessage();
            tstController.Configuration = new HttpConfiguration();

            //Cancellation token required to use ExecuteAsync
            CancellationToken cancelToken = new CancellationToken();

            //Obtain the response from the controller
            HttpResponseMessage response = tstController.Post(dateToTest).ExecuteAsync(cancelToken).Result;

            //Read back response.Content as stream to convert to string
            Stream receiveStream = response.Content.ReadAsStreamAsync().Result;
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string jsonResult = readStream.ReadToEnd();

            //Deserialize jsonResult from the controller
            var tmpData = JsonConvert.DeserializeObject<JsonData>(jsonResult);

            //If ErrorMessage is not null then assert the test is true
            Assert.IsTrue(tmpData.ErrorMessage != null);
        }
    }
}
