using MyStore.Mongo.Repository;
using MyStore.MongoDB;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyStoreApp.Controllers
{
    public class ProductMongoController : ApiController
    {
        private readonly IProductMongoRepository _productMongoRepository;

        public ProductMongoController(IProductMongoRepository mongoRepository)
        {
            _productMongoRepository = mongoRepository;
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        // GET: api/ProductMongo
        public async Task<IHttpActionResult> GetProducts()
        {
            try
            {
                var products = await _productMongoRepository.GetAllProducts();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/ProductMongo/5
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var product = await _productMongoRepository.GetProductById(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Posts the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        [HttpPost]
        // POST: api/ProductMongo
        public async Task<IHttpActionResult> Post([FromBody]ProductMongoModel product)
        {
            try
            {
                await _productMongoRepository.AddProduct(product);
                var newProduct = await _productMongoRepository.GetProductById(product.Id.ToString());
                if (newProduct != null)
                {
                    return CreatedAtRoute("DefaultApi", new { id = product.Id }, newProduct);
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return BadRequest();
        }

        // PUT: api/ProductMongo/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="productModel">The product model.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, [FromBody]ProductMongoModel productModel)
        {
            var product = await _productMongoRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            try
            {
                productModel.Id = product.Id;
                await _productMongoRepository.Update(productModel);
                return Ok(productModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/ProductMongo/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete(string id)
        {
            var product = await _productMongoRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            try
            {
                await _productMongoRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
