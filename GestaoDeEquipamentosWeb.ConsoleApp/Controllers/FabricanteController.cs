using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloFabricante;
using GestaoDeEquipamentosWeb.ConsoleApp.Models;
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

        List<ListarFabricantesViewModel> ListarVms = new List<ListarFabricantesViewModel>();

        foreach(Fabricante f in fabricantes)
        {
            
            ListarFabricantesViewModel viewModel = new ListarFabricantesViewModel(f.Id,f.Nome,f.Email,f.Telefone);
            ListarVms.Add(viewModel);
        }
        return View(ListarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarFabricanteViewModel cadastrarVm)
    {
        Fabricante novoFabricante = new Fabricante(cadastrarVm.Nome, cadastrarVm.Email, cadastrarVm.Telefone);

        repositorioFabricante.Cadastrar(novoFabricante);


        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(id);

        if(fabricante == null)
            return RedirectToAction(nameof(Listar));
        EditarFabricanteViewModel editarVm = new EditarFabricanteViewModel(
            id,
            fabricante.Nome,
            fabricante.Email,
            fabricante.Telefone
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarFabricanteViewModel editarVm)
    {
       if(!ModelState.IsValid)
            return View(editarVm);

        Fabricante fabricanteAtualizado = new Fabricante(
            editarVm.Nome,
            editarVm.Email,
            editarVm.Telefone
        );

        repositorioFabricante.Editar(editarVm.Id, fabricanteAtualizado);

        return RedirectToAction(nameof(Listar));

    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {

        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(id);

      if(fabricante == null)
            return RedirectToAction(nameof(Listar));

      ExcluirFabricanteViewModel excluirVm = new ExcluirFabricanteViewModel(
        id,
        fabricante.Nome,
        fabricante.Email,
        fabricante.Telefone
      );

        return View(excluirVm);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ExcluirConfirmado(ExcluirFabricanteViewModel excluirVm)
    {
        Fabricante? fabricante = repositorioFabricante.SelecionarPorId(excluirVm.Id);

        if(fabricante == null)
            return RedirectToAction(nameof(Listar));

        repositorioFabricante.Excluir(fabricante);
        return RedirectToAction(nameof(Listar));
    }
}

