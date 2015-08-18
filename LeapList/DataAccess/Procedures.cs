﻿using LeapList.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LeapList.DataAccess
{
    public class Procedures
    {
        public static List<SearchVM> GetSearchVMByProfileId(int profileId)
        {
            List<SearchVM> vm = new List<SearchVM>();
            using (var data = new DataAccess())
            {
                data.ProcedureName = "uspGetSearchVMByProfileId";
                data.AddParm("@ProfileId", SqlDbType.Int, profileId);

                DataTable results = data.ExecReturnDataTable();

                foreach (DataRow row in results.Rows)
                {
                    SearchVM s = new SearchVM
                    {
                        SearchId = Convert.ToInt32(row["SearchId"]),
                        SearchText = row["SearchText"].ToString(),
                        MinPrice = (!(row["MinPrice"] is DBNull) ? Convert.ToDecimal(row["MinPrice"]) : 0m),
                        MaxPrice = (!(row["MaxPrice"] is DBNull) ? Convert.ToDecimal(row["MaxPrice"]) : 0m)
                    };

                    if (s.Category != null)
                    {
                        s.Category.Add(row["Category"].ToString());
                    }
                    vm.Add(s);
                }
            }

            return vm;
        }
    }
}