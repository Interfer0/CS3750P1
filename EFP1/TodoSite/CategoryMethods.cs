using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF;

namespace TodoSite.Views.Categories
{
    public static class CategoryMethods
    {
        public static String getCount(Category cat)
        {
            Project1TodoEntities db = new Project1TodoEntities();
            return db.CategoryTodoLists
                .Where(o => o.categoryid == cat.categoryid)
                .Count().ToString();
        }
    }
}