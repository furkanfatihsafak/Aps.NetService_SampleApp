using CraSampleApp.Domain.Domain.Data.Model;
using CraSampleApp.Domain.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CraSampleApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMovieTypeService _movieTypeService;
        public MovieController(IMovieService movieService, IMovieTypeService movieTypeService)
        {
            _movieService = movieService;
            _movieTypeService = movieTypeService;
        }


        public ActionResult Index()
        {
            var list = _movieService.GetList();
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MovieTypes = _movieTypeService.GetAll().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(MovieManagement model)
        {
            var validator = new MovieValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                ViewBag.MovieTypes = _movieTypeService.GetAll().Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();
                ViewBag.Error = result.Errors.Select(d => d.ErrorMessage).Aggregate((x, y) => x + "," + y);

                return View();
            }
            _movieService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            ViewBag.MovieTypes = _movieTypeService.GetAll().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
            
            var model = _movieService.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MovieManagement model)
        {
            var validator = new MovieValidator();
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
            {
                ModelState.AddModelError("Error", validationResult.Errors.Select(d => d.ErrorMessage).Aggregate((x, y) => x + "," + y));
                return RedirectToAction("Create");
            }
            var result = _movieService.Edit(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var result = _movieService.Delete(id);
            return RedirectToAction("Index");

        }

    }
}