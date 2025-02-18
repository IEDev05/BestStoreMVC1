using BestStoreMVC.Models;
using BestStoreMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BestStoreMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductcRepository _productcRepository;

        public ProductController(ProductcRepository productcRepository)
        {
            this._productcRepository = productcRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _productcRepository.GetAllProduct();
                return View(products);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        //[HttpGet]
        //public IActionResult Insert()
        //{
        //    // Your code to handle the GET request without this function u cant do insert operation
        //    return View();
        //}
        public ViewResult Insert() => View(); //go to insert page to use this line 

        [HttpPost]
        public async Task<IActionResult> Insert(ProductModel product)
        {
            try
            {
                bool isInserted = await _productcRepository.InsertProductAsync(product);
                if (isInserted)
                {
                    return RedirectToAction("Index"); // Redirect to product list after successful insert
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to insert product.";
                    return View(product); // Stay on form if insertion fails
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View(new ProductModel()); // Show an empty form to enter details manually
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductModel product)
        {
            try
            {
                bool isUpdated = await _productcRepository.UpdateProductAsync(product);
                if (isUpdated)
                {
                    return RedirectToAction("Index"); // Redirect to the product list after a successful update
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to update product.";
                    return View(product); // Stay on the form if update fails
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
                return View(product);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Fetch the product details using the repository method
                var product = await _productcRepository.GetProductByIdAsync(id);

                if (product == null)
                {
                    // If no product found, return not found or an error page
                    return NotFound();
                }

                // Return the product to the Edit view
                return View(product);
            }
            catch (Exception ex)
            {
                // Handle exception and return error message
                ViewBag.ErrorMessage = "An error occurred while retrieving the product details.";
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _productcRepository.DeleteProductAsync(id);

            if (isDeleted)
            {
                TempData["SuccessMessage"] = "Product deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete product.";
            }

            return RedirectToAction("Index"); // Redirect back to product list
        }


    }
}
