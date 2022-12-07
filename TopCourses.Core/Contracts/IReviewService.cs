namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.Review;

    public interface IReviewService
    {
        Task AddReview(AddReviewViewModel model);

        //Task DeleteReview(int id, string userId);

        //Task<EditReviewViewModel> GetReviewForEdit(int id, string userId);

        //Task Update(EditReviewViewModel model, string userId);

        //double GetAverageRating(int courseId);
    }
}
