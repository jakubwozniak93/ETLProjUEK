using MobiParse.Models;
using MobiParse.ViewModel;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobiParse.IO.Sqlite
{
    public class LocalDB
    {
        private SQLiteAsyncConnection db { get; set; }
        public LocalDB(string dbPath)
        {
            Init();
        }

        private async void Init()
        {
            db = DependencyService.Get<ISqliteConnection>().GetConnection();

            CreateTablesResult created = await db.CreateTableAsync<CategoryDataModels>();
            created = await db.CreateTableAsync<ProductDataModels>();
            created = await db.CreateTableAsync<ReviewDetailsDataModel>();

        }

        /// <summary>
        /// Method to whipe whole DataBase.
        /// </summary>
        internal void ClearAll()
        {
            DependencyService.Get<ISqliteConnection>().DropDatabase();
            Init();
        }

        /// <summary>
        /// Gets all data from Category Table.
        /// </summary>
        /// <returns>Returns list of category data.</returns>
        internal async Task<IList<CategoryDataModels>> GetExamplesCategory()
        {
            List<CategoryDataModels> list = await db.Table<CategoryDataModels>().ToListAsync();
            return list;
        }

        /// <summary>
        /// Gets a specific category data.
        /// </summary>
        /// <returns>specific category data.</returns>
        /// <param name="id">Identifier.</param>
        internal async Task<CategoryDataModels> GetCategory(string categoryName)
        {
            CategoryDataModels c = await db.Table<CategoryDataModels>().Where(x => x.CategoryName == categoryName).FirstOrDefaultAsync();
            return c;
        }

        /// <summary>
        /// Saves the category.
        /// </summary>
        /// <returns>The category.</returns>
        /// <param name="category">Category.</param>
        internal async Task<bool> SaveCategory(CategoryDataModels category)
        {
            List<CategoryDataModels> categories = await db.Table<CategoryDataModels>().Where(x => x.CategoryName == category.CategoryName).ToListAsync();

            if (categories.Count > 1)
            {
                throw new Exception("Duplicated");
            }

            if (categories.Count == 0)
            {
                int res = await db.InsertAsync(category);
                return res != -1;
            }
            else
            {
                CategoryDataModels cr = categories[0];
                cr.CategoryName = category.CategoryName;
                int res = await db.UpdateAsync(cr);
                return res != -1;
            }
        }


        internal async Task<IList<ProductDataModels>> GetExamplesProduct()
        {
            List<ProductDataModels> list = await db.Table<ProductDataModels>().ToListAsync();
            return list;
        }

        internal async Task<ProductDataModels> GetProductById(int id)
        {
            ProductDataModels c = await db.Table<ProductDataModels>().Where(x => x.ID == id).FirstOrDefaultAsync();
            return c;
        }

        internal async Task<IList<ProductDataModels>> GetProductByCategoryName(string categoryName)
        {
            List<ProductDataModels> pn = await db.Table<ProductDataModels>().Where(x => x.CategoryName == categoryName).ToListAsync();
            return pn;
        }

        internal async Task<bool> SaveProductId(ProductDataModels product)
        {
            List<ProductDataModels> products = await db.Table<ProductDataModels>().Where(x => x.ProductKey == product.ProductKey).ToListAsync();

            if (products.Count > 1)
            {
                throw new Exception("Duplicated");
            }

            if (products.Count == 0)
            {
                int res = await db.InsertAsync(product);
                return res != -1;
            }
            else
            {
                ProductDataModels pr = products[0];
                pr.CategoryID = product.CategoryID;
                pr.ProductKey = product.ProductKey;
                pr.ProductName = product.ProductName;
                int res = await db.UpdateAsync(pr);
                return res != -1;
            }
        }

        internal async Task<IList<ReviewDetailsDataModel>> GetExampleReviewsDetails()
        {
            List<ReviewDetailsDataModel> list = await db.Table<ReviewDetailsDataModel>().ToListAsync();
            return list;
        }

        internal async Task<ReviewDetailsDataModel> GetReviewDetail(int id)
        {
            ReviewDetailsDataModel c = await db.Table<ReviewDetailsDataModel>().Where(x => x.ID == id).FirstOrDefaultAsync();
            return c;
        }

        internal async Task<IList<ReviewDetailsDataModel>> GetReviewDetailByProductKey(string productKey)
        {
            List<ReviewDetailsDataModel> pk = await db.Table<ReviewDetailsDataModel>().Where(x => x.ProductKey == productKey).ToListAsync();
            return pk;
        }

        internal async Task<bool> SaveReviewDetails(ReviewDetailsDataModel reviewDetail)
        {
            List<ReviewDetailsDataModel> reviewsDetails = await db.Table<ReviewDetailsDataModel>().Where(x => x.ReviewID == reviewDetail.ReviewID).ToListAsync();

            if (reviewsDetails.Count > 1)
            {
                throw new Exception("Duplicated");
            }

            if (reviewsDetails.Count == 0)
            {
                int res = await db.InsertAsync(reviewDetail);
                return res != -1;
            }
            else
            {
                ReviewDetailsDataModel rd = reviewsDetails[0];
                rd.CategoryName = reviewDetail.CategoryName;
                rd.ProductName = reviewDetail.ProductName;
                rd.ProductKey = reviewDetail.ProductKey;
                rd.UserName = reviewDetail.UserName;
                rd.ReviewStatus = reviewDetail.ReviewStatus;
                rd.ScoreValue = reviewDetail.ScoreValue;
                rd.DateTime = reviewDetail.DateTime;
                rd.ReviewText = reviewDetail.ReviewText;
                rd.ReviewUseful = reviewDetail.ReviewUseful;
                rd.ReviewUnuseful = reviewDetail.ReviewUnuseful;
                rd.ProductPros = reviewDetail.ProductPros;
                rd.ProductCons = reviewDetail.ProductCons;
                rd.ReviewsCount = reviewDetail.ReviewsCount;
                rd.IsVisible = reviewDetail.IsVisible;
                int res = await db.UpdateAsync(rd);
                return res != -1;
            }
        }

        internal async Task<bool> DeleteReviewAsync(string reviewID)
        {
            ReviewDetailsDataModel delRev = await db.Table<ReviewDetailsDataModel>().Where(x => x.ReviewID == reviewID).FirstOrDefaultAsync();

            int res = await db.DeleteAsync(delRev);
            return res != -1;
        }

        internal async Task<bool> UpdateCategoryAsync(CategoryDataModels example)
        {
            int res = await db.UpdateAsync(example);
            return res != -1;
        }

        internal async Task<bool> DeleteCategoryAsync(CategoryDataModels example)
        {
            int res = await db.DeleteAsync(example);
            return res != -1;
        }
    }
}
