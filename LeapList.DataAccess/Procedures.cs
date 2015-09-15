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
            List<KeyValuePair<int, string>> categories = new List<KeyValuePair<int, string>>();

            using (var data = new DataAccess())
            {
                data.ProcedureName = "uspGetAddEditSearchVMByProfileId";
                data.AddParm("@ProfileId", SqlDbType.Int, profileId);

                DataTable results = data.ExecReturnDataTable();

                foreach (DataRow row in results.Rows)
                {
                    AddEditSearchVM search = new AddEditSearchVM
                    {
                        SearchId = Convert.ToInt32(row["SearchId"]),
                        SearchText = row["SearchText"].ToString(),
                        MinPrice = (!(row["MinPrice"] is DBNull) ? Convert.ToDecimal(row["MinPrice"]) : 0m),
                        MaxPrice = (!(row["MaxPrice"] is DBNull) ? Convert.ToDecimal(row["MaxPrice"]) : 0m)
                    };

                    addEditSearchVMs.Add(search);
                    
                    // Getting the categories for each search
                    int idx = Convert.ToInt32(row["SearchId"]);
                    if (row["Category"].ToString() != null)
                    {
                        categories.Add(new KeyValuePair<int, string>(idx, row["Category"].ToString()));
                    }
                }
            }

            foreach (AddEditSearchVM s in addEditSearchVMs)
            {
                foreach (KeyValuePair<int, string> catBySearchId in categories)
                {
                    if (s.SearchId == catBySearchId.Key)
                    {
                        CheckBoxCategoryVM checkBox = new CheckBoxCategoryVM()
                        {
                            IsChecked = true,
                            Name = DictCategory.GetCategoryName(catBySearchId.Value),
                            Code = catBySearchId.Value
                        };
                    }
                }
            }

            return addEditSearchVMs;
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