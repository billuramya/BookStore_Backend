using CommanLayer.Model;
using ManagerLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Service
{
    public class ReviewManager : IReviewManager
    {
        private readonly IReviewRepo reviewRepo;
        public ReviewManager(IReviewRepo reviewRepo)
        {
            this.reviewRepo = reviewRepo;   
        }
        public ReviewModel AddReview(ReviewModel model)
        {
            return reviewRepo.AddReview(model);
        }
        public List<ReviewList> GetReviews(int bookid)
        {
            return reviewRepo.GetReviews(bookid);
        }
    }
}
