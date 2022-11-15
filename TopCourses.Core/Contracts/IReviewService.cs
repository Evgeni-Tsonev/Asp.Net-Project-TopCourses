namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.Review;

    public interface IReviewService
    {
        Task AddReview(AddReviewViewModel model);

        Task DeleteReview(int id);

        Task<EditReviewViewModel> GetReviewForEdit(int id);

        Task Update(EditReviewViewModel model);
    }
}
