using Kalendarzyk.Models;
using Microsoft.Extensions.Logging;

namespace Kalendarzyk.Serializer
{
    public static class JSONhelper
    {
        public static string GetEventsJson(List<EventModel> events)
        {
            var myeventlist = new List<Event>();

                foreach (var model in events)
                {

                    var myevent = new Event()
                    {
                        id = model.Id,
                        start = model.StartTime,
                        end = model.EndTime,
                        resourceId = model.Location.Id,
                        description = model.Description,
                        title = model.Name
                    };
                    myeventlist.Add(myevent);
                }
            return System.Text.Json.JsonSerializer.Serialize(myeventlist);

           
        }
        public static string GetResourceJson(List<LocationModel> locations)
        {
            var mylocationlist = new List<Resource>();

            foreach (var model in locations)
            {
                var mylocation = new Resource()
                {
                    id = model.Id,
                    title = model.Name
                    
                };
                mylocationlist.Add(mylocation);
            }
            return System.Text.Json.JsonSerializer.Serialize(mylocationlist);
        }
    }
    public class Event
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int resourceId { get; set; }
        public string? description { get; set; } 
    }
    public class Resource
    {
        public int id { get; set; }
        public string title { get; set; }
    }
}
