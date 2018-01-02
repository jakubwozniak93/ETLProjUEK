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

        internal void ClearAll()
        {
            DependencyService.Get<ISqliteConnection>().DropDatabase();
            Init();
        }

        internal async Task<IList<CategoryDataModels>> GetExamplesCategory()
        {
            List<CategoryDataModels> list = await db.Table<CategoryDataModels>().ToListAsync();
            return list;
        }

        internal async Task<CategoryDataModels> GetCategory(int id)
        {
            CategoryDataModels c = await db.Table<CategoryDataModels>().Where(x => x.CategoryId == id).FirstOrDefaultAsync();
            return c;
        }

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

        internal async Task<ProductDataModels> GetProduct(int id)
        {
            ProductDataModels c = await db.Table<ProductDataModels>().Where(x => x.ID == id).FirstOrDefaultAsync();
            return c;
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
                pr.ProductKey = product.ProductKey;
                int res = await db.UpdateAsync(pr);
                return res != -1;
            }
        }

        internal async Task<IList<ReviewDetailsDataModel>> GetExamplesReviews()
        {
            List<ReviewDetailsDataModel> list = await db.Table<ReviewDetailsDataModel>().ToListAsync();
            return list;
        }

        internal async Task<ReviewDetailsDataModel> GetReviews(int id)
        {
            ReviewDetailsDataModel c = await db.Table<ReviewDetailsDataModel>().Where(x => x.ID == id).FirstOrDefaultAsync();
            return c;
        }

        internal async Task<bool> SaveReviewDetails(ReviewDetailsDataModel reviewDetail)
        {
            List<ReviewDetailsDataModel> reviews = await db.Table<ReviewDetailsDataModel>().Where(x => x.ID == reviewDetail.ID).ToListAsync();

            if (reviews.Count > 1)
            {
                throw new Exception("Duplicated");
            }

            if (reviews.Count == 0)
            {
                int res = await db.InsertAsync(reviewDetail);
                return res != -1;
            }
            else
            {
                ReviewDetailsDataModel rd = reviews[0];
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
