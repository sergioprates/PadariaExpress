using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PadariaExpress.Aplicacao.Interfaces;
using PadariaExpress.Dominio.Modelo;
using PadariaExpress.Website.ViewModels;
using AutoMapper;
using System.IO;

namespace PadariaExpress.Website.Controllers
{
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioAppServico _servico;
        private readonly IPadariaAppServico _servicoPadaria;
        private readonly IConviteFuncionarioAppServico _servicoConviteFuncionario;

        public UsuarioController(IUsuarioAppServico servico, IPadariaAppServico servicoPadaria, IConviteFuncionarioAppServico servicoConviteFuncionario)
        {
            _servico = servico;
            _servicoPadaria = servicoPadaria;
            _servicoConviteFuncionario = servicoConviteFuncionario;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/proprietario")]
        public Task<HttpResponseMessage> Registrar(RegistrarProprietario model)
        {
            HttpResponseMessage response;

            try
            {
                var usuarioDominio = Mapper.Map<RegistrarProprietario, Proprietario>(model);
                usuarioDominio.TipoUsuario = TipoUsuario.Proprietario;
                usuarioDominio.Sexo = new Sexo()
                {
                    SexoId = model.SexoId
                };

                string html = String.Empty;

                using (StreamReader sr = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/html/proprietariocadastrado.html")))
                {
                    html = sr.ReadToEnd();
                }

                var usuario = _servico.Registrar(usuarioDominio, model.ConfirmaSenha, html);

                var proprietarioViewModel = Mapper.Map<Proprietario, ProprietarioViewModel>((Proprietario)usuario);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Usuário cadastrado com sucesso!",
                    d = proprietarioViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/proprietario/atualizar")]
        public Task<HttpResponseMessage> Atualizar(RegistrarProprietario model)
        {
            HttpResponseMessage response;

            try
            {
                var usuarioDominio = Mapper.Map<RegistrarProprietario, Proprietario>(model);
                usuarioDominio.TipoUsuario = TipoUsuario.Proprietario;
                usuarioDominio.Sexo = new Sexo()
                {
                    SexoId = model.SexoId
                };

                _servico.AtualizarUsuario(usuarioDominio, model.ConfirmaSenha);
                var proprietarioDominio = _servico.BuscarPorId(model.UsuarioId);

                var proprietarioViewModel = Mapper.Map<Proprietario, ProprietarioViewModel>((Proprietario)proprietarioDominio);
                proprietarioViewModel.Senha = model.Senha;
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Usuário alterado com sucesso!",
                    d = proprietarioViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/cliente")]
        public Task<HttpResponseMessage> Registrar(RegistrarCliente model)
        {
            HttpResponseMessage response;

            try
            {
                var usuarioDominio = Mapper.Map<RegistrarCliente, Cliente>(model);
                usuarioDominio.TipoUsuario = TipoUsuario.Cliente;
                usuarioDominio.Sexo = new Sexo()
                {
                    SexoId = model.SexoId
                };

                string html = String.Empty;

                using (StreamReader sr = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/html/clientecadastrado.html")))
                {
                    html = sr.ReadToEnd();
                }

                var usuario = _servico.Registrar(usuarioDominio, model.ConfirmaSenha, html);

                var clienteViewModel = Mapper.Map<Cliente, RegistrarCliente>((Cliente)usuario);
                clienteViewModel.Senha = model.Senha;
                clienteViewModel.ConfirmaSenha = model.Senha;
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Usuário cadastrado com sucesso!",
                    d = clienteViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/cliente/atualizar")]
        public Task<HttpResponseMessage> Atualizar(RegistrarCliente model)
        {
            HttpResponseMessage response;

            try
            {
                var usuarioDominio = Mapper.Map<RegistrarCliente, Cliente>(model);
                usuarioDominio.TipoUsuario = TipoUsuario.Cliente;
                usuarioDominio.Sexo = new Sexo()
                {
                    SexoId = model.SexoId
                };

                _servico.AtualizarUsuario(usuarioDominio, model.ConfirmaSenha);

                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Usuário alterado com sucesso!",
                    d = model
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/usuario")]
        public Task<HttpResponseMessage> Autenticar(AutenticarUsuario model)
        {
            HttpResponseMessage response;

            try
            {
                var usuario = _servico.Autenticar(model.Email, model.Senha);

                if (usuario.GetType().Name.Contains(typeof(Proprietario).Name) == false && usuario.GetType().Name.Contains(typeof(Funcionario).Name) == false)
                {
                    throw new Exception("Login ou senha estão incorretos");
                }

                if (usuario.GetType().Name.Contains(typeof(Proprietario).Name))
                {
                    ProprietarioViewModel proprietarioViewModel = Mapper.Map<Proprietario, ProprietarioViewModel>((Proprietario)usuario);

                    proprietarioViewModel.Senha = model.Senha;

                    response = Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        msg = "Login realizado com sucesso!",
                        d = proprietarioViewModel
                    });
                }
                else if (usuario.GetType().Name.Contains(typeof(Funcionario).Name))
                {
                    FuncionarioViewModel funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>((Funcionario)usuario);

                    funcionarioViewModel.Senha = model.Senha;

                    response = Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        msg = "Login realizado com sucesso!",
                        d = funcionarioViewModel
                    });
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, new
                    {
                        Message = "Clientes não podem acessar o sistema por aqui! Baixe nosso app!"
                    });
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/cliente/autenticar")]
        public Task<HttpResponseMessage> AutenticarCliente(AutenticarUsuario model)
        {
            HttpResponseMessage response;

            try
            {
                var usuario = _servico.Autenticar(model.Email, model.Senha);

                if (usuario.GetType().Name.Contains(typeof(Cliente).Name) == false)
                {
                    throw new Exception("Login ou senha estão incorretos");
                }

                RegistrarCliente funcionarioViewModel = Mapper.Map<Cliente, RegistrarCliente>((Cliente)usuario);

                funcionarioViewModel.Senha = model.Senha;

                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Login realizado com sucesso!",
                    d = funcionarioViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        //[Authorize]
        [Route("api/funcionarios/{PadariaId}")]
        public Task<HttpResponseMessage> ListarFuncionariosAtivos(int PadariaId)
        {
            HttpResponseMessage response;

            try
            {
                Padaria p = new Padaria();
                p.PadariaId = PadariaId;
                var funcionarios = (IEnumerable<Funcionario>)_servico.ListarFuncionarioPorPadaria(p);

                var funcionariosViewModel = Mapper.Map<IEnumerable<Funcionario>, IEnumerable<FuncionarioViewModel>>(funcionarios);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "OK",
                    d = funcionariosViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/funcionario")]
        public Task<HttpResponseMessage> Registrar(RegistrarFuncionario model)
        {
            HttpResponseMessage response;

            try
            {
                var usuarioDominio = Mapper.Map<RegistrarFuncionario, Funcionario>(model);
                usuarioDominio.TipoUsuario = TipoUsuario.Funcionario;
                usuarioDominio.Padarias = new List<Padaria>();

                ConviteFuncionario c = _servicoConviteFuncionario.BuscarPorHash(model.Hash);

                usuarioDominio.Padarias.Add(new Padaria()
                {
                    PadariaId = c.PadariaId
                });

                if (c.Utilizado == false)
                {
                    c.Utilizado = true;
                    _servicoConviteFuncionario.Alterar(c);
                }
                else
                {
                    throw new Exception("Convite já utilizado.");
                }

                usuarioDominio.Sexo = new Sexo()
                {
                    SexoId = model.SexoId
                };

                string html = String.Empty;

                using (StreamReader sr = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/html/funcionariocadastrado.html")))
                {
                    html = sr.ReadToEnd();
                }

                var usuario = _servico.Registrar(usuarioDominio, model.ConfirmaSenha, html);

                var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>((Funcionario)usuario);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Usuário cadastrado com sucesso!",
                    d = funcionarioViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        //[Authorize]
        [Route("api/funcionario/atualizar")]
        public Task<HttpResponseMessage> Atualizar(RegistrarFuncionario model)
        {
            HttpResponseMessage response;

            try
            {
                var usuarioDominio = Mapper.Map<RegistrarFuncionario, Funcionario>(model);
                usuarioDominio.TipoUsuario = TipoUsuario.Funcionario;
                usuarioDominio.Sexo = new Sexo()
                {
                    SexoId = model.SexoId
                };

                if (model.Senha == "yZuqjeQQw")
                {
                    var f = _servico.BuscarPorId(model.UsuarioId);
                    usuarioDominio.Senha = f.Senha;
                    model.ConfirmaSenha = f.Senha;
                }

                usuarioDominio.Padarias = new List<Padaria>();
                usuarioDominio.Padarias.Add(new Padaria()
                    {
                        PadariaId = model.PadariaId
                    });

                _servico.AtualizarUsuario(usuarioDominio, model.ConfirmaSenha);


                var funcionarioDominio = _servico.BuscarPorId(model.UsuarioId);

                var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>((Funcionario)funcionarioDominio);
                funcionarioViewModel.Senha = model.Senha;
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "Usuário alterado com sucesso!",
                    d = funcionarioViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }


        [HttpGet]
        //[Authorize]
        [Route("api/funcionario/buscar/{UsuarioId}")]
        public Task<HttpResponseMessage> Buscar(int UsuarioId)
        {
            HttpResponseMessage response;

            try
            {
                var funcionarioDominio = _servico.BuscarPorId(UsuarioId);

                var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>((Funcionario)funcionarioDominio);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "OK",
                    d = funcionarioViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }


        [HttpGet]
        //[Authorize]
        [Route("api/funcionario/convite/")]
        public Task<HttpResponseMessage> ConvidarFuncionario([FromUri] int PadariaId, string Email)
        {
            HttpResponseMessage response;

            try
            {
                Usuario u = new Usuario(Email, "Convite");
                Padaria p = new Padaria() { PadariaId = PadariaId };
                string html = String.Empty;

                using (StreamReader sr = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/html/convitefuncionario.html")))
                {
                    html = sr.ReadToEnd();
                }

                _servico.ConvidarFuncionario(u, p, html);

                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "OK"
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        //[Authorize]
        [Route("api/funcionario/ativacao/{UsuarioId}")]
        public Task<HttpResponseMessage> AlterarAtivacaoFuncionario(int UsuarioId)
        {
            HttpResponseMessage response;

            try
            {
                var funcionarioDominio = _servico.BuscarPorId(UsuarioId);

                funcionarioDominio.Ativo = !funcionarioDominio.Ativo;
                _servico.Alterar(funcionarioDominio);

                var funcionarioViewModel = Mapper.Map<Funcionario, FuncionarioViewModel>((Funcionario)funcionarioDominio);
                response = Request.CreateResponse(HttpStatusCode.OK, new
                {
                    msg = "OK",
                    d = funcionarioViewModel
                });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ex.Message, InnerException = ex.InnerException });
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
    }
}
