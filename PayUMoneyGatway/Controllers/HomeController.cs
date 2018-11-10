using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PayUMoneyGatway.Models;

namespace PayUMoneyGatway.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult BuyNow()
        {
            return View();  
        }

        [HttpPost]
        public ActionResult BuyNow(RequestData model)
        {
            //string MerchantKey = "GySs88Ir";
            //string MerchantSalt = "5uBMabv8dC";
            string MerchantKey = "gtKFFx";
            string MerchantSalt = "eCwWELxi";


            string hash = "";
            string txnid = "TXN1234567896";


            
            String text = MerchantKey + "|" + txnid + "|" + model.Amount + "|" + "Cloths" + "|" + model.Name + "|" + model.Email + "|" + "1" + "|" + "1" + "|" + "1" + "|" + "1" + "|" + "1" + "||||||" + MerchantSalt;
            //Response.Write(text);
            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            hash = hex;

            System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
            data.Add("hash", hex.ToString());
            data.Add("txnid", txnid);
            data.Add("key", MerchantKey);
            // string AmountForm = ;// eliminating trailing zeros

            data.Add("amount", model.Amount);
            data.Add("firstname", model.Name);
            data.Add("email", model.Email);
            data.Add("phone", model.Mobile);
            data.Add("productinfo", "Cloths");
            data.Add("udf1", "1");
            data.Add("udf2", "1");
            data.Add("udf3", "1");
            data.Add("udf4", "1");
            data.Add("udf5", "1");

            data.Add("surl", "http://localhost:49959/Home/PaymentResponse");
            data.Add("furl", "http://localhost:49959/Home/PaymentResponse");

            data.Add("service_provider", "");//payu_paisa


            string strForm = PreparePOSTForm("https://test.payu.in/_payment", data);
            //Page.Controls.Add(new LiteralControl(strForm));
            ViewBag.htmlData = strForm;



            return View("Payment");
        }

        public ActionResult Payment()
        {
            return View();
        }
        public ActionResult PaymentResponse(ResponseData model)
        {
            return View(model);
        }



        private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");

            foreach (System.Collections.DictionaryEntry key in data)
            {

                strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                               "\" value=\"" + key.Value + "\">");
            }


            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }


    }
}