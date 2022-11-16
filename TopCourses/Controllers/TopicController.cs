namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Text.Json.Nodes;
    using System.Text.Json;
    using TopCourses.Core.Models.Topic;
    using System.ComponentModel;

    public class TopicController : BaseController
    {
        public IActionResult Create()
        {
            var model = new AddTopicViewModel();

            return View(model);
        }

        public IActionResult Test()
        {
            var model = new AddTopicViewModel();
            return View(model);
        }

        //[ChildActionOnly]
        //public System.Web.Mvc.PartialViewResult GetMenu()
        //{

        //    return PartialView("~/Views/Shared/MenuPartial", "model");
        //}

        [HttpPost]
        public IActionResult SectionTest([FromBody] JsonObject jsonObj)
        {
            var model = JsonSerializer.Deserialize<AddTopicViewModel>(jsonObj,
                                                                    new JsonSerializerOptions
                                                                    {
                                                                        PropertyNameCaseInsensitive = true
                                                                    });

            var topicsModelListToReturn = new List<AddTopicViewModel>();

            if (!TempData.ContainsKey("Curriculum"))
            {
                var topicsList = new List<AddTopicViewModel>();
                topicsList.Add(model);
                TempData["Curriculum"] = JsonSerializer.Serialize(topicsList);
                topicsModelListToReturn = topicsList;
            }
            else
            {
                var data = TempData["Curriculum"]?.ToString();

                var topics = JsonSerializer.Deserialize<ICollection<AddTopicViewModel>>(data, new JsonSerializerOptions
                                                                                        {
                                                                                            PropertyNameCaseInsensitive = true
                                                                                        });

                topics.Add(model);

                TempData["Curriculum"] = JsonSerializer.Serialize(topics);

                topicsModelListToReturn = topics.ToList();
            }

            return ViewComponent("Topics", topicsModelListToReturn);
        }
    }
}
