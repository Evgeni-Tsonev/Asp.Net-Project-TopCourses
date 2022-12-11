namespace TopCourses.Core.Services
{
    using System.Threading.Tasks;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Review;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;

    public class ReviewService : IReviewService
    {
        private readonly IDbRepository repository;

        public ReviewService(IDbRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddReview(AddReviewViewModel model)
        {
            var user = await this.repository.GetByIdAsync<ApplicationUser>(model.UserId);
            if (user == null)
            {
                throw new Exception();
            }

            var course = await this.repository.GetByIdAsync<Course>(model.CourseId);
            if (course == null)
            {
                throw new Exception();
            }

            var review = new Review()
            {
                Comment = model.Comment,
                Rating = model.Rating,
                CourseId = model.CourseId,
                UserId = model.UserId,
                DateOfPublication = model.DateOfPublication,
            };

            await this.repository.AddAsync(review);
            await this.repository.SaveChangesAsync();
        }

        //public async Task DeleteReview(int id, string userId)
        //{
        //    var user = await this.repository.GetByIdAsync<ApplicationUser>(userId);
        //    if (user == null)
        //    {
        //        throw new Exception();
        //    }

        //    var review = await this.repository.GetByIdAsync<Review>(id);
        //    if (review == null)
        //    {
        //        throw new Exception();
        //    }

        //    if (review.UserId != user.Id)
        //    {
        //        throw new Exception("Invalid operation");
        //    }

        //    review.IsDeleted = true;
        //    await this.repository.SaveChangesAsync();
        //}

        //public async Task Update(EditReviewViewModel model, string userId)
        //{
        //    var review = await this.repository.GetByIdAsync<Review>(model.Id);
        //    if (review == null)
        //    {
        //        throw new Exception();
        //    }

        //    var user = await this.repository.GetByIdAsync<ApplicationUser>(userId);
        //    if (user == null)
        //    {
        //        throw new Exception();
        //    }

        //    if (model.UserId != user.Id)
        //    {
        //        throw new Exception();
        //    }

        //    review.Rating = model.Rating;
        //    review.Comment = model.Comment;
        //    review.LastUpdate = model.LastUpdate;

        //    await this.repository.SaveChangesAsync();
        //}

        //public async Task<EditReviewViewModel> GetReviewForEdit(int id, string userId)
        //{
        //    var model = await this.repository.GetByIdAsync<Review>(id);
        //    if (model == null)
        //    {
        //        throw new Exception();
        //    }

        //    var user = await this.repository.GetByIdAsync<ApplicationUser>(userId);
        //    if (user == null)
        //    {
        //        throw new Exception();
        //    }

        //    if (model.UserId != user.Id)
        //    {
        //        throw new Exception();
        //    }

        //    return new EditReviewViewModel()
        //    {
        //        Id = model.Id,
        //        Rating = model.Rating,
        //        Comment = model.Comment,
        //        UserId = model.UserId,
        //    };
        //}

        //public double GetAverageRating(int courseId)
        //{
        //    var averageRating = this.repository
        //        .AllReadonly<Review>()
        //        .Where(c => c.CourseId == courseId)
        //        .Average(r => r.Rating);
        //    return averageRating;
        //}
    }
}
