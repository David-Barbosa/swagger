using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Swagger.Models;

namespace Swagger.Controllers
{
    [RoutePrefix("api/Produtos")]
    public class ProdutosController : ApiController
    {

        private static List<Produto> ProdutosList;

        public ProdutosController()
        {
            if (ProdutosList == null)
            {
                ProdutosList = ProdutosData.CreateProdutos();
            }

        }

        /// <summary>
        /// Retorna todos os produtos
        /// </summary>
        /// <remarks>Retorna um array com todos os Produtos</remarks>
        /// <response code="500">Internal Server Error</response>
        [Route("")]
        [ResponseType(typeof(List<Produto>))]
        public IHttpActionResult Get()
        {
            return Ok(ProdutosList);
        }

        /// <summary>
        /// Get Produto
        /// </summary>
        /// <param name="Nome">Unique username</param>
        /// <remarks>Retorna o Produto pelo o nome informado</remarks>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{Nome:alpha}", Name = "RetornaProdutoPeloNome")]
        [ResponseType(typeof(Produto))]
        public IHttpActionResult Get(string Nome)
        {

            var Produto = ProdutosList.Where(s => s.Nome == Nome).FirstOrDefault();

            if (Produto == null)
            {
                return NotFound();
            }

            return Ok(Produto);
        }

        /// <summary>
        /// Novo Produto
        /// </summary>
        /// <param name="Produto">Produto Model</param>
        /// <remarks>Insere novo Produto</remarks>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [Route("")]
        [ResponseType(typeof(Produto))]
        public IHttpActionResult Post(Produto Produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ProdutosList.Any(s => s.Nome == Produto.Nome))
            {
                return BadRequest("Nome já existe");
            }

            ProdutosList.Add(Produto);

            string uri = Url.Link("RetornaProdutoPeloNome", new { Nome = Produto.Nome });

            return Created(uri, Produto);
        }

        /// <summary>
        /// Delete Produto
        /// </summary>
        /// <param name="userName">Unique username</param>
        /// <remarks>Deleta um Produto</remarks>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{Nome:alpha}")]
        public HttpResponseMessage Delete(string nome)
        {

            var Produto = ProdutosList.Where(s => s.Nome == nome).FirstOrDefault();

            if (Produto == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            ProdutosList.Remove(Produto);

            return Request.CreateResponse(HttpStatusCode.NoContent);

        }

    }

    public class ProdutosData
    {
        public static List<Produto> CreateProdutos()
        {

            List<Produto> ProdutosList = new List<Produto>();

            for (int i = 0; i < Produtos.Length; i++)
            {
                var nome = SplitValue(Produtos[i]);
                var Produto = new Produto()
                {
                    Nome = string.Format("{0}", nome[0]),
                    Descricao = string.Format("{0}", nome[1]),
                    Grupo = string.Format("{0}", nome[2]),
                };

                ProdutosList.Add(Produto);
            }

            return ProdutosList;
        }

        static string[] Produtos =
        {
            "Mouse,Teste swagger,Informática",
            "Teclado,Teste swagger,Informática",
            "Monitor,Teste swagger,Informática",
            "Notebook,Teste swagger,Informática",
            "Impressora,Teste swagger,Informática",
            "Tablet,Teste swagger,Informática",
        };

        private static string[] SplitValue(string val)
        {
            return val.Split(',');
        }
    }
}
