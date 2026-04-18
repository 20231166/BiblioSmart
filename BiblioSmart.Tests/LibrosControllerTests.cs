using BiblioSmart.Core.Entities;
using BiblioSmart.Web.Controllers;
using BiblioSmart.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiblioSmart.Tests;

public class LibrosControllerTests
{
    [Fact]
    public async Task Create_GuardaLibro_YRedirigeAlListado()
    {
        var repository = new InMemoryLibroRepository();
        var controller = new LibrosController(repository);
        var libro = new Libro
        {
            Titulo = "Clean Code",
            Autor = "Robert C. Martin",
            ISBN = "978-0-13-235088-4",
            Categoria = "Tecnologia",
            CantidadTotal = 4
        };

        var result = await controller.Create(libro);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);

        var libros = (await repository.GetAllAsync()).ToList();
        var registrado = Assert.Single(libros.Where(l => l.ISBN == libro.ISBN));
        Assert.Equal(4, registrado.CantidadTotal);
        Assert.Equal(4, registrado.CantidadDisponible);
    }

    [Fact]
    public async Task Create_ConModelStateInvalido_RetornaLaVistaConElModelo()
    {
        var repository = new InMemoryLibroRepository();
        var controller = new LibrosController(repository);
        var libro = new Libro
        {
            Titulo = string.Empty,
            Autor = "Autor de prueba",
            ISBN = "123",
            Categoria = "Prueba",
            CantidadTotal = 1
        };

        controller.ModelState.AddModelError("Titulo", "El titulo es obligatorio.");

        var result = await controller.Create(libro);

        var view = Assert.IsType<ViewResult>(result);
        Assert.Same(libro, view.Model);

        var libros = await repository.GetAllAsync();
        Assert.DoesNotContain(libros, l => l.ISBN == libro.ISBN);
    }

    [Fact]
    public async Task Index_ConBusqueda_FiltraPorTituloOAutor()
    {
        var repository = new InMemoryLibroRepository();
        var controller = new LibrosController(repository);

        var result = await controller.Index("Cervantes");

        var view = Assert.IsType<ViewResult>(result);
        var libros = Assert.IsAssignableFrom<IEnumerable<Libro>>(view.Model);
        var lista = libros.ToList();

        Assert.Single(lista);
        Assert.Equal("Miguel de Cervantes", lista[0].Autor);
    }
}
