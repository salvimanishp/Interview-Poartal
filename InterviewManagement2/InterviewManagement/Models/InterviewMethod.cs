using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InterviewManagement.Models
{
	public class InterviewMethod
	{

        string cs = ConfigurationManager.ConnectionStrings["InterviewManagemetconnectionString"].ConnectionString;

        // insert Metrhod 
        public List<InterviewMaster> GetAllCandidate()
{
    List<InterviewMaster> lstCandidate = new List<InterviewMaster>();

    using (SqlConnection con = new SqlConnection(cs))
    {
        SqlCommand cmd = new SqlCommand("UspGetCandidateData", con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        con.Open();
        da.Fill(dt);
        con.Close();

        foreach (DataRow row in dt.Rows)
        {
            InterviewMaster IM = new InterviewMaster();
            IM.CompanyId = (long)row["CompanyId"];
            IM.CompanyName = row["CompanyName"].ToString();
            IM.CandidateId = (long)row["CandidateId"];
            IM.FirstName = row["FirstName"].ToString();
            IM.LastName = row["LastName"].ToString();
            IM.Email = row["EmailId"].ToString();
            IM.MobailNumber = row["MobileNo"].ToString();
            IM.Address = row["AddressName"].ToString();
            IM.DOB = (DateTime)row["DateOfBirth"];
            IM.Age = row["Age"].ToString();
            IM.Qualification = row["Qualification"].ToString();
            IM.Skill = row["MasterSkills"].ToString();
            IM.Experience = row["WorkExperience"].ToString();
            IM.City = row["City"].ToString();
            IM.Country = row["Country"].ToString();
            IM.State = row["State"].ToString();

            lstCandidate.Add(IM);
        }
    }
    return lstCandidate;
}

		public bool AddOrUpdateCandidate(InterviewMaster IM, out string errorMessage)
		{
			try
			{
				// Retrieve CompanyId and CompanyName from session
				long CompanyId = Convert.ToInt32(HttpContext.Current.Session["CompanyId"]);
				string CompanyName = HttpContext.Current.Session["CompanyName"].ToString();

				using (SqlConnection con = new SqlConnection(cs))
				using (SqlCommand cmd = new SqlCommand("USpCandidateSave", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@P_CandidateId", IM.CandidateId);
					cmd.Parameters.AddWithValue("@P_CompanyId", CompanyId);
					cmd.Parameters.AddWithValue("@P_CompanyName", CompanyName);
					cmd.Parameters.AddWithValue("@P_FirstName", IM.FirstName);
					cmd.Parameters.AddWithValue("@P_LastName", IM.LastName);
					cmd.Parameters.AddWithValue("@P_MobileNo", IM.MobailNumber);
					cmd.Parameters.AddWithValue("@P_DateOfBirth", IM.DOB);
					cmd.Parameters.AddWithValue("@P_Age", IM.Age);
					cmd.Parameters.AddWithValue("@P_Qualification", IM.Qualification);
					cmd.Parameters.AddWithValue("@P_MasterSkills", IM.Skill);
					cmd.Parameters.AddWithValue("@P_WorkExperience", IM.Experience);
					cmd.Parameters.AddWithValue("@P_EmailId", IM.Email);
					cmd.Parameters.AddWithValue("@P_AddressName", IM.Address);
					cmd.Parameters.AddWithValue("@P_Country", IM.Country);
					cmd.Parameters.AddWithValue("@P_State", IM.State);
					cmd.Parameters.AddWithValue("@P_City", IM.City);

					// Add output parameter for the error message
					SqlParameter errorMessageParam = new SqlParameter("@P_ErrorMessage", SqlDbType.VarChar, 500);
					errorMessageParam.Direction = ParameterDirection.Output;
					cmd.Parameters.Add(errorMessageParam);

					con.Open();
					int i= cmd.ExecuteNonQuery();

					// Retrieve the error message from the output parameter
					errorMessage = cmd.Parameters["@P_ErrorMessage"].Value.ToString();

                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
			}
			catch (SqlException ex)
			{
				// Log the exception details for debugging
				errorMessage = "SQL Error occurred: " + ex.Message;
				return false;
			}
			catch (Exception ex)
			{
				// Handle other exceptions
				errorMessage = "An error occurred: " + ex.Message;
				return false;
			}
		}

		

        //DeleteCandidate
        public bool DeleteCandidate(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd = new SqlCommand("UspSoftDeleteCandidate", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@P_CandidateId", id);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();

                        return i > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log it, throw it, etc.)
                Console.WriteLine("An error occurred while deleting an employee: " + ex.Message);
                return false;
            }
        }


    }
}