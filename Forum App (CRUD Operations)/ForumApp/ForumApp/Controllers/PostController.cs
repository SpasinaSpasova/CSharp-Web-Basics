﻿using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Controllers
{
    public class PostController : Controller
    {
        private readonly ForumDbContext context;
        public PostController(ForumDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await context.Posts
                .Where(p => p.IsDeleted == false)
                .Select(p => new PostViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                }).ToListAsync();

            return View(model);
        }

        /// <summary>
        /// Load add/ edit view
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public  IActionResult Add()
        //{
        //    var model = new PostViewModel();

        //    ViewData["Title"] = "Add new post";

        //    return View("Edit", model);
        //}

        ///// <summary>
        ///// Add or Edit 
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> Edit(PostViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewData["Title"] = model.Id == 0 ? "Add new Post" : "Edit post";
        //        return View(model);
        //    }

        //    if (model.Id == 0)
        //    {
        //        context.Posts.Add(new Post()
        //        {
        //            Title = model.Title,
        //            Content = model.Content
        //        });
        //    }
        //    else
        //    {
        //        var post = await context.Posts.FindAsync(model.Id);
        //        if (post != null)
        //        {
        //            post.Title = model.Title;
        //            post.Content = model.Content;
        //        }
        //    }

        //    await context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}


        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddPostViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            context.Posts.Add(new Post()
            {
                Title = model.Title,
                Content = model.Content
            });

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await context.Posts.
                Where(p => p.Id == id)
                .Select(p => new PostViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                }).FirstOrDefaultAsync();

            if (post != null)
            {
                return View(post);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var post = await context.Posts.FindAsync(model.Id);
            if (post != null)
            {
                post.Title = model.Title;
                post.Content = model.Content;
            }

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var post = await context.Posts.FindAsync(id);

            if (post != null)
            {
                post.IsDeleted = true;
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
