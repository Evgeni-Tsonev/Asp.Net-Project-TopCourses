namespace TopCourses.Controllers
{
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Models.Topic;

    public class TopicController : BaseController
    {
        [HttpPost]
        public IActionResult CreateTopic([FromBody] JsonObject jsonObj)
        {
            var model = JsonSerializer.Deserialize<AddTopicViewModel>(
                                                                    jsonObj,
                                                                    new JsonSerializerOptions
                                                                    {
                                                                        PropertyNameCaseInsensitive = true,
                                                                    });

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var topicsModelListToReturn = new List<AddTopicViewModel>();

            if (!this.TempData.ContainsKey("Curriculum"))
            {
                var topicsList = new List<AddTopicViewModel>();
                topicsList.Add(model);
                this.TempData["Curriculum"] = JsonSerializer.Serialize(topicsList);
                topicsModelListToReturn = topicsList;
            }
            else
            {
                var data = this.TempData["Curriculum"]?.ToString();

                var topics = JsonSerializer.Deserialize<ICollection<AddTopicViewModel>>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                topics.Add(model);

                this.TempData["Curriculum"] = JsonSerializer.Serialize(topics);

                topicsModelListToReturn = topics.ToList();
            }

            return this.PartialView("_TopicPartial", topicsModelListToReturn);
        }
    }
}
