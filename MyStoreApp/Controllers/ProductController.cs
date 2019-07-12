using AutoMapper;
using Microsoft.Web.Http;
using MyStore.Database;
using MyStore.Repository.Interface;
using MyStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace MyStoreApp.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> repository, IMapper mapper)
        {
            _productRepository = repository;
            _mapper = mapper;
        }

        // GET: api/product
        public IHttpActionResult GetProducts()
        {
            try
            {
                var products = _mapper.Map<IEnumerable<ProductModel>>(_productRepository.GetAll());
                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/product/a012c714-ef16-421c-8a45-0fd50352b000
        [ResponseType(typeof(Product))]
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetProduct(Guid id)
        {
            try
            {
                var product = _mapper.Map<ProductModel>(_productRepository.GetById(id));
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/product/search/xs
        [MapToApiVersion("1.1")]
        [Route("search/{name}")]
        [HttpGet]
        public IHttpActionResult SearchByName(string name)
        {
            try
            {
                var products = _mapper.Map<IEnumerable<ProductModel>>(_productRepository.GetAll().Where(x => x.Name.Contains(name)));
                
                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        
        [HttpPost]
        public IHttpActionResult Post(ProductModel productModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var product = _mapper.Map<Product>(productModel);

                    _productRepository.AddItem(product);
                    if(_productRepository.SaveChanges())
                    {
                        var newProduct = _mapper.Map<ProductModel>(product);
                        return CreatedAtRoute("DefaultApi", new { id = product.Id }, newProduct);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult PutItem(Guid id, ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            try
            {
                _mapper.Map(productModel, product);

                if(_productRepository.SaveChanges())
                {
                    return Ok(_mapper.Map<ProductModel>(product));
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                var product = _productRepository.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }
                _productRepository.RemoveItem(product);

                if(_productRepository.SaveChanges())
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
