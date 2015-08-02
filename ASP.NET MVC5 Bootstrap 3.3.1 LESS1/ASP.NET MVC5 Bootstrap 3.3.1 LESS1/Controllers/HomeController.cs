using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC5_Bootstrap3_3_1_LESS.Models;

namespace ASP.NET_MVC5_Bootstrap3_3_1_LESS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(TextAnalysis model = null)
        {
            if (model != null)
            {
                model = new TextAnalysis();
                model.Text = "А й правда, крилатим ґрунту не треба.\n" +
                            "Землі немає, то буде небо.\n\r" +

                            "Немає поля, то буде воля.\n" +
                            "Немає пари, то будуть хмари. \n\r" +

                            "В цьому, напевно, правда пташина...\n" +
                            "А як же людина? А що ж людина? \n\r" +

                            "Живе на землі. Сама не літає.\n" +
                            "А крила має. А крила має! \n\r" +

                            "Вони, ті крила, не з пуху-пір*я,\n" +
                            "А з правди, чесноти і довір*я. \n\r" +

                            "У кого - з вірності у коханні.\n" +
                            "У кого - з вічного поривання. \n\r" +

                            "У кого - з щирості до роботи.\n" +
                            "У кого - з щедрості на турботи. \n\r" +

                            "У кого - з пісні, або з надії,\n" +
                            "Або з поезії, або з мрії. \n\r" +

                            "Людина нібито не літає...\n" +
                            "А крила має. А крила має!";
            }

            return View(model);
        }
        public ActionResult Analysis(TextAnalysis model)
        {
            var txt = model.Text;
            txt = Regex.Replace(txt, @"[^\w\s]", string.Empty);
            txt = txt.Replace("\r\n", string.Empty);
            var words = txt.Split();
            var dictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (dictionary.ContainsKey(word))
                {
                    dictionary[word] = dictionary[word] + 1;
                }
                else
                {
                    dictionary.Add(word, 1);
                }
            }
            model.Counts = dictionary;
            return View("Index", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}