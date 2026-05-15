using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentosWeb.ConsoleApp;

//MVC



public class FabricanteController : Controller
{

    private readonly IRepositorio<Fabricante> repositorioFabricante;
    // GET: FabricanteController
    public FabricanteController()
    {
        ContextoJson contexto = new ContextoJson();

        contexto.Carregar();

        repositorioFabricante = new RepositorioFabricanteEmArquivo(contexto);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Fabricante> fabricantes = repositorioFabricante.SelecionarTodos();

        return View(fabricantes);
    }
}

