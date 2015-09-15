using LeapList.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LeapList.DataAccess
{
    public class Procedures
    {
        public static List<AddEditSearchVM> GetAddEditSearchVMByProfileId(int profileId)
        {
            List<AddEditSearchVM> addEditSearchVMs = new List<AddEditSearchVM>();
            List<int> uniqueSearches = new List<int>();

            using (var data = new DataAccess())
            {
                data.ProcedureName = "uspGetAddEditSearchVMByProfileId";
                data.AddParm("@ProfileId", SqlDbType.Int, profileId);

                DataTable results = data.ExecReturnDataTable();

                CheckBoxCategoryVM category = new CheckBoxCategoryVM();
                foreach (DataRow row in results.Rows)
                {
                    int searchId = Convert.ToInt32(row["SearchId"]);
                    if (!uniqueSearches.Contains(searchId))
                    {
                        uniqueSearches.Add(searchId);
                        AddEditSearchVM search = new AddEditSearchVM
                        {
                            SearchId = Convert.ToInt32(row["SearchId"]),
                            SearchText = row["SearchText"].ToString(),
                            MinPrice = (!(row["MinPrice"] is DBNull) ? Convert.ToDecimal(row["MinPrice"]) : 0m),
                            MaxPrice = (!(row["MaxPrice"] is DBNull) ? Convert.ToDecimal(row["MaxPrice"]) : 0m)
                        };

                        category = new CheckBoxCategoryVM()
                            {
                                Code = row["Category"].ToString(),
                                Name = DictCategory.GetCategoryName(row["Category"].ToString()),
                                IsChecked = true
                            };

                        search.Categories.Add(category);
                        addEditSearchVMs.Add(search);
                    }
                    else
                    {
                        addEditSearchVMs.Find(x => x.SearchId == searchId).Categories.Add(new CheckBoxCategoryVM()
                            {
                                Code = row["Category"].ToString(),
                                Name = DictCategory.GetCategoryName(row["Category"].ToString()),
                                IsChecked = true
                            });
                    }
                }
            }

            return addEditSearchVMs;
        }

        public static AddEditSearchVM GetAddEditSearchVMBySearchId(int searchId)
        {
            AddEditSearchVM searchVM = new AddEditSearchVM();
            using (var data = new DataAccess())
            {
                data.ProcedureName = "uspGetAddEditSearchVMBySearchId";
                data.AddParm("@searchId", SqlDbType.Int, searchId);

                DataTable results = data.ExecReturnDataTable();

                DataRow firstRow = results.Rows[0];

                searchVM.SearchId = Convert.ToInt32(firstRow["SearchId"]);
                searchVM.SearchText = firstRow["SearchText"].ToString();
                searchVM.MinPrice = (!(firstRow["MinPrice"] is DBNull) ? Convert.ToDecimal(firstRow["MinPrice"]) : 0m);
                searchVM.MaxPrice = (!(firstRow["MaxPrice"] is DBNull) ? Convert.ToDecimal(firstRow["MaxPrice"]) : 0m);

                CheckBoxCategoryVM category = new CheckBoxCategoryVM();
                foreach (DataRow row in results.Rows)
                {   
                    category = new CheckBoxCategoryVM()
                        {
                            Code = row["Category"].ToString(),
                            Name = DictCategory.GetCategoryName(row["Category"].ToString()),
                            IsChecked = true
                        };

                    searchVM.Categories.Add(category);
                }           
            }

            return searchVM;
        }

        public static UsernamePassword GetUsernameAndPasswordHash(string usernameEntered)
        {
            using (var data = new DataAccess())
            {
                data.ProcedureName = "uspGetUsernameAndPasswordHash";
                data.AddParm("@username", SqlDbType.VarChar, usernameEntered);

                DataTable results = data.ExecReturnDataTable();
                if (results == null)
                {
                    throw new Exception(string.Format("No profile found for user {0}.", usernameEntered));
                }
                if (results.Rows.Count > 1)
                {
                    throw new Exception(string.Format("Duplicate profile found for user {0}.", usernameEntered));
                }


                UsernamePassword user = new UsernamePassword();
                foreach (DataRow row in results.Rows)
                {
                    user.Username = row["Username"].ToString();
                    user.PasswordHash = row["PasswordHash"].ToString();
                }

                return user;
            }
        }

        public static bool CheckIfUserExists(string username)
        {
            using (var data = new DataAccess())
            {
                data.ProcedureName = "uspCheckIfUserExists";
                data.AddParm("@username", SqlDbType.VarChar, username);

                DataTable results = data.ExecReturnDataTable();
                foreach (DataRow row in results.Rows)
                {
                    if (row["UserCheck"].Equals((int)0))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}