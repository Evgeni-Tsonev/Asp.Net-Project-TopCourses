namespace TopCourses.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using TopCourses.Core.Models.Topic;

    public class TopicsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ICollection<AddTopicViewModel> topics)
        {
            topics ??= new List<AddTopicViewModel>();

            return View(topics);
        }
    }
}
