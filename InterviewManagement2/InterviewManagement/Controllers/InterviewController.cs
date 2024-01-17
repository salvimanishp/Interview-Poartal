using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InterviewManagement.Models;

namespace InterviewManagement.Controllers
{
	public class InterviewController : Controller
	{
		// show Data
		public ActionResult CandidateList()
		{
			InterviewMethod db = new InterviewMethod();
			List<InterviewMaster> obj = db.GetAllCandidate();
			return View(obj);
		}

		// GET: Interview
		public ActionResult AddCandidate(int? id)
		{
			// If candidateId is provided, fetch existing data and load the update view
			if (id.HasValue)
			{
				InterviewMethod IM = new InterviewMethod();
				//InterviewMaster existingCandidate = IM.GetCandidateById(candidateId.Value);

				var existingCandidate = IM.GetAllCandidate().FirstOrDefault(model => model.CandidateId == id);
				if (existingCandidate != null)
				{
					// Populate the view model with existing data for update
					return View(existingCandidate);
				}
				else
				{
					// Handle the case where the candidate with the provided ID doesn't exist
					return HttpNotFound();
				}
			}

			// If candidateId is not provided, load the insert view
			return View();
		}

		[HttpPost]
		public ActionResult AddCandidate(InterviewMaster Inter)
		{
			ResponseBase response = new ResponseBase();
			try
			{
				//if (ModelState.IsValid)
				//{
				InterviewMethod IM = new InterviewMethod();

				if (Inter.CandidateId > 0)
				{
					// Update existing candidate
					bool check = IM.AddOrUpdateCandidate(Inter, out string errorMessage);
					if (check)
					{
						TempData["interviewerror"] = "Candidate Data updated successfully.";
						//return RedirectToAction("CandidateList");
					}
					else
					{
						response.IsSuccess = false;
						response.ErrorDescription = errorMessage;
					}
				}
				else
				{
					// Insert new candidate
					bool check = IM.AddOrUpdateCandidate(Inter, out string errorMessage);
					if (check)
					{
						TempData["interviewerror"] = "New Candidate added successfully.";
						//return RedirectToAction("CandidateList");
					}
					else
					{
						response.IsSuccess = false;
						response.ErrorDescription = errorMessage;
					}
				}

			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.ErrorDescription = ex.Message;
			}
			// Return the ErrorDescription property as a JSON response
			return Json(new { errorDescription = response.ErrorDescription });
		}


		// Delete Candidate
		public ActionResult CandidateDelete(int id)
		{

			try
			{
				InterviewMethod context = new InterviewMethod();

				bool check = context.DeleteCandidate(id);

				if (check)
				{
					TempData["interviewerror"] = "Data has been deleted successfully";
					return RedirectToAction("CandidateList");
				}
				else
				{
					TempData["interviewerror"] = "Failed to delete data";
					return RedirectToAction("CandidateList");
				}
			}
			catch (Exception ex)
			{
				// Handle the exception (log it, display a user-friendly message, etc.)
				TempData["deleteMessage"] = "An error occurred while deleting data. Please try again later.";
				return RedirectToAction("CandidateList");
			}
		}


	}
}
