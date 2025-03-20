using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using RotaViagemAPI.Controllers;
using RotaViagemAPI.Models;
using RotaViagemAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaViagemAPI.Tests
{
    public class RotasControllerTests
    {
        private RotaContext _context;
        private RotasController _controller;

        public RotasControllerTests()
        {
            // Usando o InMemoryDatabase para testes
            var options = new DbContextOptionsBuilder<RotaContext>()
                            .UseInMemoryDatabase("TestDatabase")  // Nome do banco de dados em memória
                            .Options;

            _context = new RotaContext(options); // Criando o contexto com a DB em memória
            _controller = new RotasController(_context); // Passando o contexto para o controller
        }

        [Fact]
        public async Task GetRotas_ReturnsOkResult_WithListOfRotas()
        {
            // Arrange
            var rotas = new List<Rota>
            {
                new Rota { Id = 1, Origem = "A", Destino = "B", Valor = 100 },
                new Rota { Id = 2, Origem = "B", Destino = "C", Valor = 150 }
            };

            // Adicionando as rotas ao banco em memória
            _context.Rotas.AddRange(rotas);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetRotas();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultRotas = Assert.IsType<List<Rota>>(okResult.Value);
            Assert.Equal(2, resultRotas.Count);
        }

        [Fact]
        public async Task PostRota_ReturnsCreatedAtAction_WithRota()
        {
            // Arrange
            var newRota = new Rota { Origem = "A", Destino = "C", Valor = 200 };

            // Act
            var result = await _controller.PostRota(newRota);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var rota = Assert.IsType<Rota>(createdAtActionResult.Value);
            Assert.Equal("A", rota.Origem);
            Assert.Equal("C", rota.Destino);
            Assert.Equal(200, rota.Valor);
        }

        // Outros testes seguem o mesmo padrão...
    }
}

