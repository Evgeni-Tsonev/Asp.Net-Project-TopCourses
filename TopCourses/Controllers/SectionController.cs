namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using TopCourses.Core.Models.Topic;

    public class SectionController : BaseController
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

        [HttpPost]
        public IActionResult SectionTest([FromBody] JsonObject jsonObj)
        {
            var model = JsonSerializer.Deserialize<AddTopicViewModel>(jsonObj,
                                                                    new JsonSerializerOptions
                                                                    {
                                                                        PropertyNameCaseInsensitive = true
                                                                    });

            if (!TempData.ContainsKey("Curriculum"))
            {
                var sectionList = new List<AddTopicViewModel>();
                sectionList.Add(model);
                TempData["Curriculum"] = JsonSerializer.Serialize(sectionList);
            }
            else
            {
                var data = TempData["Curriculum"]?.ToString();
                var sections = JsonSerializer.Deserialize<ICollection<AddTopicViewModel>>(data, new JsonSerializerOptions
                                                                                 {
                                                                                     PropertyNameCaseInsensitive = true
                                                                                 });
                sections.Add(model);
                TempData["Curriculum"] = JsonSerializer.Serialize(sections);
            }

            return Ok();
        }
    }
}
