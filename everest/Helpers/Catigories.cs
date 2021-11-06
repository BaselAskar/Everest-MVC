using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everest.Helpers
{
    public class Catigories
    {
        public class Catigory
        {
            public string CatigoryName { get; set; }
            public List<string> Sections { get; set; }

        }

        public static List<Catigory> CatigoriesList => new List<Catigory>
        {
            new Catigory
            {
                CatigoryName = "ملبوسات",
                Sections = new List<string>
                {
                    "رجالي","نسائي","ولادي","حجابات","معاطف","أحذية","حقائب","بالات",
                }
            },
            new Catigory
            {
                CatigoryName = "طعام",
                Sections = new List<string>
                {
                    "مطاعم","بيع لحوم دجاج","بيع لحوم خروف","بيع لحوم عجل","حلويات","موالح","بوظة وعصائر","بهارات",
                }
            },
            new Catigory
            {
                CatigoryName = "ترفيه",
                Sections = new List<string>
                {
                    "مقهى","ملاهي","حدائق","روضات أطفال","صالات أفراح","نوادي رياضية","أماكن ترفيهية",
                }
            },
            new Catigory
            {
                CatigoryName = "الجمال",
                Sections = new List<string>
                {
                    "حلاقة رجالي","كوافيرة نسائي","إكسسوارات","مكياج",
                }
            },
            new Catigory
            {
                CatigoryName = "الصحة",
                Sections = new List<string>
                {
                    "صيدليات","عيادات بشرية","مشافي","تصوير أشعة","مراكز صحية","معالجة فيزيائية","صيدليات بيطرية","عيادات بيطرية",
                }
            },
            new Catigory
            {
                CatigoryName = "تكنولوجيا",
                Sections = new List<string>
                {
                    "بيع وصيانة موبايلات","بيع وصيانة كوبيوتر","صيانة طابعات","مزود خدمة انترنت","بيع وصيانة ساعات","تركيب وصيانة نظم مراقبة",
                }
            },
            new Catigory
            {
                CatigoryName = "أدوات كهربائية",
                Sections = new List<string>
                {
                    "بيع أدوات كهربائية","صيانة برادات وغسالات","بيع وصيانة أجهزة الصوت","تركيب الستلايت وتوابعه","أنظمة الطاقة الشمسية",
                }
            },
            new Catigory
            {
                CatigoryName = "تأجير",
                Sections = new List<string>
                {
                    "تأجير خيم مناسبات","تأجير كراسي","منازل للإيجار","محلات للإيجار",
                }
            },
            new Catigory
            {
                CatigoryName = "أثاث مفروشات",
                Sections = new List<string>
                {
                    "سجاد وموكيت","أقمشة وستائر","إسفنج","أدوات منزلية","أطقم كتب",
                }
            },
            new Catigory
            {
                CatigoryName = "سيارات ومعدات",
                Sections = new List<string>
                {
                    "إصلاح سيارات","بيع سيارات","إطارات سيارات","فانات أجرة","تكسي أجرة","تركس","حفارة","سيارة قلاب",
                }
            },
            new Catigory
            {
                CatigoryName = "صرافة وصاغة",
                Sections = new List<string>
                {
                    "صرافة وحوالات مالية","صاغات ذهب",
                }
            },
            new Catigory
            {
                CatigoryName = "محطات وقود وزيوت",
                Sections = new List<string>
                {
                    "محطات وقود","زيوت سيارات","مركز غاز",
                }
            },
            new Catigory
            {
                CatigoryName = "خردوات وبناء",
                Sections = new List<string>
                {
                    "خردوات","موبيليا","بلاط","سيراميك ورخام","زرينة وإضاءة","خزانات وسخانات","مواد بناء",
                }
            },
            new Catigory
            {
                CatigoryName = "معامل ومشاغل",
                Sections = new List<string>
                {
                    "مشاغل خياظة",
                }
            },
            new Catigory
            {
                CatigoryName = "مكاتب",
                Sections = new List<string>
                {
                    "مهندس معماري","محامي","تعهدات","مكتب عقاري","مكتب شحن بضائع",
                }
            },
            new Catigory
            {
                CatigoryName = "تربية وتعليم",
                Sections = new List<string>
                {
                    "معهد شرعي","دورات تعليمية","دورات لغة",
                }
            },
            new Catigory
            {
                CatigoryName = "ورشات فنية و معمارية",
                Sections = new List<string>
                {
                    "كهربائي","صحية","حداد","موبيليا","نجار باتون","تكييف وتبريد",
                }
            },

        };
    }
}
