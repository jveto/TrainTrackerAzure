using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainTracker.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TrainTracker.Controllers
{
    public class HomeController : Controller
    {

        private Context _context;
        public HomeController(Context context){
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet ("Dashboard")]
        public IActionResult Dashboard () {
            return View ();
        }

        [HttpGet ("Tips")]
        public IActionResult Tips () {
            ViewBag.Tips = _context.Tips.OrderBy(t => t.Date);
            return View ();
        }

        [HttpPost ("Tips")]
        public IActionResult PostTip(Tip submission) {
            submission.Date = DateTime.Now;
            _context.Add(submission);
            _context.SaveChanges();
            return RedirectToAction("Tips");
        }

        [HttpGet]
        [Route("/map")]
        public IActionResult Map()
        {
            return View();
        }

        [HttpGet("dashboard3")]
        public IActionResult dashboard3(){
            return View();
        }

        [HttpPost]
        [Route("/singletrain")]
        public IActionResult SingleTrain(int id)
        {
            ViewBag.id = id;
            return View("singletrain");
        }

        [HttpGet("makeDB")]
        public IActionResult DBMe(){
            string URL = "https://data.cityofchicago.org/resource/8mj8-j3c4.json";
            string urlParameters;

            // Create dictionary to run through each API request
            Dictionary<string, string> Stations = new Dictionary<string, string>();
            Stations.Add("red", "Red");
            Stations.Add("blue", "Blue");
            Stations.Add("g", "Green");
            Stations.Add("brn", "Brown");
            Stations.Add("p", "Purple");
            Stations.Add("pexp", "PurpleExpress");
            Stations.Add("y", "Yellow");
            Stations.Add("pnk", "Pink");
            Stations.Add("o", "Orange");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // looping through each key, using they key as parameter
            foreach(KeyValuePair<string, string> entry in Stations){
                urlParameters = $"?{entry.Key}=true";
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if(response.IsSuccessStatusCode){
                    System.Console.WriteLine("Yay!");
                    // read the response as a string
                    var dataObjects = response.Content.ReadAsStringAsync().Result;
                    System.Console.WriteLine(dataObjects);
                    System.Console.WriteLine("******************************************************************************");
                    // convert the response to a json object
                    dynamic json2 = JsonConvert.DeserializeObject(dataObjects);
                    // go through each value in the call
                    
                    foreach(var x in json2){
                        TrainStation NewStation = new TrainStation();
                        NewStation.Latitude = x.location.coordinates[1];
                        NewStation.Longitude = x.location.coordinates[0];
                        NewStation.Direction = x.direction_id;
                        NewStation.MapId = x.map_id;
                        NewStation.DescStationName = x.station_descriptive_name;
                        NewStation.StationName = x.station_name;
                        NewStation.StopId = x.stop_id;
                        NewStation.StopName = x.stop_name;
                        NewStation.ada = x.ada;
                        NewStation.Blue = x.blue;
                        NewStation.Brown = x.brn;
                        NewStation.Green = x.g;
                        NewStation.Orange = x.o;
                        NewStation.Purple = x.p;
                        NewStation.PurpleExpress = x.pexp;
                        NewStation.Pink = x.pnk;
                        NewStation.Red = x.red;
                        NewStation.Yellow = x.y;
                        _context.Add(NewStation);
                        _context.SaveChanges();
                        System.Console.WriteLine(x);
                    }
                }
                else{
                    System.Console.WriteLine("Aww");
                }
                
            }
            client.Dispose();
            
            
            
            return View("dashboard");
        }

        // [HttpGet("test")]
        // public IActionResult Test(){
        //     string URL = "https://data.cityofchicago.org/resource/8mj8-j3c4.json";
        //     string urlParameters;
        //     HttpClient client = new HttpClient();
        //     client.BaseAddress = new Uri(URL);
        //     client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        // }
    }
}


//olx_GzafvOnx@{^rb@kMrXz@rg@Snd@{JzOjvLja@ocMzc@oFvGrmLjk@koM~RwVzm@_Srl@_]fm@{JfTSbe@b}a@jC__b@?vdf@nFszHzYwi\z@j}n@fYs~n@bQ~~RjH__SnFftI~HsvERz|@R~a@Rzm@?~k@rI__IrNvkBz@vkLvj@klLjC_|BrDjoMja@owM?jxKnAcuGrSwcARrl@vVklBrIvoJbLkpJrS?vQ~wIv[ozI~R~qGbV{sGnKnxEja@wmFz@{|@jCbmFfc@o~GbBjzEfh@g~D??Rco@vBvgDnZ_iDbBnyBvLstA~HbvXRbt@RkvB?jrDf@b~@f@vy@z@slJ~CgfU?bcA?ovA?jRRbdCf@nhMR_lJnAjtHRkiPbB~hNRo}@fEo`L?_SbBnn@nKce@?rInFgT~Cbo@bBce@?rIbG{ERzTrDvlDf@~xARwfGRby@?ju@?zpFnFrdX?svTz@siNf@vdRz@b{NvGg_BbB_aH?cxM~Cr}SfTwd\?_Djz@rhGfTbj@z@zw@Rr{@RfaAf@vy@?rrBRgw@bBcfQRw[bGjmX?gfAf^o~Qnn@njBzm@{uFbB~\fJnwHbo@_qJn_@_Nve@jiAzEsjAj\nvKz@n~BbL{wOjMnuSnZcfQf@soAvt@njVjf@_xXf@~z@nFzlH?oyBr`AsrBnoBwBv|AoAfpAoA