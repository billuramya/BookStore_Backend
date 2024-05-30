using CommanLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IReviewManager
    {
        public ReviewModel AddReview(ReviewModel model);
        public List<ReviewList> GetReviews(int bookid);
    }
}
